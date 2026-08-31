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
using BrandEntity = Clothes_Shop_ERP.DAL.Brands;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBrands : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBrands()
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
                gridView1.GridControl.DataSource = db.Brands
                    .Select(x => new { x.Id, x.Name })
                    .ToList();
            }
        }
        private void UcBrands_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            
        }
        private void AddNew()
        {
            string name = XtraInputBox.Show("Brand name:", "New Brand", "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Brands.Add(new BrandEntity { Name = name });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Brand added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show("Enter new brand name:", $"Editing Brand: {currentName}", currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var brand = db.Brands.Where(x => x.Id == id).FirstOrDefault();
                if (brand == null) { Sett.MsgBlue("Error", $"No brand found with Id = {id}"); return; }
                brand.Name = newName;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Brand updated");
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
                    var brand = db.Brands.Where(x => x.Id == id).FirstOrDefault();
                    if (brand == null) { Sett.MsgBlue("Error", $"No brand found with Id = {id}"); return; }
                    db.Brands.Remove(brand);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Brand deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This brand is linked to one or more products. Remove those first.");
            }
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
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }
        }
    }
}
