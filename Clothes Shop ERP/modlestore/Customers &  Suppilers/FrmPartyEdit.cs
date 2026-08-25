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

namespace Clothes_Shop_ERP
{
    public partial class FrmPartyEdit : DevExpress.XtraEditors.XtraForm
    {
        public string PartyName => TxtName.Text.Trim();
        public string Address => TxtAddress.Text.Trim();
        public string Phone => TxtPhone.Text.Trim();
        public bool IsActive => ChkIsActive.Checked;

        private TextEdit TxtName, TxtAddress, TxtPhone;
        private CheckEdit ChkIsActive;
        public FrmPartyEdit()
        {
            InitializeComponent();
        }
        public FrmPartyEdit(string title, string name = "", string address = "", string phone = "", bool isActive = true)
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 290;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new LabelControl { Text = "Name:", Location = new System.Drawing.Point(20, 20) };
            TxtName = new TextEdit { Text = name, Location = new System.Drawing.Point(20, 40), Width = 320 };

            var lblAddress = new LabelControl { Text = "Address:", Location = new System.Drawing.Point(20, 75) };
            TxtAddress = new TextEdit { Text = address, Location = new System.Drawing.Point(20, 95), Width = 320 };

            var lblPhone = new LabelControl { Text = "Phone:", Location = new System.Drawing.Point(20, 130) };
            TxtPhone = new TextEdit { Text = phone, Location = new System.Drawing.Point(20, 150), Width = 320 };

            ChkIsActive = new CheckEdit { Text = "Active", Checked = isActive, Location = new System.Drawing.Point(20, 185) };

            var btnSave = new SimpleButton { Text = "Save", Location = new System.Drawing.Point(180, 220), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show("Please enter a name.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(260, 220), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblName); this.Controls.Add(TxtName);
            this.Controls.Add(lblAddress); this.Controls.Add(TxtAddress);
            this.Controls.Add(lblPhone); this.Controls.Add(TxtPhone);
            this.Controls.Add(ChkIsActive);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}