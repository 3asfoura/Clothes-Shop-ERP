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
    public class TransferLineItem
    {
        public int ProductVariantId { get; set; }
        public string ProductDisplay { get; set; }
        public decimal Quantity { get; set; }
    }

    public partial class FrmStockTransferEdit : DevExpress.XtraEditors.XtraForm
    {
        public int FromBranchId => _branchIds[CmbFromBranch.SelectedIndex];
        public int ToBranchId => _branchIds[CmbToBranch.SelectedIndex];
        public List<TransferLineItem> Lines => _lines.ToList();

        private ComboBoxEdit CmbFromBranch, CmbToBranch, CmbVariant;
        private SpinEdit SpinQty;
        private GridControl GridLines;
        private GridView GridViewLines;

        private List<int> _branchIds = new List<int>();
        private List<int> _variantIds = new List<int>();
        private BindingList<TransferLineItem> _lines = new BindingList<TransferLineItem>();

        public FrmStockTransferEdit(string title)
        {
            this.Text = title;
            this.Width = 600;
            this.Height = 480;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // ---- Branch pickers ----
            var lblFrom = new LabelControl { Text = "From Branch:", Location = new System.Drawing.Point(20, 15) };
            CmbFromBranch = new ComboBoxEdit { Location = new System.Drawing.Point(20, 35), Width = 260 };
            CmbFromBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblTo = new LabelControl { Text = "To Branch:", Location = new System.Drawing.Point(300, 15) };
            CmbToBranch = new ComboBoxEdit { Location = new System.Drawing.Point(300, 35), Width = 260 };
            CmbToBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // ---- Add-line row: create controls first, before loading any data ----
            var lblLine = new LabelControl { Text = "Add item:", Location = new System.Drawing.Point(20, 70) };

            CmbVariant = new ComboBoxEdit { Location = new System.Drawing.Point(20, 90), Width = 300 };
            CmbVariant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblQty = new LabelControl { Text = "Qty:", Location = new System.Drawing.Point(330, 70) };
            SpinQty = new SpinEdit { Location = new System.Drawing.Point(330, 90), Width = 80, Value = 1 };
            SpinQty.Properties.MinValue = 1;

            var btnAddLine = new SimpleButton { Text = "Add", Location = new System.Drawing.Point(420, 90), Width = 80 };
            btnAddLine.Click += (s, e) =>
            {
                if (CmbVariant.SelectedIndex < 0)
                {
                    XtraMessageBox.Show("Please select a product first.");
                    return;
                }
                _lines.Add(new TransferLineItem
                {
                    ProductVariantId = _variantIds[CmbVariant.SelectedIndex],
                    ProductDisplay = CmbVariant.Text,
                    Quantity = (decimal)SpinQty.Value
                });
            };

            // ---- Load branches ----
            using (var db = new ClothesShopDBContext())
            {
                foreach (var b in db.Branches.ToList())
                {
                    CmbFromBranch.Properties.Items.Add(b.Name);
                    CmbToBranch.Properties.Items.Add(b.Name);
                    _branchIds.Add(b.Id);
                }
            }

            // Whenever the source branch changes, refresh the item list to only
            // show what's actually available there right now.
            CmbFromBranch.SelectedIndexChanged += (s, e) => LoadAvailableVariants();

            if (_branchIds.Count > 0) CmbFromBranch.SelectedIndex = 0;   // triggers LoadAvailableVariants()
            if (_branchIds.Count > 1) CmbToBranch.SelectedIndex = 1;

            // ---- Lines grid ----
            GridLines = new GridControl { Location = new System.Drawing.Point(20, 125), Size = new System.Drawing.Size(540, 220) };
            GridViewLines = new GridView(GridLines);
            GridLines.MainView = GridViewLines;
            GridLines.DataSource = _lines;
            GridViewLines.OptionsBehavior.Editable = false;

            var btnRemoveLine = new SimpleButton { Text = "Remove Selected Line", Location = new System.Drawing.Point(20, 355), Width = 180 };
            btnRemoveLine.Click += (s, e) =>
            {
                if (GridViewLines.FocusedRowHandle < 0) return;
                var line = GridViewLines.GetFocusedRow() as TransferLineItem;
                if (line != null) _lines.Remove(line);
            };

            var btnSave = new SimpleButton { Text = "Save Transfer", Location = new System.Drawing.Point(340, 400), Width = 120, DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (CmbFromBranch.SelectedIndex == CmbToBranch.SelectedIndex)
                {
                    XtraMessageBox.Show("From and To branches must be different.");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (_lines.Count == 0)
                {
                    XtraMessageBox.Show("Please add at least one item.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(470, 400), Width = 90, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblFrom); this.Controls.Add(CmbFromBranch);
            this.Controls.Add(lblTo); this.Controls.Add(CmbToBranch);
            this.Controls.Add(lblLine);
            this.Controls.Add(CmbVariant); this.Controls.Add(lblQty); this.Controls.Add(SpinQty); this.Controls.Add(btnAddLine);
            this.Controls.Add(GridLines);
            this.Controls.Add(btnRemoveLine);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        // Shows only variants that actually have stock in the currently selected "From" branch.
        private void LoadAvailableVariants()
        {
            CmbVariant.Properties.Items.Clear();
            _variantIds.Clear();

            if (CmbFromBranch.SelectedIndex < 0) return;
            int fromBranchId = _branchIds[CmbFromBranch.SelectedIndex];

            using (var db = new ClothesShopDBContext())
            {
                var available = db.BranchStock
    .Include(x => x.ProductVariant).ThenInclude(v => v.Product).ThenInclude(p => p.Category)
    .Where(x => x.BranchId == fromBranchId
             && x.Quantity > 0
             && x.ProductVariant.IsActive == true
             && x.ProductVariant.Product.IsActive == true
             && x.ProductVariant.Product.Category.IsActive == true)  
    .ToList();

                foreach (var stock in available)
                {
                    CmbVariant.Properties.Items.Add(
                        $"{stock.ProductVariant.Product.Name} - {stock.ProductVariant.Barcode} (Available: {stock.Quantity})");
                    _variantIds.Add(stock.ProductVariantId);
                }
            }

            if (CmbVariant.Properties.Items.Count > 0)
                CmbVariant.SelectedIndex = 0;
        }
    }
}