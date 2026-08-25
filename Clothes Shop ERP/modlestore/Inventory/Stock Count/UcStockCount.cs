using Clothes_Shop_ERP.DAL;
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
            var form = new FrmStockCountEdit("New Stock Entry", isEditMode: false);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                bool exists = db.BranchStock.Any(s => s.ProductVariantId == form.ProductVariantId && s.BranchId == form.BranchId);
                if (exists) { Sett.MsgBlue("Error", "This variant already has a stock entry for this branch. Edit it instead."); return; }

                db.BranchStock.Add(new StockEntity
                {
                    ProductVariantId = form.ProductVariantId,
                    BranchId = form.BranchId,
                    Quantity = form.Quantity,
                    MinQuantity = form.MinQuantity
                });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Stock entry added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            BranchStock current;
            using (var db = new ClothesShopDBContext())
                current = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue("Error", $"No stock entry found with Id = {id}"); return; }

            var form = new FrmStockCountEdit("Edit Stock Quantity", isEditMode: true,
                current.ProductVariantId, current.BranchId, current.Quantity, current.MinQuantity);

            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var stock = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();
                stock.Quantity = form.Quantity;
                stock.MinQuantity = form.MinQuantity;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Stock entry updated");
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            if (XtraMessageBox.Show("Delete this stock entry?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var stock = db.BranchStock.Where(x => x.Id == id).FirstOrDefault();
                if (stock != null) db.BranchStock.Remove(stock);
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Stock entry deleted");
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
            menu.Items.Add("New", null, (s, ev) => AddNew());

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }

            menu.Show(gridControl1, e.Location);
        }
    }
}
