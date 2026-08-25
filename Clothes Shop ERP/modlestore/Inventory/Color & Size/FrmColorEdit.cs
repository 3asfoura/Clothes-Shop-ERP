using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Clothes_Shop_ERP
{
    public partial class FrmColorEdit : DevExpress.XtraEditors.XtraForm
    {
        public string ColorName => TxtName.Text.Trim();
        public string HexCode => ColorTranslator.ToHtml(ColorPicker.Color);

        private TextEdit TxtName;
        private ColorPickEdit ColorPicker;

        public FrmColorEdit(string title, string name = "", string hex = "")
        {
            this.Text = title;
            this.Width = 340;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new LabelControl { Text = "Name:", Location = new System.Drawing.Point(20, 20) };
            TxtName = new TextEdit { Text = name, Location = new System.Drawing.Point(20, 40), Width = 280 };

            var lblColor = new LabelControl { Text = "Pick a color:", Location = new System.Drawing.Point(20, 75) };
            ColorPicker = new ColorPickEdit
            {
                Location = new System.Drawing.Point(20, 95),
                Width = 280,
                Color = string.IsNullOrWhiteSpace(hex) ? Color.Black : ColorTranslator.FromHtml(hex)
            };

            var btnSave = new SimpleButton { Text = "Save", Location = new System.Drawing.Point(140, 130), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show("Please enter a color name.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(220, 130), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblName);
            this.Controls.Add(TxtName);
            this.Controls.Add(lblColor);
            this.Controls.Add(ColorPicker);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}