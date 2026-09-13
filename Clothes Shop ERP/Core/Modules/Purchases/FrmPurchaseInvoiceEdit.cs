using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
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
    public class PurchaseLineItem
    {
        public int ProductVariantId { get; set; }
        public string ProductDisplay { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total => Quantity * UnitCost;
    }

    public partial class FrmPurchaseInvoiceEdit : DevExpress.XtraEditors.XtraForm
    {
        public int SupplierId => _supplierIds[CmbSupplier.SelectedIndex];
        public int BranchId => _branchIds[CmbBranch.SelectedIndex];
        public List<PurchaseLineItem> Lines => _lines.ToList();
        public decimal PaidNow => (decimal)SpinPaidNow.Value;

        // One selectable product variant in the item picker.
        private class VariantPick
        {
            public int Id { get; set; }
            public string Display { get; set; }
        }

        private ComboBoxEdit CmbSupplier, CmbBranch;
        private LookUpEdit CmbVariant;
        private TextEdit TxtBarcode;
        private SpinEdit SpinQty, SpinCost, SpinPaidNow;
        private GridControl GridLines;
        private GridView GridViewLines;
        private LabelControl LblTotal;

        private List<int> _supplierIds = new List<int>();
        private List<int> _branchIds = new List<int>();
        private BindingList<PurchaseLineItem> _lines = new BindingList<PurchaseLineItem>();

        public FrmPurchaseInvoiceEdit(string title)
        {
            this.Text = title;
            this.Width = 620;
            this.Height = 620;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // ---- Header ----
            var lblSupplier = new LabelControl { Text = LocalizationManager.T("FrmPurchaseInvoiceEdit_Supplier"), Location = new System.Drawing.Point(20, 15) };
            CmbSupplier = new ComboBoxEdit { Location = new System.Drawing.Point(20, 33), Width = 270 };
            CmbSupplier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblBranch = new LabelControl { Text = LocalizationManager.T("Shared_ColBranch"), Location = new System.Drawing.Point(310, 15) };
            CmbBranch = new ComboBoxEdit { Location = new System.Drawing.Point(310, 33), Width = 270 };
            CmbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // ---- Quick-add by barcode: the CmbVariant dropdown below lists EVERY
            // active variant in the whole shop with no search - fine for a handful of
            // products, but a real scroll-hunt once there are hundreds. Scanning (or
            // typing) a barcode here and pressing Enter adds the line directly,
            // skipping that dropdown entirely - same pattern as the POS screen's own
            // barcode field (TxtBarcode_KeyDown in UcPointOfSale.cs). ----
            var lblBarcode = new LabelControl { Text = LocalizationManager.T("Purchases_ScanBarcodeQuickAdd"), Location = new System.Drawing.Point(20, 70) };
            TxtBarcode = new TextEdit { Location = new System.Drawing.Point(20, 90), Width = 280 };

            // ---- Add-line row: create every control first, before loading any data ----
            var lblLine = new LabelControl { Text = LocalizationManager.T("Shared_AddItem"), Location = new System.Drawing.Point(20, 110) };

            // Typeable, self-filtering picker: the shop's full active-variant list is
            // far too long to scroll through, so SearchMode.AutoFilter narrows it to
            // matching products/barcodes as the user types (the barcode box above is
            // still the fastest route when the item is physically in hand).
            CmbVariant = new LookUpEdit { Location = new System.Drawing.Point(20, 130), Width = 280 };
            CmbVariant.Properties.DisplayMember = "Display";
            CmbVariant.Properties.ValueMember = "Id";
            CmbVariant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            CmbVariant.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            CmbVariant.Properties.AutoSearchColumnIndex = 0;
            CmbVariant.Properties.ShowHeader = false;
            CmbVariant.Properties.NullText = LocalizationManager.T("Shared_AddItem");

            var lblQty = new LabelControl { Text = LocalizationManager.T("Shared_Qty"), Location = new System.Drawing.Point(310, 110) };
            SpinQty = new SpinEdit { Location = new System.Drawing.Point(310, 130), Width = 80, Value = 1 };
            SpinQty.Properties.MinValue = 1;
            SpinQty.Properties.MaxValue = 99999;

            var lblCost = new LabelControl { Text = LocalizationManager.T("FrmPurchaseInvoiceEdit_UnitCost"), Location = new System.Drawing.Point(400, 110) };
            SpinCost = new SpinEdit { Location = new System.Drawing.Point(400, 130), Width = 90 };
            SpinCost.Properties.MaxValue = 999999;
            SpinCost.Properties.DisplayFormat.FormatString = "n2";

            var btnAddLine = new SimpleButton { Text = LocalizationManager.T("POS_BtnAddManual"), Location = new System.Drawing.Point(500, 130), Width = 80 };

            btnAddLine.Click += (s, e) =>
            {
                if (CmbVariant.EditValue == null)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Shared_SelectProductFirst"));
                    return;
                }
                AddLine(Convert.ToInt32(CmbVariant.EditValue), CmbVariant.Text, (decimal)SpinQty.Value, (decimal)SpinCost.Value);
            };

            // ---- Now that every combo box exists, it's safe to load data into them ----
            using (var db = new ClothesShopDBContext())
            {
                foreach (var s in db.Suppliers.Where(x => x.IsActive == true).ToList())
                {
                    CmbSupplier.Properties.Items.Add(s.Name);
                    _supplierIds.Add(s.Id);
                }
                foreach (var b in db.Branches.ToList())
                {
                    CmbBranch.Properties.Items.Add(b.Name);
                    _branchIds.Add(b.Id);
                }
                var variantPicks = db.ProductVariants
                    .Include(x => x.Product).ThenInclude(p => p.Category)
                    .Where(x => x.IsActive == true
                             && x.Product.IsActive == true
                             && x.Product.Category.IsActive == true)
                    .ToList()
                    .Select(v => new VariantPick { Id = v.Id, Display = $"{v.Product.Name} - {v.Barcode}" })
                    .ToList();
                CmbVariant.Properties.DataSource = variantPicks;
            }
            if (_supplierIds.Count > 0) CmbSupplier.SelectedIndex = 0;
            if (_branchIds.Count > 0)
            {
                int branchIdx = _branchIds.IndexOf(FrmLogin.CurrentBranchId);
                CmbBranch.SelectedIndex = branchIdx >= 0 ? branchIdx : 0;
            }

            // ---- Lines grid ----
            GridLines = new GridControl { Location = new System.Drawing.Point(20, 165), Size = new System.Drawing.Size(560, 220) };
            GridViewLines = new GridView(GridLines);
            GridLines.MainView = GridViewLines;
            // Off BEFORE binding: left on, DevExpress generates its own columns from the
            // property names ("Product Display", "Unit Cost"...) and regenerates them
            // every time the data source changes, wiping any captions set beforehand -
            // which is why this grid kept coming out in English.
            GridViewLines.OptionsBehavior.AutoPopulateColumns = false;
            GridLines.DataSource = _lines;
            GridViewLines.OptionsBehavior.Editable = false;
            ConfigureLineColumns();

            var btnRemoveLine = new SimpleButton { Text = LocalizationManager.T("Shared_RemoveSelectedLine"), Location = new System.Drawing.Point(20, 395), Width = 180 };
            btnRemoveLine.Click += (s, e) =>
            {
                if (GridViewLines.FocusedRowHandle < 0) return;
                var line = GridViewLines.GetFocusedRow() as PurchaseLineItem;
                if (line != null)
                {
                    _lines.Remove(line);
                    RefreshTotal();
                }
            };

            LblTotal = new LabelControl
            {
                Text = string.Format(LocalizationManager.T("FrmPurchaseInvoiceEdit_TotalFmt"), 0),
                Location = new System.Drawing.Point(420, 398),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };

            // ---- Payment ----
            var lblPaid = new LabelControl { Text = LocalizationManager.T("FrmPurchaseInvoiceEdit_AmountPaidNow"), Location = new System.Drawing.Point(20, 440) };
            SpinPaidNow = new SpinEdit { Value = 0, Location = new System.Drawing.Point(20, 460), Width = 200 };
            SpinPaidNow.Properties.MaxValue = 9999999;
            SpinPaidNow.Properties.DisplayFormat.FormatString = "n2";

            var lblPaidHint = new LabelControl
            {
                Text = LocalizationManager.T("FrmPurchaseInvoiceEdit_PaidHint"),
                Location = new System.Drawing.Point(230, 465),
                ForeColor = System.Drawing.Color.Gray
            };

            var btnSave = new SimpleButton { Text = LocalizationManager.T("FrmPurchaseInvoiceEdit_BtnSaveInvoice"), Location = new System.Drawing.Point(340, 500), Width = 120, DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (CmbSupplier.SelectedIndex < 0 || CmbBranch.SelectedIndex < 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Purchases_MustHaveSupplierAndBranch"));
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (_lines.Count == 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Purchases_AddAtLeastOneInvoiceItem"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(470, 500), Width = 100, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblSupplier); this.Controls.Add(CmbSupplier);
            this.Controls.Add(lblBranch); this.Controls.Add(CmbBranch);
            this.Controls.Add(lblBarcode); this.Controls.Add(TxtBarcode);
            this.Controls.Add(lblLine);
            this.Controls.Add(CmbVariant);
            this.Controls.Add(lblQty); this.Controls.Add(SpinQty);
            this.Controls.Add(lblCost); this.Controls.Add(SpinCost);
            this.Controls.Add(btnAddLine);
            this.Controls.Add(GridLines);
            this.Controls.Add(btnRemoveLine);
            this.Controls.Add(LblTotal);
            this.Controls.Add(lblPaid); this.Controls.Add(SpinPaidNow); this.Controls.Add(lblPaidHint);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        // Columns are declared here rather than generated, so the internal
        // ProductVariantId never appears and the captions are the app's own translated
        // ones. Same approach the POS cart grid already uses.
        private void ConfigureLineColumns()
        {
            var colProduct = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "ProductDisplay",
                Caption = LocalizationManager.T("StockCount_ColProduct"),
                Visible = true,
                VisibleIndex = 0
            };

            var colQuantity = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Quantity",
                Caption = LocalizationManager.T("StockCount_ColQuantity"),
                Visible = true,
                VisibleIndex = 1
            };
            colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQuantity.DisplayFormat.FormatString = "0.###";

            var colUnitCost = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "UnitCost",
                Caption = LocalizationManager.T("Purchases_ColUnitCost"),
                Visible = true,
                VisibleIndex = 2
            };

            var colTotal = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Total",
                Caption = LocalizationManager.T("Shared_ColTotal"),
                Visible = true,
                VisibleIndex = 3
            };

            GridViewLines.Columns.AddRange(new[] { colProduct, colQuantity, colUnitCost, colTotal });
        }

        private void RefreshTotal()
        {
            decimal total = _lines.Sum(l => l.Total);
            LblTotal.Text = string.Format(LocalizationManager.T("FrmPurchaseInvoiceEdit_TotalFmt"), total);
        }

        private void AddLine(int variantId, string displayText, decimal qty, decimal cost)
        {
            _lines.Add(new PurchaseLineItem
            {
                ProductVariantId = variantId,
                ProductDisplay = displayText,
                Quantity = qty,
                UnitCost = cost
            });
            RefreshTotal();
        }

        private void ProcessBarcodeQuickAdd()
        {
            string code = TxtBarcode.Text.Trim();
            TxtBarcode.Text = "";
            if (string.IsNullOrEmpty(code)) return;

            using (var db = new ClothesShopDBContext())
            {
                var variant = db.ProductVariants
                    .Include(x => x.Product).ThenInclude(p => p.Category)
                    .FirstOrDefault(v => v.Barcode == code
                        && v.IsActive == true
                        && v.Product.IsActive == true
                        && v.Product.Category.IsActive == true);

                if (variant == null)
                {
                    Sett.MsgBlue(LocalizationManager.T("POS_NotFoundTitle"), string.Format(LocalizationManager.T("POS_ProductNotFoundByBarcode"), code));
                    return;
                }

                // Uses whatever qty/cost are already typed in SpinQty/SpinCost at the
                // moment of the scan (set those first, then scan) - not the variant's
                // stored cost, so this matches buying a batch at today's price.
                AddLine(variant.Id, $"{variant.Product.Name} - {variant.Barcode}", (decimal)SpinQty.Value, (decimal)SpinCost.Value);
            }

            // Reset after every add so a leftover qty/cost from this scan can't be
            // silently reused for the next, different item.
            SpinQty.Value = 1;
            SpinCost.Value = 0;
        }

        // A single-line DevExpress TextEdit doesn't "claim" the Enter key the way a
        // Multiline TextBox does, so pressing Enter inside TxtBarcode never even
        // reaches its KeyDown event - Windows routes it straight to this dialog's
        // AcceptButton (btnSave) first, confirmed in the field (scanning a barcode
        // was submitting/closing the whole invoice instead of adding the line).
        // Overriding ProcessCmdKey intercepts Enter earlier, before that routing
        // happens, so it can be handled here instead and never reach btnSave at all.
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && TxtBarcode != null && TxtBarcode.Focused)
            {
                ProcessBarcodeQuickAdd();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}