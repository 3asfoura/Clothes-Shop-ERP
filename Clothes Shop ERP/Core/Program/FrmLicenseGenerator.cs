using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Vendor-only tool: turns a customer's Machine ID into a license key.
    // Not linked from any menu - only reachable via Ctrl+Alt+G on the login
    // screen (see FrmLogin.cs), so ordinary shop users never see it.
    public partial class FrmLicenseGenerator : DevExpress.XtraEditors.XtraForm
    {
        private TextEdit TxtMachineId;
        private RadioGroup RadioExpiry;
        private DateEdit DtExpiry;
        private MemoEdit TxtResult;

        public FrmLicenseGenerator()
        {
            InitializeComponent();
            this.Text = LocalizationManager.T("LicenseGen_Title");
            this.Width = 460;
            this.Height = 440;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblMachineId = new LabelControl { Text = LocalizationManager.T("LicenseGen_MachineId"), Location = new System.Drawing.Point(20, 15) };
            TxtMachineId = new TextEdit { Location = new System.Drawing.Point(20, 35), Width = 400 };

            // Explicit choice instead of an easy-to-miss checkbox: lifetime is
            // the default (index 0), so generating a key with no further
            // clicks already produces a never-expiring license.
            RadioExpiry = new RadioGroup { Location = new System.Drawing.Point(20, 70), Width = 400, Height = 54 };
            RadioExpiry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(false, LocalizationManager.T("LicenseGen_Lifetime")));
            RadioExpiry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(true, LocalizationManager.T("LicenseGen_HasExpiry")));
            RadioExpiry.SelectedIndex = 0;

            DtExpiry = new DateEdit { Location = new System.Drawing.Point(20, 130), Width = 150, Enabled = false };
            DtExpiry.DateTime = DateTime.Today.AddYears(1);
            RadioExpiry.SelectedIndexChanged += (s, e) => DtExpiry.Enabled = (bool)RadioExpiry.EditValue;

            var btnGenerate = new SimpleButton { Text = LocalizationManager.T("LicenseGen_BtnGenerate"), Location = new System.Drawing.Point(20, 165), Width = 400 };
            btnGenerate.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtMachineId.Text))
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("LicenseGen_EnterIdFirst"));
                    return;
                }

                DateTime? expiry = (bool)RadioExpiry.EditValue ? DtExpiry.DateTime.Date : (DateTime?)null;
                TxtResult.Text = LicenseManager.GenerateLicenseKey(TxtMachineId.Text, expiry);
            };

            var lblResult = new LabelControl { Text = LocalizationManager.T("LicenseGen_ResultKey"), Location = new System.Drawing.Point(20, 205) };
            TxtResult = new MemoEdit { Location = new System.Drawing.Point(20, 225), Width = 400, Height = 100 };
            TxtResult.Properties.ReadOnly = true;

            var btnCopy = new SimpleButton { Text = LocalizationManager.T("LicenseGen_BtnCopy"), Location = new System.Drawing.Point(20, 335), Width = 400 };
            btnCopy.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtResult.Text)) return;
                Clipboard.SetText(TxtResult.Text);
                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("LicenseGen_Copied"));
            };

            this.Controls.Add(lblMachineId); this.Controls.Add(TxtMachineId);
            this.Controls.Add(RadioExpiry); this.Controls.Add(DtExpiry);
            this.Controls.Add(btnGenerate);
            this.Controls.Add(lblResult); this.Controls.Add(TxtResult);
            this.Controls.Add(btnCopy);
        }
    }
}
