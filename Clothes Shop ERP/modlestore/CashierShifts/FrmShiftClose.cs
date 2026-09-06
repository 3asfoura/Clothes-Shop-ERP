using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class FrmShiftClose : DevExpress.XtraEditors.XtraForm
    {
        public decimal CountedCash => (decimal)SpinCounted.Value;
        public string Notes => TxtNotes.Text.Trim();

        private SpinEdit SpinCounted;
        private TextEdit TxtNotes;
        private LabelControl LblDifference;
        private readonly decimal _expectedCash;

        public FrmShiftClose(decimal expectedCash)
        {
            _expectedCash = expectedCash;
            InitializeComponent();

            this.Text = LocalizationManager.T("Shift_CloseTitle");
            this.Width = 360;
            this.Height = 320;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblExpected = new LabelControl
            {
                Text = string.Format(LocalizationManager.T("Shift_ExpectedCashFmt"), expectedCash),
                Location = new System.Drawing.Point(20, 20),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };

            var lblCounted = new LabelControl { Text = LocalizationManager.T("Shift_CountedCash"), Location = new System.Drawing.Point(20, 55) };
            SpinCounted = new SpinEdit { Location = new System.Drawing.Point(20, 75), Width = 300, Value = expectedCash };
            SpinCounted.Properties.MaxValue = 9999999;
            SpinCounted.Properties.MinValue = 0;
            SpinCounted.Properties.DisplayFormat.FormatString = "n2";

            LblDifference = new LabelControl { Location = new System.Drawing.Point(20, 110), AutoSizeMode = LabelAutoSizeMode.None, Size = new System.Drawing.Size(300, 20) };
            SpinCounted.EditValueChanged += (s, e) => RefreshDifference();

            var lblNotes = new LabelControl { Text = LocalizationManager.T("Shared_Notes"), Location = new System.Drawing.Point(20, 140) };
            TxtNotes = new TextEdit { Location = new System.Drawing.Point(20, 160), Width = 300 };

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shift_BtnClose"), Location = new System.Drawing.Point(20, 220), Width = 300, DialogResult = DialogResult.OK };
            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(20, 255), Width = 300, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblExpected);
            this.Controls.Add(lblCounted);
            this.Controls.Add(SpinCounted);
            this.Controls.Add(LblDifference);
            this.Controls.Add(lblNotes);
            this.Controls.Add(TxtNotes);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            RefreshDifference();
        }

        private void RefreshDifference()
        {
            decimal diff = (decimal)SpinCounted.Value - _expectedCash;
            LblDifference.Text = string.Format(LocalizationManager.T("Shift_DifferenceFmt"), diff);
            LblDifference.ForeColor = diff == 0 ? System.Drawing.Color.DarkGreen : System.Drawing.Color.DarkRed;
        }
    }
}
