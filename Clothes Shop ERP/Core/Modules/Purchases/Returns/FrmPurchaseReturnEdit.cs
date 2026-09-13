using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // One row per still-returnable line of the chosen purchase invoice. The user types
    // a quantity against each item going back to the supplier, so returning three
    // items off one invoice is one trip through this dialog, not three.
    public class PurchaseReturnLineItem
    {
        public int ProductVariantId { get; set; }
        public string Product { get; set; }
        public decimal Remaining { get; set; }
        public decimal ReturnQty { get; set; }
        public decimal UnitCost { get; set; }
    }

    public partial class FrmPurchaseReturnEdit : DevExpress.XtraEditors.XtraForm
    {
        // One selectable purchase invoice in the picker.
        private class InvoicePick
        {
            public int Id { get; set; }
            public string Display { get; set; }
        }

        public int PurchaseInvoiceId => Convert.ToInt32(CmbInvoice.EditValue);

        /// <summary>Only the rows the user actually put a quantity against.</summary>
        public List<PurchaseReturnLineItem> Lines => _lines.Where(l => l.ReturnQty > 0).ToList();

        // Everything is loaded once and filtered as the user types, so this only needs
        // to be larger than any realistic return window - it is not a "most recent N"
        // cut-off the user can hit and get stuck on.
        private const int InvoiceHistoryLimit = 2000;

        private LookUpEdit CmbInvoice;
        private GridControl GridLines;
        private GridView GridViewLines;
        private BindingList<PurchaseReturnLineItem> _lines = new BindingList<PurchaseReturnLineItem>();

        public FrmPurchaseReturnEdit()
        {
            InitializeComponent();
        }

        public FrmPurchaseReturnEdit(string title)
        {
            this.Text = title;
            this.Width = 580;
            this.Height = 425;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // A LookUpEdit rather than a plain dropdown: the user can type straight into
            // it and SearchMode.AutoFilter narrows the list as they type. Purchase
            // invoices have no invoice number of their own (unlike sales invoices), so
            // what's searchable here is the supplier name and the date.
            var lblInvoice = new LabelControl { Text = LocalizationManager.T("FrmPurchaseReturnEdit_Invoice"), Location = new System.Drawing.Point(20, 20) };
            CmbInvoice = new LookUpEdit { Location = new System.Drawing.Point(20, 40), Width = 520 };
            CmbInvoice.Properties.DisplayMember = "Display";
            CmbInvoice.Properties.ValueMember = "Id";
            CmbInvoice.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            CmbInvoice.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            CmbInvoice.Properties.AutoSearchColumnIndex = 0;
            CmbInvoice.Properties.ShowHeader = false;
            CmbInvoice.Properties.NullText = LocalizationManager.T("FrmPurchaseReturnEdit_SearchSupplier");

            var lblLines = new LabelControl { Text = LocalizationManager.T("FrmReturnEdit_LinesHint"), Location = new System.Drawing.Point(20, 75) };

            GridLines = new GridControl { Location = new System.Drawing.Point(20, 95), Size = new System.Drawing.Size(520, 210) };
            GridViewLines = new GridView(GridLines);
            GridLines.MainView = GridViewLines;
            // Off BEFORE binding: left on, DevExpress generates its own columns from the
            // property names and regenerates them every time the data source changes,
            // wiping any captions set beforehand.
            GridViewLines.OptionsBehavior.AutoPopulateColumns = false;
            GridLines.DataSource = _lines;
            GridViewLines.OptionsView.ShowGroupPanel = false;
            GridViewLines.OptionsBehavior.Editable = true;
            ConfigureLineColumns();

            CmbInvoice.EditValueChanged += (s, e) => LoadInvoiceLines();

            LoadInvoices();

            var btnSave = new SimpleButton { Text = LocalizationManager.T("FrmReturnEdit_BtnSaveReturn"), Location = new System.Drawing.Point(320, 330), Width = 110, DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                // Commits the cell still being edited - without this, the quantity the
                // user just typed isn't written back to the bound row yet and the
                // return would silently come out short.
                GridViewLines.PostEditor();
                GridViewLines.UpdateCurrentRow();

                if (CmbInvoice.EditValue == null)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Returns_SelectInvoiceAndItem"));
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (Lines.Count == 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("FrmReturnEdit_NoQuantityTyped"));
                    this.DialogResult = DialogResult.None;
                    return;
                }
                var tooMany = _lines.FirstOrDefault(l => l.ReturnQty > l.Remaining);
                if (tooMany != null)
                {
                    XtraMessageBox.Show(string.Format(LocalizationManager.T("FrmReturnEdit_QtyTooHighFmt"), tooMany.Product, tooMany.Remaining));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(440, 330), Width = 100, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblInvoice); this.Controls.Add(CmbInvoice);
            this.Controls.Add(lblLines); this.Controls.Add(GridLines);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.CancelButton = btnCancel;
        }

        // Columns are declared here rather than generated, so the internal id/cost
        // fields simply never appear and the captions are the app's own translated
        // ones. Same approach the POS cart grid already uses.
        private void ConfigureLineColumns()
        {
            var colProduct = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Product",
                Caption = LocalizationManager.T("StockCount_ColProduct"),
                Visible = true,
                VisibleIndex = 0
            };
            colProduct.OptionsColumn.AllowEdit = false;

            var colRemaining = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Remaining",
                Caption = LocalizationManager.T("FrmReturnEdit_ColRemaining"),
                Visible = true,
                VisibleIndex = 1
            };
            colRemaining.OptionsColumn.AllowEdit = false;
            colRemaining.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colRemaining.DisplayFormat.FormatString = "0.###";

            var qtyEditor = new RepositoryItemSpinEdit();
            qtyEditor.MinValue = 0;
            qtyEditor.MaxValue = 99999;
            qtyEditor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            qtyEditor.DisplayFormat.FormatString = "0.###";
            GridLines.RepositoryItems.Add(qtyEditor);

            // The only editable column - everything else is there to read.
            var colReturnQty = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "ReturnQty",
                Caption = LocalizationManager.T("FrmReturnEdit_ColReturnQty"),
                Visible = true,
                VisibleIndex = 2,
                ColumnEdit = qtyEditor
            };

            GridViewLines.Columns.AddRange(new[] { colProduct, colRemaining, colReturnQty });
        }

        // Loaded once; the editor filters this list itself as the user types, so there
        // is no per-keystroke database round trip and no "recent N only" blind spot.
        private void LoadInvoices()
        {
            List<InvoicePick> picks;
            using (var db = new ClothesShopDBContext())
            {
                picks = db.PurchaseInvoices
                    .Include(x => x.Supplier)
                    .Where(x => x.BranchId == FrmLogin.CurrentBranchId)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Take(InvoiceHistoryLimit)
                    .ToList()
                    .Select(x => new InvoicePick
                    {
                        Id = x.Id,
                        Display = $"{x.Supplier.Name} - {x.InvoiceDate:dd/MM/yyyy HH:mm}"
                    })
                    .ToList();
            }

            CmbInvoice.Properties.DataSource = picks;
            if (picks.Count > 0)
                CmbInvoice.EditValue = picks[0].Id;   // raises EditValueChanged -> loads its lines
            else
                _lines.Clear();
        }

        private void LoadInvoiceLines()
        {
            _lines.Clear();

            if (CmbInvoice.EditValue == null) return;
            int invoiceId = Convert.ToInt32(CmbInvoice.EditValue);

            using (var db = new ClothesShopDBContext())
            {
                var lines = db.PurchaseInvoiceDetails
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.PurchaseInvoiceId == invoiceId)
                    .ToList();

                foreach (var l in lines)
                {
                    decimal alreadyReturned = db.PurchaseReturns
                        .Where(r => r.PurchaseInvoiceId == invoiceId)
                        .SelectMany(r => r.PurchaseReturnDetails)
                        .Where(d => d.ProductVariantId == l.ProductVariantId)
                        .Sum(d => (decimal?)d.Quantity) ?? 0;

                    decimal remaining = l.Quantity - alreadyReturned;

                    // Fully returned already - nothing left to offer for this item.
                    if (remaining <= 0) continue;

                    _lines.Add(new PurchaseReturnLineItem
                    {
                        ProductVariantId = l.ProductVariantId,
                        Product = $"{l.ProductVariant.Product.Name} ({l.ProductVariant.Barcode})",
                        Remaining = remaining,
                        ReturnQty = 0,
                        UnitCost = l.UnitCost
                    });
                }
            }
        }
    }
}
