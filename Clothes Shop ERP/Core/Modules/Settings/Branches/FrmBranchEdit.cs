using DevExpress.XtraEditors;
using Clothes_Shop_ERP.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.Resources
{
    public partial class FrmBranchEdit : DevExpress.XtraEditors.XtraForm
    {
        public string BranchName => TxtName.Text.Trim();
        public string BranchAddress => TxtAddress.Text.Trim();
        public string BranchPhone => TxtPhone.Text.Trim();

        private TextEdit TxtName;
        private TextEdit TxtAddress;
        private TextEdit TxtPhone;

        public FrmBranchEdit()
        {
            InitializeComponent();
        }
        public FrmBranchEdit(string title, string name = "", string address = "", string phone = "")
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 260;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new LabelControl { Text = LocalizationManager.T("Shared_ColName"), Location = new System.Drawing.Point(20, 20) };
            TxtName = new TextEdit { Text = name, Location = new System.Drawing.Point(20, 40), Width = 320 };

            var lblAddress = new LabelControl { Text = LocalizationManager.T("Shared_ColAddress"), Location = new System.Drawing.Point(20, 75) };
            TxtAddress = new TextEdit { Text = address, Location = new System.Drawing.Point(20, 95), Width = 320 };

            var lblPhone = new LabelControl { Text = LocalizationManager.T("Shared_ColPhone"), Location = new System.Drawing.Point(20, 130) };
            TxtPhone = new TextEdit { Text = phone, Location = new System.Drawing.Point(20, 150), Width = 320 };

            var btnSave = new SimpleButton
            {
                Text = LocalizationManager.T("Shared_BtnSave"),
                Location = new System.Drawing.Point(180, 190),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show(LocalizationManager.T("Branches_NameRequired"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton
            {
                Text = LocalizationManager.T("Shared_BtnCancel"),
                Location = new System.Drawing.Point(260, 190),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(lblName);
            this.Controls.Add(TxtName);
            this.Controls.Add(lblAddress);
            this.Controls.Add(TxtAddress);
            this.Controls.Add(lblPhone);
            this.Controls.Add(TxtPhone);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}