using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
using UserEntity = Clothes_Shop_ERP.DAL.Users;
namespace Clothes_Shop_ERP.modlestore.Settings.Users
{
    public partial class UcUsers_Roles : DevExpress.XtraEditors.XtraUserControl
    {
        public UcUsers_Roles()
        {
            InitializeComponent();
            GetDataUsers();
            GetDataRoles();
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsCustomization.AllowSort = false;
            dgv_RolesList.OptionsView.ShowGroupPanel = false;
            dgv_RolesList.OptionsCustomization.AllowSort = false;
            gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgv_RolesList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            groupControl3.Text = LocalizationManager.T("UsersRoles_Users");
            groupControl1.Text = LocalizationManager.T("UsersRoles_Roles");
            ColUsername.Caption = LocalizationManager.T("UsersRoles_ColUsername");
            ColFullName.Caption = LocalizationManager.T("UsersRoles_ColFullName");
            ColRoleName.Caption = LocalizationManager.T("UsersRoles_ColRoleName");
            ColIsActive.Caption = LocalizationManager.T("Shared_IsActive");
            Col_Role.Caption = LocalizationManager.T("Shared_Name");
        }
        public void GetDataRoles()
        {
            using (var db = new ClothesShopDBContext())
            {
                dgv_Roles.DataSource = db.Roles
                    //     .Select(x => new { x.Id , x.Name })
                    .ToList();
            }
        }
        public void GetDataUsers()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView2.GridControl.DataSource = db.Users
                    .Include(u => u.Role)
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        u.FullName,
                        RoleName = u.Role.Name,
                        u.IsActive
                    })
                    .ToList();
            }
        }
        private void AddNewUsers()
        {
            var form = new FrmUserEdit(LocalizationManager.T("UsersRoles_NewUserTitle"), isEditMode: false);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                bool usernameTaken = db.Users.Any(u => u.Username == form.Username);
                if (usernameTaken)
                {
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("UsersRoles_UsernameTaken"));
                    return;
                }

                db.Users.Add(new UserEntity
                {
                    Username = form.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(form.Password),
                    FullName = form.FullName,
                    RoleId = form.RoleId,
                    IsActive = form.IsActive
                });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("UsersRoles_UserEntityName")));
            GetDataUsers();
        }

        private void EditSelectedUsers()
        {
            if (gridView2.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Id"));
            string currentUsername = gridView2.GetFocusedRowCellValue("Username").ToString();
            string currentFullName = gridView2.GetFocusedRowCellValue("FullName").ToString();

            int currentRoleId;
            using (var db = new ClothesShopDBContext())
                currentRoleId = db.Users.Where(u => u.Id == id).Select(u => u.RoleId).FirstOrDefault();

            var form = new FrmUserEdit(string.Format(LocalizationManager.T("UsersRoles_EditingUserTitleFmt"), currentUsername), isEditMode: true, currentUsername, currentFullName, currentRoleId);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("UsersRoles_UserEntityName"), id)); return; }

                user.FullName = form.FullName;
                user.RoleId = form.RoleId;
                user.IsActive = form.IsActive;

                if (!string.IsNullOrWhiteSpace(form.Password))
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(form.Password);

                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("UsersRoles_UserEntityName")));
            GetDataUsers();
        }

        private void ToggleActiveUsers()
        {
            if (gridView2.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Id"));
            string username = gridView2.GetFocusedRowCellValue("Username").ToString();
            bool currentStatus = Convert.ToBoolean(gridView2.GetFocusedRowCellValue("IsActive"));
            string action = currentStatus ? LocalizationManager.T("Shared_Deactivate") : LocalizationManager.T("Shared_Activate");

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmAction"), action, username), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null) return;
                user.IsActive = !currentStatus;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XActionedPastTense"), LocalizationManager.T("UsersRoles_UserEntityName"), action.ToLower()));
            GetDataUsers();
        }

        private void DeleteSelectedUsers()
        {
            if (gridView2.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Id"));
            string username = gridView2.GetFocusedRowCellValue("Username").ToString();

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), username), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                    if (user == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("UsersRoles_UserEntityName"), id)); return; }
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("UsersRoles_UserEntityName")));
                GetDataUsers();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_UserHasRecords"));
            }
        }
        private void AddNewRoles()
        {
            string name = XtraInputBox.Show(LocalizationManager.T("Roles_NamePrompt"), LocalizationManager.T("Roles_NewTitle"), "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Roles.Add(new Roles { Name = name });
                db.SaveChanges();
            }

            Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("UsersRoles_RoleEntityName")));
            GetDataRoles();
        }

        private void EditSelectedRoles()
        {
            if (dgv_RolesList.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(dgv_RolesList.GetFocusedRowCellValue("Id"));
            string currentName = dgv_RolesList.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show(LocalizationManager.T("Roles_EditNamePrompt"), string.Format(LocalizationManager.T("Roles_EditingTitleFmt"), currentName), currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var role = db.Roles.Where(r => r.Id == id).FirstOrDefault();

                if (role == null)
                {
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("UsersRoles_RoleEntityName"), id));
                    return;
                }

                role.Name = newName;
                db.SaveChanges();
            }

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("UsersRoles_RoleEntityName")));
            GetDataRoles();
        }

        private void DeleteSelectedRoles()
        {
            if (dgv_RolesList.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(dgv_RolesList.GetFocusedRowCellValue("Id"));
            string name = dgv_RolesList.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), name), LocalizationManager.T("Common_ConfirmTitle"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var role = db.Roles.Where(r => r.Id == id).FirstOrDefault();

                    if (role == null)
                    {
                        Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("UsersRoles_RoleEntityName"), id));
                        return;
                    }

                    db.Roles.Remove(role);
                    db.SaveChanges();
                }

                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("UsersRoles_RoleEntityName")));
                GetDataRoles();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_RoleAssigned"));
            }
        }
        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = gridView2.CalcHitInfo(e.Location);
            if (hit.InRow) gridView2.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;

            var menu = new ContextMenuStrip();
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNewUsers());

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelectedUsers());
                menu.Items.Add(LocalizationManager.T("Shared_MenuActivateDeactivate"), null, (s, ev) => ToggleActiveUsers());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelectedUsers());
            }

            menu.Show(gridControl2, e.Location);
        }

        private void dgv_Roles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = dgv_RolesList.CalcHitInfo(e.Location);
            if (hit.InRow)
                dgv_RolesList.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNewRoles());
            menu.Show(dgv_Roles, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelectedRoles());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelectedRoles());
            }
        }
    }
}
