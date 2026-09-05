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
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColCode.Caption = LocalizationManager.T("Shared_Code");
            ColName.Caption = LocalizationManager.T("Shared_Name");
            ColBasePrice.Caption = LocalizationManager.T("Products_ColBasePrice");
            Col.Caption = LocalizationManager.T("Shared_IsActive");
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
            var form = new FrmProductEdit(LocalizationManager.T("Products_NewTitle"));
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
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("Products_EntityName")));
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            Products current;
            using (var db = new ClothesShopDBContext())
                current = db.Products.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Products_EntityName"), id)); return; }

            var form = new FrmProductEdit(string.Format(LocalizationManager.T("Products_EditingTitleFmt"), current.Name), current.Code, current.Name,
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
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("Products_EntityName")));
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), name), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                    if (product != null) db.Products.Remove(product);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("Products_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Products_HasVariantsOrSales"));
            }
        }

        private void ToggleActive()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();
            bool currentStatus = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));
            string action = currentStatus ? LocalizationManager.T("Shared_Deactivate") : LocalizationManager.T("Shared_Activate");

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmAction"), action, name), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                if (product == null) return;
                product.IsActive = !currentStatus;
                db.SaveChanges();
            }

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XActionedPastTense"), LocalizationManager.T("Products_EntityName"), action.ToLower()));
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
            bool canEdit = PermissionManager.CanEdit("Products");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuActivateDeactivate"), null, (s, ev) => ToggleActive());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
        }
    }
}
