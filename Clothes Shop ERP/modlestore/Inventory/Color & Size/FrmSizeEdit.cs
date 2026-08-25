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
    public partial class FrmSizeEdit : DevExpress.XtraEditors.XtraForm
    {
        public string SizeName => TxtName.Text.Trim();
        public int SortOrder => (int)SpinSortOrder.Value;

        private TextEdit TxtName;
        private SpinEdit SpinSortOrder;
        public FrmSizeEdit()
        {
            InitializeComponent();
        }
        public FrmSizeEdit(string title, string name = "", int sortOrder = 0)
        {
            this.Text = title;
            this.Width = 320;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new LabelControl { Text = "Name (e.g. M, L, XL):", Location = new System.Drawing.Point(20, 20) };
            TxtName = new TextEdit { Text = name, Location = new System.Drawing.Point(20, 40), Width = 260 };

            var lblSort = new LabelControl { Text = "Sort Order:", Location = new System.Drawing.Point(20, 75) };
            SpinSortOrder = new SpinEdit { Value = sortOrder, Location = new System.Drawing.Point(20, 95), Width = 260 };
            SpinSortOrder.Properties.MinValue = 0;
            SpinSortOrder.Properties.MaxValue = 999;

            var btnSave = new SimpleButton { Text = "Save", Location = new System.Drawing.Point(120, 130), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show("Please enter a size name.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(200, 130), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblName);
            this.Controls.Add(TxtName);
            this.Controls.Add(lblSort);
            this.Controls.Add(SpinSortOrder);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}