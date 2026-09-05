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
        public FrmLogin()
        {
            InitializeComponent();
            ApplyLanguage();

            // Hidden vendor-only shortcut (not shown anywhere in the UI): opens the
            // license key generator. See LicenseManager.cs for how keys work.
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.Alt && e.KeyCode == Keys.G)
                    new FrmLicenseGenerator().ShowDialog(this);
            };
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
                TXT_Branch.Properties.DataSource = db.Branches.ToList();
                TXT_Branch.EditValue = TXT_Branch.Properties.GetKeyValue(0);
            }
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

                 Sett.MsgGreen(LocalizationManager.T("Login_WelcomeTitle"), string.Format(LocalizationManager.T("Login_WelcomeUser"), user.FullName));
                this.Hide();
            }

        }

        private void TXT_Password_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TXT_Password.Properties.UseSystemPasswordChar = !TXT_Password.Properties.UseSystemPasswordChar;
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

