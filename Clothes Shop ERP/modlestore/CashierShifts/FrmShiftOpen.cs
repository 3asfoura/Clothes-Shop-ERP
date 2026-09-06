using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class FrmShiftOpen : DevExpress.XtraEditors.XtraForm
    {
        public decimal OpeningFloat => (decimal)SpinFloat.Value;

        private SpinEdit SpinFloat;

        public FrmShiftOpen()
        {
            InitializeComponent();

            this.Text = LocalizationManager.T("Shift_OpenTitle");
            this.Width = 340;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblFloat = new LabelControl { Text = LocalizationManager.T("Shift_OpeningFloat"), Location = new System.Drawing.Point(20, 20) };
            SpinFloat = new SpinEdit { Location = new System.Drawing.Point(20, 40), Width = 280, Value = 0 };
            SpinFloat.Properties.MaxValue = 9999999;
            SpinFloat.Properties.MinValue = 0;
            SpinFloat.Properties.DisplayFormat.FormatString = "n2";

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shift_BtnOpen"), Location = new System.Drawing.Point(20, 100), Width = 280, DialogResult = DialogResult.OK };
            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(20, 135), Width = 280, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblFloat);
            this.Controls.Add(SpinFloat);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}
