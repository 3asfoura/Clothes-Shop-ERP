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
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), barcode), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var variant = db.ProductVariants.Where(x => x.Id == id).FirstOrDefault();
                    if (variant != null) db.ProductVariants.Remove(variant);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("ProductVariants_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("ProductVariants_HasStockOrSales"));
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
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
        }
    }
}
