using Clothes_Shop_ERP.DAL;
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
            var form = new FrmVariantEdit("New Variant");
            if (form.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    bool barcodeTaken = db.ProductVariants.Any(v => v.Barcode == form.Barcode);
                    if (barcodeTaken) { Sett.MsgBlue("Error", "This barcode is already used."); return; }

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
                Sett.MsgBlue("Success", "Variant added");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Error", "This exact combination (same product, color, and size) already exists.");
            }
        }
        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            ProductVariants current;
            using (var db = new ClothesShopDBContext())
                current = db.ProductVariants.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue("Error", $"No variant found with Id = {id}"); return; }

            var form = new FrmVariantEdit($"Editing: {current.Barcode}", current.Barcode, current.SalePrice,
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
            Sett.MsgBlue("Success", "Variant updated");
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();

            if (XtraMessageBox.Show($"Delete '{barcode}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var variant = db.ProductVariants.Where(x => x.Id == id).FirstOrDefault();
                    if (variant != null) db.ProductVariants.Remove(variant);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Variant deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This variant has stock or sales linked to it. Deactivate instead.");
            }
        }

        private void UcProductVariants_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InRow)
                gridView1.FocusedRowHandle = hit.RowHandle;

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
