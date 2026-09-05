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
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            //if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Row) return;
            //if (e.HitInfo == null) return;
            //if (e.HitInfo.InColumn) return;
            //if (e.HitInfo.InRow) gridView1.FocusedRowHandle = e.HitInfo.RowHandle;

            //e.Menu.Items.Clear();
            //e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("New", (s, ev) => AddNew()));
            //if (e.HitInfo.InRow)
            //{
            //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Edit", (s, ev) => EditSelected()));
            //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Delete", (s, ev) => DeleteSelected()));
            //}
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

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            if (XtraMessageBox.Show(LocalizationManager.T("StockCount_ConfirmDeleteEntry"), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var stock = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();
                if (stock != null) db.BranchStock.Remove(stock);
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("StockCount_EntityName")));
            GetData();
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
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }

            menu.Show(gridControl1, e.Location);
        }
    }
}
