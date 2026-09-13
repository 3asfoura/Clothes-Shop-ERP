using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VariantEntity = Clothes_Shop_ERP.DAL.ProductVariants;

namespace Clothes_Shop_ERP
{
    public partial class UcProductVariants : DevExpress.XtraEditors.XtraUserControl
    {
        public UcProductVariants()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            // Ctrl/Shift+click to pick several rows (any mix of products), so labels
            // can be printed for just a handful of specific items - see PrintSelectedLabels.
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColProductName.Caption = LocalizationManager.T("ProductVariants_ColProductName");
            ColColor.Caption = LocalizationManager.T("Shared_Color");
            ColSize.Caption = LocalizationManager.T("Shared_Size");
            ColBarcode.Caption = LocalizationManager.T("ProductVariants_ColBarcode");
            ColSalePrice.Caption = LocalizationManager.T("ProductVariants_ColSalePrice");
            ColCostPrice.Caption = LocalizationManager.T("ProductVariants_ColCostPrice");
            ColIsActive.Caption = LocalizationManager.T("Shared_IsActive");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.ProductVariants
                    .Include(x => x.Product)
                    .Include(x => x.Color)
                    .Include(x => x.Size)
                    .Select(x => new
                    {
                        x.Id,
                        ProductName = x.Product.Name,
                        Color = x.Color.Name,
                        Size = x.Size.Name,
                        x.Barcode,
                        x.SalePrice,
                        x.CostPrice,
                        x.IsActive
                    })
                    .ToList();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void AddNew()
        {
            var form = new FrmVariantEdit(LocalizationManager.T("ProductVariants_NewTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    bool barcodeTaken = db.ProductVariants.Any(v => v.Barcode == form.Barcode);
                    if (barcodeTaken) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("ProductVariants_BarcodeUsed")); return; }

                    db.ProductVariants.Add(new VariantEntity
                    {
                        ProductId = form.ProductId,
                        ColorId = form.ColorId,
                        SizeId = form.SizeId,
                        Barcode = form.Barcode,
                        SalePrice = form.SalePrice,
                        CostPrice = form.CostPrice,
                        IsActive = form.IsActive
                    });
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("ProductVariants_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("ProductVariants_CombinationExists"));
            }
        }
        // One (color, size) combination that already exists for the product is
        // silently skipped rather than raising the DbUpdateException AddNew() relies
        // on - with dozens of combinations generated at once, hitting even one
        // duplicate would otherwise abort the whole batch instead of just that pair.
        private void BulkAddVariants()
        {
            var form = new FrmBulkVariantAdd(LocalizationManager.T("BulkVariant_Title"));
            if (form.ShowDialog() != DialogResult.OK) return;

            int created = 0, skipped = 0;
            using (var db = new ClothesShopDBContext())
            {
                var existingCombos = new HashSet<(int ColorId, int SizeId)>(
                    db.ProductVariants
                        .Where(v => v.ProductId == form.ProductId)
                        .Select(v => new { v.ColorId, v.SizeId })
                        .ToList()
                        .Select(v => (v.ColorId, v.SizeId)));

                // Reserved as one increasing block in memory instead of re-querying the
                // max barcode after each insert - SaveChanges only runs once at the end,
                // so the DB's own max wouldn't advance between iterations anyway.
                long nextBarcode = db.ProductVariants
                    .Select(v => v.Barcode)
                    .AsEnumerable()
                    .Select(b => long.TryParse(b, out long n) ? n : 0)
                    .DefaultIfEmpty(0)
                    .Max();
                nextBarcode = nextBarcode == 0 ? 1000001 : nextBarcode + 1;

                foreach (int colorId in form.SelectedColorIds)
                {
                    foreach (int sizeId in form.SelectedSizeIds)
                    {
                        if (existingCombos.Contains((colorId, sizeId))) { skipped++; continue; }

                        db.ProductVariants.Add(new VariantEntity
                        {
                            ProductId = form.ProductId,
                            ColorId = colorId,
                            SizeId = sizeId,
                            Barcode = nextBarcode.ToString(),
                            SalePrice = form.SalePrice,
                            CostPrice = form.CostPrice,
                            IsActive = true
                        });
                        nextBarcode++;
                        created++;
                    }
                }
                db.SaveChanges();
            }

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("BulkVariant_ResultFmt"), created, skipped));
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            ProductVariants current;
            using (var db = new ClothesShopDBContext())
                current = db.ProductVariants.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("ProductVariants_EntityName"), id)); return; }

            var form = new FrmVariantEdit(string.Format(LocalizationManager.T("ProductVariants_EditingTitleFmt"), current.Barcode), current.Barcode, current.SalePrice,
                current.CostPrice, current.IsActive ?? true, current.ProductId, current.ColorId, current.SizeId);

            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var variant = db.ProductVariants.Where(x => x.Id == id).FirstOrDefault();
                variant.ProductId = form.ProductId;
                variant.ColorId = form.ColorId;
                variant.SizeId = form.SizeId;
                variant.Barcode = form.Barcode;
                variant.SalePrice = form.SalePrice;
                variant.CostPrice = form.CostPrice;
                variant.IsActive = form.IsActive;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("ProductVariants_EntityName")));
            GetData();
        }

        // One confirmation for the whole selection (Ctrl/Shift+click to pick several),
        // and a row the database refuses because it is still referenced elsewhere is
        // reported at the end instead of stopping the rest of the batch.
        private void DeleteSelected()
        {
            Sett.DeleteSelectedRows(gridView1, GetData, id =>
            {
                using (var db = new ClothesShopDBContext())
                {
                    var row = db.ProductVariants.FirstOrDefault(x => x.Id == id);
                    if (row != null) db.ProductVariants.Remove(row);
                    db.SaveChanges();
                }
            });
        }

        private void UcProductVariants_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void PrintBarcodeLabel()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            BarcodeLabelData label;
            using (var db = new ClothesShopDBContext())
            {
                var v = db.ProductVariants.Include(x => x.Product).Include(x => x.Color).Include(x => x.Size)
                    .FirstOrDefault(x => x.Id == id);
                if (v == null) return;

                label = new BarcodeLabelData
                {
                    ProductName = v.Product.Name,
                    VariantInfo = $"{v.Color.Name} - {v.Size.Name}",
                    Barcode = v.Barcode,
                    Price = v.SalePrice
                };
            }

            string qtyText = XtraInputBox.Show(LocalizationManager.T("ProductVariants_LabelQtyPrompt"), LocalizationManager.T("ProductVariants_PrintLabelTitle"), "1");
            if (string.IsNullOrWhiteSpace(qtyText)) return;
            if (!int.TryParse(qtyText, out int qty) || qty < 1) qty = 1;

            BarcodeLabelPrinter.Preview(label, qty);
        }

        // Prints labels for exactly the rows the user Ctrl/Shift-clicked - any mix of
        // products/variants, not necessarily a whole product's worth (that's what
        // PrintAllLabelsForProduct is for). One combined print job either way.
        private void PrintSelectedLabels()
        {
            int[] handles = gridView1.GetSelectedRows();
            if (handles.Length == 0) return;

            var ids = handles
                .Where(h => h >= 0)
                .Select(h => Convert.ToInt32(gridView1.GetRowCellValue(h, "Id")))
                .Distinct()
                .ToList();
            if (ids.Count == 0) return;

            List<BarcodeLabelData> labels;
            using (var db = new ClothesShopDBContext())
            {
                labels = db.ProductVariants
                    .Include(x => x.Product).Include(x => x.Color).Include(x => x.Size)
                    .Where(v => ids.Contains(v.Id))
                    .ToList()
                    .Select(v => new BarcodeLabelData
                    {
                        ProductName = v.Product.Name,
                        VariantInfo = $"{v.Color.Name} - {v.Size.Name}",
                        Barcode = v.Barcode,
                        Price = v.SalePrice
                    })
                    .ToList();
            }
            if (labels.Count == 0) return;

            string qtyText = XtraInputBox.Show(LocalizationManager.T("ProductVariants_LabelQtyPrompt"), LocalizationManager.T("BulkVariant_PrintSelectedTitle"), "1");
            if (string.IsNullOrWhiteSpace(qtyText)) return;
            if (!int.TryParse(qtyText, out int qty) || qty < 1) qty = 1;

            int totalLabels = labels.Count * qty;
            if (XtraMessageBox.Show(
                string.Format(LocalizationManager.T("BulkVariant_ConfirmPrintSelectedFmt"), labels.Count, qty, totalLabels),
                LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            BarcodeLabelPrinter.PrintBatch(labels.Select(l => (Data: l, Quantity: qty)));
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("BulkVariant_PrintedAllFmt"), totalLabels));
        }

        // Prints one label per variant of the same product as the focused row, in one
        // go instead of right-clicking + previewing + printing each row separately.
        // Skips the per-item PrintPreviewDialog Preview() uses (fine for one label,
        // but would mean clicking through N preview windows in a row here) and sends
        // straight to the printer - the confirmation below is the only checkpoint,
        // since this uses real label stock.
        private void PrintAllLabelsForProduct()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            int productId;
            string productName;
            List<BarcodeLabelData> labels;
            using (var db = new ClothesShopDBContext())
            {
                var focused = db.ProductVariants.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
                if (focused == null) return;
                productId = focused.ProductId;
                productName = focused.Product.Name;

                labels = db.ProductVariants
                    .Include(x => x.Product).Include(x => x.Color).Include(x => x.Size)
                    .Where(v => v.ProductId == productId && v.IsActive == true)
                    .ToList()
                    .Select(v => new BarcodeLabelData
                    {
                        ProductName = v.Product.Name,
                        VariantInfo = $"{v.Color.Name} - {v.Size.Name}",
                        Barcode = v.Barcode,
                        Price = v.SalePrice
                    })
                    .ToList();
            }

            if (labels.Count == 0) return;

            string qtyText = XtraInputBox.Show(LocalizationManager.T("ProductVariants_LabelQtyPrompt"), LocalizationManager.T("BulkVariant_PrintAllLabelsTitle"), "1");
            if (string.IsNullOrWhiteSpace(qtyText)) return;
            if (!int.TryParse(qtyText, out int qty) || qty < 1) qty = 1;

            int totalLabels = labels.Count * qty;
            if (XtraMessageBox.Show(
                string.Format(LocalizationManager.T("BulkVariant_ConfirmPrintAllFmt"), labels.Count, qty, totalLabels, productName),
                LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // One combined print job for the whole batch, not one job per variant -
            // looping Print() per item here previously meant a separate "Save Print
            // Output As" dialog for every single variant on a machine whose default
            // printer is a virtual PDF printer (confirmed in the field).
            BarcodeLabelPrinter.PrintBatch(labels.Select(l => (Data: l, Quantity: qty)));

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("BulkVariant_PrintedAllFmt"), totalLabels));
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = gridView1.CalcHitInfo(e.Location);
            // Only move focus (and with it, the selection) to the clicked row if it
            // wasn't already part of a multi-selection - right-clicking inside an
            // existing Ctrl/Shift-click selection should keep that selection intact,
            // the same way Windows Explorer's own multi-select context menu does,
            // rather than collapsing it down to just the row under the cursor.
            if (hit.InRow && !gridView1.GetSelectedRows().Contains(hit.RowHandle))
                gridView1.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            bool canEdit = PermissionManager.CanEdit("ProductVariants");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            if (canEdit) menu.Items.Add(LocalizationManager.T("BulkVariant_MenuBulkAdd"), null, (s, ev) => BulkAddVariants());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("ProductVariants_MenuPrintLabel"), null, (s, ev) => PrintBarcodeLabel());
                menu.Items.Add(LocalizationManager.T("BulkVariant_MenuPrintSelected"), null, (s, ev) => PrintSelectedLabels());
                menu.Items.Add(LocalizationManager.T("BulkVariant_MenuPrintAllLabels"), null, (s, ev) => PrintAllLabelsForProduct());
                if (canEdit)
                {
                    menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                    if (PermissionManager.CanDelete("ProductVariants"))
                        menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
                }
            }
        }
    }
}
