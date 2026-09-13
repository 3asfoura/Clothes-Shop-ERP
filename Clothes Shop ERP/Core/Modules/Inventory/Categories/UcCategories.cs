using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using CategoryEntity = Clothes_Shop_ERP.DAL.Categories;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcCategories : DevExpress.XtraEditors.XtraUserControl
    {
        public UcCategories()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            Sett.EnableMultiSelect(gridView1);
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColName.Caption = LocalizationManager.T("Shared_Name");
            ColIsActive.Caption = LocalizationManager.T("Shared_IsActive");
        }



        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.Categories
                    .Select(x => new { x.Id, x.Name, x.IsActive })
                    .ToList();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void AddNew()
        {
            string name = XtraInputBox.Show(LocalizationManager.T("Categories_NamePrompt"), LocalizationManager.T("Categories_NewTitle"), "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Categories.Add(new CategoryEntity { Name = name, IsActive = true });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("Categories_EntityName")));
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show(LocalizationManager.T("Categories_EditNamePrompt"), string.Format(LocalizationManager.T("Categories_EditingTitleFmt"), currentName), currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var category = db.Categories.Where(x => x.Id == id).FirstOrDefault();
                if (category == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Categories_EntityName"), id)); return; }
                category.Name = newName;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("Categories_EntityName")));
            GetData();
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
                var category = db.Categories.Where(x => x.Id == id).FirstOrDefault();
                if (category == null) return;
                category.IsActive = !currentStatus;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XActionedPastTense"), LocalizationManager.T("Categories_EntityName"), action.ToLower()));
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
                    var row = db.Categories.FirstOrDefault(x => x.Id == id);
                    if (row != null) db.Categories.Remove(row);
                    db.SaveChanges();
                }
            });
        }

        private void UcCategories_Load(object sender, EventArgs e)
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
            bool canEdit = PermissionManager.CanEdit("Categories");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuActivateDeactivate"), null, (s, ev) => ToggleActive());
                if (PermissionManager.CanDelete("Categories"))
                    menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
        }
    }
}