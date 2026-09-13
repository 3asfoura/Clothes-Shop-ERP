using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public partial class FrmPurchaseReturnEdit : DevExpress.XtraEditors.XtraForm
    {
        public int PurchaseInvoiceId => _invoiceIds[CmbInvoice.SelectedIndex];
        public int ProductVariantId => _lineVariantIds[CmbLine.SelectedIndex];
        public decimal UnitCost => _lineUnitCosts[CmbLine.SelectedIndex];
        public decimal Quantity => (decimal)SpinQuantity.Value;

        private ComboBoxEdit CmbInvoice, CmbLine;
        private SpinEdit SpinQuantity;
        private TextEdit TxtSearchSupplier;
        private List<int> _invoiceIds = new List<int>();
        private List<int> _lineVariantIds = new List<int>();
        private List<decimal> _lineUnitCosts = new List<decimal>();
        private List<decimal> _lineMaxQty = new List<decimal>();

        public FrmPurchaseReturnEdit()
        {
            InitializeComponent();
        }

        public FrmPurchaseReturnEdit(string title)
        {
            this.Text = title;
            this.Width = 400;
            this.Height = 315;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // The invoice list below only shows the 50 most recent for this branch (the
            // common case, and cheap to load) - purchase invoices have no invoice
            // number of their own (unlike sales invoices), so this searches by
            // supplier name instead, to still reach an older invoice.
            var lblSearch = new LabelControl { Text = LocalizationManager.T("FrmPurchaseReturnEdit_SearchSupplier"), Location = new System.Drawing.Point(20, 20) };
            TxtSearchSupplier = new TextEdit { Location = new System.Drawing.Point(20, 40), Width = 340 };

            var lblInvoice = new LabelControl { Text = LocalizationManager.T("FrmPurchaseReturnEdit_Invoice"), Location = new System.Drawing.Point(20, 75) };
            CmbInvoice = new ComboBoxEdit { Location = new System.Drawing.Point(20, 95), Width = 340 };
            CmbInvoice.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblLine = new LabelControl { Text = LocalizationManager.T("FrmReturnEdit_ItemToReturn"), Location = new System.Drawing.Point(20, 130) };
            CmbLine = new ComboBoxEdit { Location = new System.Drawing.Point(20, 150), Width = 340 };
            CmbLine.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblQty = new LabelControl { Text = LocalizationManager.T("FrmReturnEdit_QuantityToReturn"), Location = new System.Drawing.Point(20, 185) };
            SpinQuantity = new SpinEdit { Location = new System.Drawing.Point(20, 205), Width = 340, Value = 1 };
            SpinQuantity.Properties.MinValue = 1;
            CmbInvoice.SelectedIndexChanged += (s, e) => LoadInvoiceLines();
            CmbLine.SelectedIndexChanged += (s, e) =>
            {
                if (CmbLine.SelectedIndex >= 0)
                    SpinQuantity.Properties.MaxValue = _lineMaxQty[CmbLine.SelectedIndex];
            };
            TxtSearchSupplier.EditValueChanged += (s, e) => LoadInvoices(TxtSearchSupplier.Text);

            LoadInvoices(null);

            var btnSave = new SimpleButton { Text = LocalizationManager.T("FrmReturnEdit_BtnSaveReturn"), Location = new System.Drawing.Point(180, 240), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (CmbInvoice.SelectedIndex < 0 || CmbLine.SelectedIndex < 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Returns_SelectInvoiceAndItem"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(280, 240), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblSearch); this.Controls.Add(TxtSearchSupplier);
            this.Controls.Add(lblInvoice); this.Controls.Add(CmbInvoice);
            this.Controls.Add(lblLine); this.Controls.Add(CmbLine);
            this.Controls.Add(lblQty); this.Controls.Add(SpinQuantity);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        // No filter = the 50 most recent invoices for this branch (fast, covers the
        // common case). A filter searches by supplier name across ALL of this
        // branch's invoices instead, so an older one is still reachable.
        private void LoadInvoices(string searchFilter)
        {
            CmbInvoice.Properties.Items.Clear();
            _invoiceIds.Clear();

            using (var db = new ClothesShopDBContext())
            {
                var query = db.PurchaseInvoices
                    .Include(x => x.Supplier)
                    .Where(x => x.BranchId == FrmLogin.CurrentBranchId);
                if (!string.IsNullOrWhiteSpace(searchFilter))
                    query = query.Where(x => x.Supplier.Name.Contains(searchFilter));

                var invoices = query
                    .OrderByDescending(x => x.InvoiceDate)
                    .Take(string.IsNullOrWhiteSpace(searchFilter) ? 50 : 100)
                    .ToList();
                foreach (var inv in invoices)
                {
                    CmbInvoice.Properties.Items.Add($"{inv.Supplier.Name} - {inv.InvoiceDate:dd/MM/yyyy HH:mm}");
                    _invoiceIds.Add(inv.Id);
                }
            }

            if (_invoiceIds.Count > 0)
            {
                CmbInvoice.SelectedIndex = 0;
                LoadInvoiceLines();
            }
            else
            {
                CmbLine.Properties.Items.Clear();
                _lineVariantIds.Clear();
                _lineUnitCosts.Clear();
                _lineMaxQty.Clear();
            }
        }

        private void LoadInvoiceLines()
        {
            CmbLine.Properties.Items.Clear();
            _lineVariantIds.Clear();
            _lineUnitCosts.Clear();
            _lineMaxQty.Clear();

            if (CmbInvoice.SelectedIndex < 0) return;
            int invoiceId = _invoiceIds[CmbInvoice.SelectedIndex];

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

                    if (remaining <= 0) continue;

                    CmbLine.Properties.Items.Add(
                        $"{l.ProductVariant.Product.Name} ({l.ProductVariant.Barcode})  " + string.Format(LocalizationManager.T("Shared_RemainingOfFmt"), remaining, l.Quantity));
                    _lineVariantIds.Add(l.ProductVariantId);
                    _lineUnitCosts.Add(l.UnitCost);
                    _lineMaxQty.Add(remaining);
                }
            }
            if (CmbLine.Properties.Items.Count > 0) CmbLine.SelectedIndex = 0;
        }
    }
}
