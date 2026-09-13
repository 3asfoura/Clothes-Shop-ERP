using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockEntity = Clothes_Shop_ERP.DAL.BranchStock;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcStockCount : DevExpress.XtraEditors.XtraUserControl
    {
        public UcStockCount()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            ColQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            ColQuantity.DisplayFormat.FormatString = "0.###";
            Sett.EnableMultiSelect(gridView1);
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColProduct.Caption = LocalizationManager.T("StockCount_ColProduct");
            ColBranch.Caption = LocalizationManager.T("Shared_Branch");
            ColQuantity.Caption = LocalizationManager.T("StockCount_ColQuantity");
            ColMinQuantity.Caption = LocalizationManager.T("StockCount_ColMinQuantity");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.BranchStock
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Include(x => x.Branch)
                    .Select(x => new
                    {
                        x.Id,
                        Product = x.ProductVariant.Product.Name + " - " + x.ProductVariant.Barcode,
                        Branch = x.Branch.Name,
                        x.Quantity,
                        x.MinQuantity
                    })
                    .ToList();
            }
            if (gridView1.Columns["Quantity"] != null)
            {
                gridView1.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
            }
            if (gridView1.Columns["MinQuantity"] != null)
            {
                gridView1.Columns["MinQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["MinQuantity"].DisplayFormat.FormatString = "0.###";
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            // Unused - right-click menus use gridControl1_MouseUp instead.
        }
        private void AddNew()
        {
            var form = new FrmStockCountEdit(LocalizationManager.T("StockCount_NewEntryTitle"), isEditMode: false);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                bool exists = db.BranchStock.Any(s => s.ProductVariantId == form.ProductVariantId && s.BranchId == form.BranchId);
                if (exists) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("StockCount_EntryExists")); return; }

                db.BranchStock.Add(new StockEntity
                {
                    ProductVariantId = form.ProductVariantId,
                    BranchId = form.BranchId,
                    Quantity = form.Quantity,
                    MinQuantity = form.MinQuantity
                });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("StockCount_EntityName")));
            GetData();
        }

        // Applies a whole counting session at once: every item somebody actually
        // counted is set to that quantity, and each change is recorded as a stock
        // movement so the adjustment is traceable later, not a silent overwrite.
        private void ScanSession()
        {
            var form = new FrmStockCountSession(LocalizationManager.T("StockCountSession_Title"));
            if (form.ShowDialog() != DialogResult.OK) return;

            var lines = form.CountedLines.Where(l => l.Counted.Value != l.SystemQty).ToList();
            if (lines.Count == 0)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), LocalizationManager.T("StockCountSession_NoDifferences"));
                return;
            }

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("StockCountSession_ConfirmFmt"), lines.Count),
                LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int branchId = FrmLogin.CurrentBranchId;
            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var line in lines)
                    {
                        decimal counted = line.Counted.Value;

                        var stock = db.BranchStock.FirstOrDefault(s =>
                            s.ProductVariantId == line.ProductVariantId && s.BranchId == branchId);

                        if (stock == null)
                        {
                            db.BranchStock.Add(new StockEntity
                            {
                                ProductVariantId = line.ProductVariantId,
                                BranchId = branchId,
                                Quantity = counted,
                                MinQuantity = 0
                            });
                        }
                        else
                        {
                            stock.Quantity = counted;
                        }

                        db.StockMovements.Add(new Clothes_Shop_ERP.DAL.StockMovements
                        {
                            ProductVariantId = line.ProductVariantId,
                            BranchId = branchId,
                            MovementType = "Manual",
                            // Signed on purpose: negative when the count came up short of
                            // the system, positive when there was more on the shelf.
                            Quantity = counted - line.SystemQty,
                            RefType = "StockCount",
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = FrmLogin.CurrentUserId
                        });
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("StockCountSession_AppliedFmt"), lines.Count));
                    GetData();
                }
                catch (Exception ex)
                {
                    ErrorReporter.Log(ex, "Stock count - apply counting session");
                    transaction.Rollback();
                    Sett.MsgRed(LocalizationManager.T("Shared_Error"), ex.Message);
                }
            }
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            BranchStock current;
            using (var db = new ClothesShopDBContext())
                current = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("StockCount_EntityName"), id)); return; }

            var form = new FrmStockCountEdit(LocalizationManager.T("StockCount_EditQuantityTitle"), isEditMode: true,
                current.ProductVariantId, current.BranchId, current.Quantity, current.MinQuantity);

            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var stock = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();
                stock.Quantity = form.Quantity;
                stock.MinQuantity = form.MinQuantity;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("StockCount_EntityName")));
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
                    var row = db.BranchStock.FirstOrDefault(x => x.Id == id);
                    if (row != null) db.BranchStock.Remove(row);
                    db.SaveChanges();
                }
            });
        }

        private void UcStockCount_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InRow)
                gridView1.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            bool canEdit = PermissionManager.CanEdit("StockCount");
            if (canEdit) menu.Items.Add(LocalizationManager.T("StockCount_MenuScanSession"), null, (s, ev) => ScanSession());
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                if (PermissionManager.CanDelete("StockCount"))
                    menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }

            menu.Show(gridControl1, e.Location);
        }
    }
}
