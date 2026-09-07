using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using Clothes_Shop_ERP.Resources;
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
using BranchEntity = Clothes_Shop_ERP.DAL.Branches;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBranches : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBranches()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColName.Caption = LocalizationManager.T("Shared_Name");
            ColAddress.Caption = LocalizationManager.T("Shared_Address");
            ColPhone.Caption = LocalizationManager.T("Shared_Phone");
            ColIsActive.Caption = LocalizationManager.T("Shared_IsActive");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridControl1.DataSource = db.Branches
                    //     .Select(x => new { x.Id , x.Name })
                    .ToList();
            }
        }
       
        private void UcBranches_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void AddNew()
        {
            var form = new FrmBranchEdit(LocalizationManager.T("Branches_NewTitle"));

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ClothesShopDBContext())
                {
                    db.Branches.Add(new BranchEntity
                    {
                        Name = form.BranchName,
                        Address = form.BranchAddress,
                        Phone = form.BranchPhone,
                        IsActive = true
                    });
                    db.SaveChanges();
                }

                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("Branches_EntityName")));
                GetData();
            }
        }
        private void ToggleActive()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();
            bool currentStatus = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));

            string action = currentStatus ? LocalizationManager.T("Shared_Deactivate") : LocalizationManager.T("Shared_Activate");

            if (currentStatus)
            {
                using (var db = new ClothesShopDBContext())
                {
                    if (db.Branches.Count(b => b.IsActive == true) <= 1)
                    {
                        Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Branches_CannotDeactivateLast"));
                        return;
                    }
                }
            }

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmAction"), action, name), LocalizationManager.T("Common_ConfirmTitle"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();
                if (branch == null) return;

                branch.IsActive = !currentStatus;
                db.SaveChanges();
            }

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XActionedPastTense"), LocalizationManager.T("Branches_EntityName"), action.ToLower()));
            GetData();
        }
        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            string currentAddress = gridView1.GetFocusedRowCellValue("Address")?.ToString() ?? "";
            string currentPhone = gridView1.GetFocusedRowCellValue("Phone")?.ToString() ?? "";

            var form = new FrmBranchEdit(string.Format(LocalizationManager.T("Branches_EditingTitleFmt"), currentName), currentName, currentAddress, currentPhone);

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ClothesShopDBContext())
                {
                    var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();

                    if (branch == null)
                    {
                        Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Branches_EntityName"), id));
                        return;
                    }

                    branch.Name = form.BranchName;
                    branch.Address = form.BranchAddress;
                    branch.Phone = form.BranchPhone;
                    db.SaveChanges();
                }

                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("Branches_EntityName")));
                GetData();
            }
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            using (var db = new ClothesShopDBContext())
            {
                if (db.Branches.Count() <= 1)
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Branches_CannotDeactivateLast"));
                    return;
                }
            }

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), name), LocalizationManager.T("Common_ConfirmTitle"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();

                    if (branch == null)
                    {
                        Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Branches_EntityName"), id));
                        return;
                    }

                    db.Branches.Remove(branch);
                    db.SaveChanges();
                }

                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("Branches_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Branches_HasRelatedData"));
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            
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
            bool canEdit = PermissionManager.CanEdit("Branches");
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
