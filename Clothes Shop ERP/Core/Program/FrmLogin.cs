using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Clothes_Shop_ERP
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        public static int CurrentUserId;
        public static string CurrentUserFullName;
        public static int CurrentBranchId;
        public static int CurrentRoleId;
        private bool _loginSucceeded;
        private DateTime _secretGPressedAt = DateTime.MinValue;

        public FrmLogin()
        {
            InitializeComponent();
            ApplyLanguage();
        }

        // Closing this screen any other way than a successful login (the X
        // button, Alt+F4, Escape) exits the whole program instead of quietly
        // dropping into the main app unauthenticated - this dialog is also
        // reused for the idle-lock re-auth screen, so dismissing it has to be
        // a real "I'm done" rather than a way to slip past the lock.
        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_loginSucceeded)
                Application.Exit();
        }

        // Hidden vendor-only shortcut (not shown anywhere in the UI): Ctrl+Alt+G
        // then, within 3 seconds, Ctrl+Alt+B opens the license key generator.
        // See LicenseManager.cs for how keys work.
        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.G)
            {
                _secretGPressedAt = DateTime.Now;
                return;
            }

            if (e.Control && e.Alt && e.KeyCode == Keys.B
                && _secretGPressedAt != DateTime.MinValue
                && (DateTime.Now - _secretGPressedAt).TotalSeconds <= 3)
            {
                _secretGPressedAt = DateTime.MinValue;
                new FrmLicenseGenerator().ShowDialog(this);
            }
        }

        public void ApplyLanguage()
        {
            this.Text = LocalizationManager.T("Login_Title");
            BTN_Login.Text = LocalizationManager.T("Login_BtnLogin");
            LB_Welcome.Text = LocalizationManager.T("Login_WelcomeBack");
            LB_plsSignIn.Text = LocalizationManager.T("Login_PleaseSignIn");
            layoutControlItem5.Text = LocalizationManager.T("Login_Branch");
            layoutControlItem1.Text = LocalizationManager.T("Login_Username");
            layoutControlItem2.Text = LocalizationManager.T("Login_Password");
            COL_Id.Caption = LocalizationManager.T("Login_ColId");
            COL_Name.Caption = LocalizationManager.T("Login_ColName");
        }
        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            using (var db = new ClothesShopDBContext())
            {
                var branches = db.Branches.ToList();
                TXT_Branch.Properties.DataSource = branches;
                if (branches.Count > 0)
                    TXT_Branch.EditValue = TXT_Branch.Properties.GetKeyValue(0);
            }

            var recent = GetRecentUsernames();
            if (recent.Count > 0)
            {
                TXT_Username.Text = recent[0];
                TXT_Password.Focus();
            }
        }

        // Remembered usernames live per-machine (not in the database - a PC in
        // Branch A shouldn't autofill a cashier who only ever worked at Branch
        // B), most-recently-used first, capped so the list can't grow forever.
        private static List<string> GetRecentUsernames()
        {
            string raw = Properties.Settings.Default.RecentUsernames;
            if (string.IsNullOrEmpty(raw)) return new List<string>();
            return raw.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        private static void RememberUsername(string username)
        {
            var list = GetRecentUsernames();
            list.RemoveAll(x => string.Equals(x, username, StringComparison.OrdinalIgnoreCase));
            list.Insert(0, username);
            if (list.Count > 5) list = list.Take(5).ToList();
            Properties.Settings.Default.RecentUsernames = string.Join("|", list);
            Properties.Settings.Default.Save();
        }

        // Icon on the username field: pick a previously-used name instead of
        // retyping it (handy when more than one cashier shares the same PC).
        // Each entry has a "Remove" sub-item to delete it from the list -
        // done as a submenu rather than an inline X glyph so it's a plain
        // ContextMenuStrip with no custom drawing. Uses a context menu (not a
        // custom popup control) specifically because that keeps TXT_Username
        // a normal typable ButtonEdit - the earlier PopupContainerEdit
        // version looked fancier but silently blocked typing a new username.
        private void TXT_Username_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var recent = GetRecentUsernames();
            if (recent.Count == 0) return;

            var menu = new ContextMenuStrip();
            foreach (var name in recent)
            {
                string capturedName = name;
                var item = new ToolStripMenuItem(capturedName);
                item.Click += (s, ev) =>
                {
                    TXT_Username.Text = capturedName;
                    TXT_Password.Focus();
                };

                var removeItem = new ToolStripMenuItem("✕ " + LocalizationManager.T("Shared_MenuDelete"));
                removeItem.Click += (s, ev) =>
                {
                    if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Login_ConfirmRemoveUsernameFmt"), capturedName),
                        LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    var list = GetRecentUsernames();
                    list.RemoveAll(x => string.Equals(x, capturedName, StringComparison.OrdinalIgnoreCase));
                    Properties.Settings.Default.RecentUsernames = string.Join("|", list);
                    Properties.Settings.Default.Save();
                };
                item.DropDownItems.Add(removeItem);

                menu.Items.Add(item);
            }
            menu.Show(TXT_Username, new Point(0, TXT_Username.Height));
        }

       
        private void BTN_Login_Click(object sender, EventArgs e)
        {
            string username = TXT_Username.Text.Trim();
            string password = TXT_Password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Login_EnterCredentials"));
                return;
            }

            if (TXT_Branch.EditValue == null)
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Login_SelectBranch"));
                return;
            }

            using (var db = new ClothesShopDBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.IsActive == true);

                if (user == null)
                {
                    Sett.MsgRed(LocalizationManager.T("Login_Failed"), LocalizationManager.T("Login_UsernameNotFound"));
                    return;
                }

                bool passwordCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                if (!passwordCorrect)
                {
                    Sett.MsgRed(LocalizationManager.T("Login_Failed"), LocalizationManager.T("Login_IncorrectPassword"));
                    return;
                }

                CurrentUserId = user.Id;
                CurrentUserFullName = user.FullName;
                CurrentBranchId = (int)TXT_Branch.EditValue;
                CurrentRoleId = user.RoleId;
                PermissionManager.Load(CurrentRoleId);

                RememberUsername(username);

                _loginSucceeded = true;
                Sett.MsgGreen(LocalizationManager.T("Login_WelcomeTitle"), string.Format(LocalizationManager.T("Login_WelcomeUser"), user.FullName));
                this.Hide();
            }

        }

        private void TXT_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTN_Login_Click(sender, e);
            }
        }
    }
}

