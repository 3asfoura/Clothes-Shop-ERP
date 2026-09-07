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
            Sett.CenterColumns(gridView2);
            Sett.CenterColumns(dgv_RolesList);
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

            // Deactivating would take away the ability to log in - block it if this
            // is the account currently logged in, or the last active account left
            // (either way, nobody would be able to sign in afterwards).
            if (currentStatus)
            {
                if (id == FrmLogin.CurrentUserId)
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_CannotDeactivateSelf"));
                    return;
                }
                using (var db = new ClothesShopDBContext())
                {
                    if (db.Users.Count(u => u.IsActive == true) <= 1)
                    {
                        Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_CannotDeactivateLastUser"));
                        return;
                    }
                }
            }

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

            if (id == FrmLogin.CurrentUserId)
            {
                Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_CannotDeleteSelf"));
                return;
            }
            using (var db = new ClothesShopDBContext())
            {
                bool isActive = db.Users.Where(u => u.Id == id).Select(u => u.IsActive).FirstOrDefault() == true;
                if (isActive && db.Users.Count(u => u.IsActive == true) <= 1)
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("UsersRoles_CannotDeactivateLastUser"));
                    return;
                }
            }

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
            var form = new FrmRoleEdit(LocalizationManager.T("Roles_NewTitle"), "", new System.Collections.Generic.Dictionary<string, string>());
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var role = new Roles { Name = form.RoleName };
                db.Roles.Add(role);
                db.SaveChanges();

                foreach (var row in form.ScreenPermissions)
                {
                    db.RolePermissions.Add(new RolePermissions
                    {
                        RoleId = role.Id,
                        ScreenName = row.ScreenName,
                        PermissionLevel = row.PermissionLevel
                    });
                }
                db.SaveChanges();
            }

            Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("UsersRoles_RoleEntityName")));
            GetDataRoles();
        }

        // The role with the lowest Id always has full access regardless of what's
        // configured (see PermissionManager.Load) - it can't be deleted or have its
        // permissions edited, so nobody accidentally locks every admin out.
        private int GetFirstRoleId()
        {
            using (var db = new ClothesShopDBContext())
                return db.Roles.OrderBy(r => r.Id).Select(r => r.Id).First();
        }

        private void EditSelectedRoles()
        {
            if (dgv_RolesList.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(dgv_RolesList.GetFocusedRowCellValue("Id"));
            string currentName = dgv_RolesList.GetFocusedRowCellValue("Name").ToString();
            bool isProtected = id == GetFirstRoleId();

            System.Collections.Generic.Dictionary<string, string> existingLevels;
            using (var db = new ClothesShopDBContext())
            {
                existingLevels = db.RolePermissions
                    .Where(x => x.RoleId == id)
                    .ToDictionary(x => x.ScreenName, x => x.PermissionLevel);
            }

            var form = new FrmRoleEdit(string.Format(LocalizationManager.T("Roles_EditingTitleFmt"), currentName), currentName, existingLevels, isProtected);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var role = db.Roles.Where(r => r.Id == id).FirstOrDefault();

                if (role == null)
                {
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("UsersRoles_RoleEntityName"), id));
                    return;
                }

                role.Name = form.RoleName;

                // The protected role's permission grid is locked/forced to "Write" in
                // the dialog since it always has full access anyway - nothing to save.
                if (!isProtected)
                {
                    var currentRows = db.RolePermissions.Where(x => x.RoleId == id).ToDictionary(x => x.ScreenName, x => x);
                    foreach (var row in form.ScreenPermissions)
                    {
                        if (currentRows.TryGetValue(row.ScreenName, out var existing))
                            existing.PermissionLevel = row.PermissionLevel;
                        else
                            db.RolePermissions.Add(new RolePermissions
                            {
                                RoleId = id,
                                ScreenName = row.ScreenName,
                                PermissionLevel = row.PermissionLevel
                            });
                    }
                }

                db.SaveChanges();
            }

            // If the edited role is the one currently logged in, the sidebar/screen
            // access needs the freshly-saved permissions right away.
            if (id == FrmLogin.CurrentRoleId)
                PermissionManager.Load(id);

            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("UsersRoles_RoleEntityName")));
            GetDataRoles();
        }

        private void DeleteSelectedRoles()
        {
            if (dgv_RolesList.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(dgv_RolesList.GetFocusedRowCellValue("Id"));
            string name = dgv_RolesList.GetFocusedRowCellValue("Name").ToString();

            if (id == GetFirstRoleId())
            {
                Sett.MsgRed(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Roles_CannotDeleteProtected"));
                return;
            }

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
            bool canEdit = PermissionManager.CanEdit("UsersRoles");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNewUsers());

            if (hit.InRow && canEdit)
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
            bool canEdit = PermissionManager.CanEdit("UsersRoles");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNewRoles());
            menu.Show(dgv_Roles, e.Location);

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelectedRoles());

                int focusedRoleId = Convert.ToInt32(dgv_RolesList.GetFocusedRowCellValue("Id"));
                if (focusedRoleId != GetFirstRoleId())
                    menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelectedRoles());
            }
        }
    }
}
