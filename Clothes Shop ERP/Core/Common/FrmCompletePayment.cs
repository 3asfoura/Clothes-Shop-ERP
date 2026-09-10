using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Shared "how much are you paying right now" dialog, reused for completing
    // a partially-paid Purchase/Sales invoice and for staging a partial payment
    // at POS checkout - same three numbers (total/paid so far/due) either way,
    // just a different label and Treasury direction on the caller's side.
    public partial class FrmCompletePayment : DevExpress.XtraEditors.XtraForm
    {
        public decimal AmountToPay => (decimal)SpinAmount.Value;

        private SpinEdit SpinAmount;

        public FrmCompletePayment()
        {
            InitializeComponent();
        }

        public FrmCompletePayment(string title, decimal total, decimal paidSoFar)
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            decimal due = total - paidSoFar;

            var lblTotal = new LabelControl { Text = LocalizationManager.T("Payment_Total") + ": " + total.ToString("n2"), Location = new Point(20, 20), Width = 320 };
            var lblPaid = new LabelControl { Text = LocalizationManager.T("Payment_PaidSoFar") + ": " + paidSoFar.ToString("n2"), Location = new Point(20, 45), Width = 320 };
            var lblDue = new LabelControl
            {
                Text = LocalizationManager.T("Payment_Due") + ": " + due.ToString("n2"),
                Location = new Point(20, 70),
                Width = 320,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var lblAmount = new LabelControl { Text = LocalizationManager.T("Payment_AmountNow"), Location = new Point(20, 115) };
            SpinAmount = new SpinEdit { Location = new Point(20, 135), Width = 320 };
            SpinAmount.Properties.MinValue = 0;
            SpinAmount.Properties.MaxValue = due;
            SpinAmount.Properties.DisplayFormat.FormatString = "n2";
            SpinAmount.Properties.EditFormat.FormatString = "n2";
            SpinAmount.Value = due;

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shared_BtnSave"), Location = new Point(160, 220), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (SpinAmount.Value <= 0 || SpinAmount.Value > due)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Payment_InvalidAmount"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new Point(240, 220), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblTotal);
            this.Controls.Add(lblPaid);
            this.Controls.Add(lblDue);
            this.Controls.Add(lblAmount);
            this.Controls.Add(SpinAmount);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}
