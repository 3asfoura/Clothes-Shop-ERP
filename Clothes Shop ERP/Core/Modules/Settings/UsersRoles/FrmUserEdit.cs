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
using UserEntity = Clothes_Shop_ERP.DAL.Users;
namespace Clothes_Shop_ERP
{
    public partial class FrmUserEdit : DevExpress.XtraEditors.XtraForm
    {
        public string Username => TxtUsername.Text.Trim();
        public string Password => TxtPassword.Text;                 // empty on edit = keep old password
        public string FullName => TxtFullName.Text.Trim();
        public int RoleId => _roleIds[CmbRole.SelectedIndex];
        public bool IsActive => ChkIsActive.Checked;

        private TextEdit TxtUsername;
        private TextEdit TxtPassword;
        private TextEdit TxtFullName;
        private ComboBoxEdit CmbRole;
        private CheckEdit ChkIsActive;
        private List<int> _roleIds = new List<int>();
        private bool _isEditMode;
        public FrmUserEdit()
        {
            InitializeComponent();
        }
        public FrmUserEdit(string title, bool isEditMode, string username = "", string fullName = "", int currentRoleId = 0, bool isActive = true)
        {
            _isEditMode = isEditMode;

            this.Text = title;
            this.Width = 380;
            this.Height = 330;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblUsername = new LabelControl { Text = LocalizationManager.T("FrmUserEdit_Username"), Location = new System.Drawing.Point(20, 20) };
            TxtUsername = new TextEdit { Text = username, Location = new System.Drawing.Point(20, 40), Width = 320 };
            TxtUsername.Enabled = !isEditMode;   // don't allow changing the username once created

            var lblPassword = new LabelControl
            {
                Text = isEditMode ? LocalizationManager.T("FrmUserEdit_NewPasswordHint") : LocalizationManager.T("FrmUserEdit_Password"),
                Location = new System.Drawing.Point(20, 75)
            };
            TxtPassword = new TextEdit { Location = new System.Drawing.Point(20, 95), Width = 320 };
            TxtPassword.Properties.UseSystemPasswordChar = true;

            var lblFullName = new LabelControl { Text = LocalizationManager.T("FrmUserEdit_FullName"), Location = new System.Drawing.Point(20, 130) };
            TxtFullName = new TextEdit { Text = fullName, Location = new System.Drawing.Point(20, 150), Width = 320 };

            var lblRole = new LabelControl { Text = LocalizationManager.T("FrmUserEdit_Role"), Location = new System.Drawing.Point(20, 185) };
            CmbRole = new ComboBoxEdit { Location = new System.Drawing.Point(20, 205), Width = 320 };
            CmbRole.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            using (var db = new ClothesShopDBContext())
            {
                var roles = db.Roles.ToList();
                foreach (var r in roles)
                {
                    CmbRole.Properties.Items.Add(r.Name);
                    _roleIds.Add(r.Id);
                }
            }
            int idx = _roleIds.IndexOf(currentRoleId);
            CmbRole.SelectedIndex = idx >= 0 ? idx : 0;

            ChkIsActive = new CheckEdit { Text = LocalizationManager.T("Shared_Active"), Checked = isActive, Location = new System.Drawing.Point(20, 240) };

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shared_BtnSave"), Location = new System.Drawing.Point(160, 270), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtUsername.Text) || string.IsNullOrWhiteSpace(TxtFullName.Text))
                {
                    XtraMessageBox.Show(LocalizationManager.T("UsersRoles_FillUsernameFullName"));
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (!_isEditMode && string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    XtraMessageBox.Show(LocalizationManager.T("UsersRoles_PasswordRequiredForNewUser"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(240, 270), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblUsername);
            this.Controls.Add(TxtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(TxtPassword);
            this.Controls.Add(lblFullName);
            this.Controls.Add(TxtFullName);
            this.Controls.Add(lblRole);
            this.Controls.Add(CmbRole);
            this.Controls.Add(ChkIsActive);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}
