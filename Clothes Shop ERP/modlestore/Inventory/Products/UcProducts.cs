using Clothes_Shop_ERP.DAL;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductEntity = Clothes_Shop_ERP.DAL.Products;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcProducts : DevExpress.XtraEditors.XtraUserControl
    {
        public UcProducts()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.Products
                    .Select(x => new { x.Id, x.Code, x.Name, x.BasePrice, x.IsActive })
                    .ToList();
            }
        }

        private void UcProducts_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
           
        }

        private void AddNew()
        {
            var form = new FrmProductEdit("New Product");
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Products.Add(new ProductEntity
                {
                    Code = form.Code,
                    Name = form.ProductName,
                    BasePrice = form.BasePrice,
                    IsActive = form.IsActive,
                    CategoryId = form.CategoryId,
                    BrandId = form.BrandId
                });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Product added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            Products current;
            using (var db = new ClothesShopDBContext())
                current = db.Products.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue("Error", $"No product found with Id = {id}"); return; }

            var form = new FrmProductEdit($"Editing: {current.Name}", current.Code, current.Name,
             current.BasePrice, current.IsActive ?? true, current.CategoryId, current.BrandId);


            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                product.Code = form.Code;
                product.Name = form.ProductName;
                product.BasePrice = form.BasePrice;
                product.IsActive = form.IsActive;
                product.CategoryId = form.CategoryId;
                product.BrandId = form.BrandId;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Product updated");
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show($"Delete '{name}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                    if (product != null) db.Products.Remove(product);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Product deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This product has variants or sales linked to it. Deactivate instead.");
            }
        }

        private void ToggleActive()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();
            bool currentStatus = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));
            string action = currentStatus ? "Deactivate" : "Activate";

            if (XtraMessageBox.Show($"{action} '{name}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                if (product == null) return;
                product.IsActive = !currentStatus;
                db.SaveChanges();
            }

            Sett.MsgBlue("Success", $"Product {action.ToLower()}d");
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
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected());
                menu.Items.Add("Activate/Deactivate", null, (s, ev) => ToggleActive());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }
        }
    }
}
