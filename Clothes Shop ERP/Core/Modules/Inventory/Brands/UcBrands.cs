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
using BrandEntity = Clothes_Shop_ERP.DAL.Brands;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBrands : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBrands()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            Sett.EnableMultiSelect(gridView1);
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            Col.Caption = LocalizationManager.T("Shared_Name");
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
            string name = XtraInputBox.Show(LocalizationManager.T("Brands_NamePrompt"), LocalizationManager.T("Brands_NewTitle"), "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Brands.Add(new BrandEntity { Name = name });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("Brands_EntityName")));
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show(LocalizationManager.T("Brands_EditNamePrompt"), string.Format(LocalizationManager.T("Brands_EditingTitleFmt"), currentName), currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var brand = db.Brands.Where(x => x.Id == id).FirstOrDefault();
                if (brand == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Brands_EntityName"), id)); return; }
                brand.Name = newName;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("Brands_EntityName")));
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
                    var row = db.Brands.FirstOrDefault(x => x.Id == id);
                    if (row != null) db.Brands.Remove(row);
                    db.SaveChanges();
                }
            });
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
            bool canEdit = PermissionManager.CanEdit("Brands");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                if (PermissionManager.CanDelete("Brands"))
                    menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
        }
    }
}
