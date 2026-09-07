using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
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
    public partial class FrmTreasuryEdit : DevExpress.XtraEditors.XtraForm
    {
        public string TransactionType => CmbType.SelectedIndex == 0 ? "In" : "Out";
        public decimal Amount => (decimal)SpinAmount.Value;
        public string Description => TxtDescription.Text.Trim();
        public int BranchId => _branchIds[CmbBranch.SelectedIndex];

        private ComboBoxEdit CmbType, CmbBranch;
        private SpinEdit SpinAmount;
        private TextEdit TxtDescription;
        private List<int> _branchIds = new List<int>();
        public FrmTreasuryEdit()
        {
            InitializeComponent();
        }
        public FrmTreasuryEdit(string title, string type = "In", decimal amount = 0,
            string description = "", int currentBranchId = 0)
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 320;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblType = new LabelControl { Text = LocalizationManager.T("FrmTreasuryEdit_Type"), Location = new System.Drawing.Point(20, 20) };
            CmbType = new ComboBoxEdit { Location = new System.Drawing.Point(20, 40), Width = 320 };
            CmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            CmbType.Properties.Items.Add(LocalizationManager.T("FrmTreasuryEdit_TypeIn"));
            CmbType.Properties.Items.Add(LocalizationManager.T("FrmTreasuryEdit_TypeOut"));
            CmbType.SelectedIndex = type == "In" ? 0 : 1;

            var lblBranch = new LabelControl { Text = LocalizationManager.T("Shared_ColBranch"), Location = new System.Drawing.Point(20, 75) };
            CmbBranch = new ComboBoxEdit { Location = new System.Drawing.Point(20, 95), Width = 320 };
            CmbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            using (var db = new ClothesShopDBContext())
            {
                foreach (var b in db.Branches.ToList())
                {
                    CmbBranch.Properties.Items.Add(b.Name);
                    _branchIds.Add(b.Id);
                }
            }
            int bIdx = _branchIds.IndexOf(currentBranchId != 0 ? currentBranchId : FrmLogin.CurrentBranchId);
            CmbBranch.SelectedIndex = bIdx >= 0 ? bIdx : 0;

            var lblAmount = new LabelControl { Text = LocalizationManager.T("FrmTreasuryEdit_Amount"), Location = new System.Drawing.Point(20, 130) };
            SpinAmount = new SpinEdit { Value = amount, Location = new System.Drawing.Point(20, 150), Width = 320 };
            SpinAmount.Properties.MaxValue = 9999999;
            SpinAmount.Properties.DisplayFormat.FormatString = "n2";

            var lblDescription = new LabelControl { Text = LocalizationManager.T("FrmTreasuryEdit_Description"), Location = new System.Drawing.Point(20, 185) };
            TxtDescription = new TextEdit { Text = description, Location = new System.Drawing.Point(20, 205), Width = 320 };

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shared_BtnSave"), Location = new System.Drawing.Point(160, 250), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (SpinAmount.Value <= 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("Treasury_AmountGreaterThanZero"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(240, 250), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblType); this.Controls.Add(CmbType);
            this.Controls.Add(lblBranch); this.Controls.Add(CmbBranch);
            this.Controls.Add(lblAmount); this.Controls.Add(SpinAmount);
            this.Controls.Add(lblDescription); this.Controls.Add(TxtDescription);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}