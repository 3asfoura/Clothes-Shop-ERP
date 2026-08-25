using Clothes_Shop_ERP.DAL;
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

        private ComboBoxEdit CmbSupplier, CmbBranch, CmbVariant;
        private SpinEdit SpinQty, SpinCost, SpinPaidNow;
        private GridControl GridLines;
        private GridView GridViewLines;
        private LabelControl LblTotal;

        private List<int> _supplierIds = new List<int>();
        private List<int> _branchIds = new List<int>();
        private List<int> _variantIds = new List<int>();
        private BindingList<PurchaseLineItem> _lines = new BindingList<PurchaseLineItem>();

        public FrmPurchaseInvoiceEdit(string title)
        {
            this.Text = title;
            this.Width = 620;
            this.Height = 580;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // ---- Header ----
            var lblSupplier = new LabelControl { Text = "Supplier:", Location = new System.Drawing.Point(20, 15) };
            CmbSupplier = new ComboBoxEdit { Location = new System.Drawing.Point(20, 33), Width = 270 };
            CmbSupplier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblBranch = new LabelControl { Text = "Branch:", Location = new System.Drawing.Point(310, 15) };
            CmbBranch = new ComboBoxEdit { Location = new System.Drawing.Point(310, 33), Width = 270 };
            CmbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // ---- Add-line row: create every control first, before loading any data ----
            var lblLine = new LabelControl { Text = "Add item:", Location = new System.Drawing.Point(20, 70) };

            CmbVariant = new ComboBoxEdit { Location = new System.Drawing.Point(20, 90), Width = 280 };
            CmbVariant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblQty = new LabelControl { Text = "Qty:", Location = new System.Drawing.Point(310, 70) };
            SpinQty = new SpinEdit { Location = new System.Drawing.Point(310, 90), Width = 80, Value = 1 };
            SpinQty.Properties.MinValue = 1;
            SpinQty.Properties.MaxValue = 99999;

            var lblCost = new LabelControl { Text = "Unit Cost:", Location = new System.Drawing.Point(400, 70) };
            SpinCost = new SpinEdit { Location = new System.Drawing.Point(400, 90), Width = 90 };
            SpinCost.Properties.MaxValue = 999999;
            SpinCost.Properties.DisplayFormat.FormatString = "n2";

            var btnAddLine = new SimpleButton { Text = "Add", Location = new System.Drawing.Point(500, 90), Width = 80 };
            btnAddLine.Click += (s, e) =>
            {
                if (CmbVariant.SelectedIndex < 0)
                {
                    XtraMessageBox.Show("Please select a product first.");
                    return;
                }
                _lines.Add(new PurchaseLineItem
                {
                    ProductVariantId = _variantIds[CmbVariant.SelectedIndex],
                    ProductDisplay = CmbVariant.Text,
                    Quantity = (decimal)SpinQty.Value,
                    UnitCost = (decimal)SpinCost.Value
                });
                RefreshTotal();
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
                foreach (var v in db.ProductVariants.Include(x => x.Product).Where(x => x.IsActive == true).ToList())
                {
                    CmbVariant.Properties.Items.Add($"{v.Product.Name} - {v.Barcode}");
                    _variantIds.Add(v.Id);
                }
            }
            if (_supplierIds.Count > 0) CmbSupplier.SelectedIndex = 0;
            int branchIdx = _branchIds.IndexOf(FrmLogin.CurrentBranchId);
            CmbBranch.SelectedIndex = branchIdx >= 0 ? branchIdx : 0;

            // ---- Lines grid ----
            GridLines = new GridControl { Location = new System.Drawing.Point(20, 125), Size = new System.Drawing.Size(560, 220) };
            GridViewLines = new GridView(GridLines);
            GridLines.MainView = GridViewLines;
            GridLines.DataSource = _lines;
            GridViewLines.OptionsBehavior.Editable = false;

            var btnRemoveLine = new SimpleButton { Text = "Remove Selected Line", Location = new System.Drawing.Point(20, 355), Width = 180 };
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
                Text = "Total: 0.00",
                Location = new System.Drawing.Point(420, 358),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };

            // ---- Payment ----
            var lblPaid = new LabelControl { Text = "Amount Paid Now:", Location = new System.Drawing.Point(20, 400) };
            SpinPaidNow = new SpinEdit { Value = 0, Location = new System.Drawing.Point(20, 420), Width = 200 };
            SpinPaidNow.Properties.MaxValue = 9999999;
            SpinPaidNow.Properties.DisplayFormat.FormatString = "n2";

            var lblPaidHint = new LabelControl
            {
                Text = "(Leave as 0 for a fully credit/unpaid purchase)",
                Location = new System.Drawing.Point(230, 425),
                ForeColor = System.Drawing.Color.Gray
            };

            var btnSave = new SimpleButton { Text = "Save Invoice", Location = new System.Drawing.Point(340, 460), Width = 120, DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (_lines.Count == 0)
                {
                    XtraMessageBox.Show("Please add at least one item to the invoice.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(470, 460), Width = 100, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblSupplier); this.Controls.Add(CmbSupplier);
            this.Controls.Add(lblBranch); this.Controls.Add(CmbBranch);
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

        private void RefreshTotal()
        {
            decimal total = _lines.Sum(l => l.Total);
            LblTotal.Text = $"Total: {total:n2}";
        }
    }
}