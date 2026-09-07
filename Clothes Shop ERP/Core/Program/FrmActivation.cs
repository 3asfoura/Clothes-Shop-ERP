using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Shown once, before login, on any PC that hasn't been activated yet.
    // Blocks the app from opening until a valid license key is entered - see
    // LicenseManager.cs for how the key itself is generated/checked.
    public partial class FrmActivation : DevExpress.XtraEditors.XtraForm
    {
        public bool Activated { get; private set; }

        private TextEdit TxtMachineId;
        private MemoEdit TxtLicenseKey;

        public FrmActivation()
        {
            InitializeComponent();
            this.Text = LocalizationManager.T("Activation_Title");
            this.Width = 460;
            this.Height = 340;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblIntro = new LabelControl
            {
                Text = LocalizationManager.T("Activation_Intro"),
                Location = new System.Drawing.Point(20, 15),
                Width = 400,
                AutoSizeMode = LabelAutoSizeMode.None,
                Height = 50
            };
            lblIntro.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            var lblMachineId = new LabelControl { Text = LocalizationManager.T("Activation_MachineId"), Location = new System.Drawing.Point(20, 75) };
            TxtMachineId = new TextEdit { Text = LicenseManager.GetMachineId(), Location = new System.Drawing.Point(20, 95), Width = 300 };
            TxtMachineId.Properties.ReadOnly = true;

            var btnCopyId = new SimpleButton { Text = LocalizationManager.T("Activation_BtnCopy"), Location = new System.Drawing.Point(330, 95), Width = 90 };
            btnCopyId.Click += (s, e) =>
            {
                Clipboard.SetText(TxtMachineId.Text);
                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Activation_IdCopied"));
            };

            var lblLicenseKey = new LabelControl { Text = LocalizationManager.T("Activation_LicenseKey"), Location = new System.Drawing.Point(20, 135) };
            TxtLicenseKey = new MemoEdit { Location = new System.Drawing.Point(20, 155), Width = 400, Height = 70 };

            var btnActivate = new SimpleButton { Text = LocalizationManager.T("Activation_BtnActivate"), Location = new System.Drawing.Point(230, 245), Width = 90, DialogResult = DialogResult.OK };
            btnActivate.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtLicenseKey.Text))
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Activation_EnterKeyFirst"));
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (!LicenseManager.ValidateLicenseKey(TxtMachineId.Text, TxtLicenseKey.Text, out _, out string error))
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_Error"), error);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                LicenseManager.SaveActivation(TxtLicenseKey.Text);
                Activated = true;
                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Activation_Success"));
            };

            var btnExit = new SimpleButton { Text = LocalizationManager.T("Activation_BtnExit"), Location = new System.Drawing.Point(330, 245), Width = 90 };
            btnExit.Click += (s, e) =>
            {
                if (XtraMessageBox.Show(LocalizationManager.T("Activation_ExitConfirm"), LocalizationManager.T("Common_ConfirmTitle"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Activated = false;
                    this.DialogResult = DialogResult.Cancel;
                }
            };

            this.Controls.Add(lblIntro);
            this.Controls.Add(lblMachineId); this.Controls.Add(TxtMachineId); this.Controls.Add(btnCopyId);
            this.Controls.Add(lblLicenseKey); this.Controls.Add(TxtLicenseKey);
            this.Controls.Add(btnActivate); this.Controls.Add(btnExit);

            this.AcceptButton = btnActivate;

            // Same hidden vendor-only shortcut as the login screen (see FrmLogin.cs) -
            // it also needs to work here, since this screen shows up *before* login on
            // a fresh install, which is exactly when the vendor needs to generate the
            // very first key for that machine.
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.Alt && e.KeyCode == Keys.G)
                {
                    using (var gen = new FrmLicenseGenerator())
                    {
                        gen.ShowDialog(this);
                    }
                }
            };
        }
    }
}
