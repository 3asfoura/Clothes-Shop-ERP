using Clothes_Shop_ERP.DAL;
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
    public partial class FrmProductEdit : DevExpress.XtraEditors.XtraForm
    {
        public string Code => TxtCode.Text.Trim();
        public string ProductName => TxtName.Text.Trim();
        public decimal BasePrice => (decimal)SpinBasePrice.Value;
        public bool IsActive => ChkIsActive.Checked;
        public int CategoryId => _categoryIds[CmbCategory.SelectedIndex];
        public int? BrandId => CmbBrand.SelectedIndex <= 0 ? (int?)null : _brandIds[CmbBrand.SelectedIndex - 1];

        private TextEdit TxtCode, TxtName;
        private SpinEdit SpinBasePrice;
        private CheckEdit ChkIsActive;
        private ComboBoxEdit CmbCategory, CmbBrand;
        private List<int> _categoryIds = new List<int>();
        private List<int> _brandIds = new List<int>();
        public FrmProductEdit()
        {
            InitializeComponent();
        }
        public FrmProductEdit(string title, string code = "", string name = "", decimal basePrice = 0,
            bool isActive = true, int currentCategoryId = 0, int? currentBrandId = null)
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblCode = new LabelControl { Text = "Code:", Location = new System.Drawing.Point(20, 20) };
            TxtCode = new TextEdit
            {
                Text = string.IsNullOrEmpty(code) ? GenerateNextCode() : code,
                Location = new System.Drawing.Point(20, 40),
                Width = 320
            };

            var lblName = new LabelControl { Text = "Name:", Location = new System.Drawing.Point(20, 75) };
            TxtName = new TextEdit { Text = name, Location = new System.Drawing.Point(20, 95), Width = 320 };

            var lblCategory = new LabelControl { Text = "Category:", Location = new System.Drawing.Point(20, 130) };
            CmbCategory = new ComboBoxEdit { Location = new System.Drawing.Point(20, 150), Width = 320 };
            CmbCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblBrand = new LabelControl { Text = "Brand (optional):", Location = new System.Drawing.Point(20, 185) };
            CmbBrand = new ComboBoxEdit { Location = new System.Drawing.Point(20, 205), Width = 320 };
            CmbBrand.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            using (var db = new ClothesShopDBContext())
            {
                foreach (var c in db.Categories.ToList())
                {
                    CmbCategory.Properties.Items.Add(c.Name);
                    _categoryIds.Add(c.Id);
                }

                CmbBrand.Properties.Items.Add("(None)");
                foreach (var b in db.Brands.ToList())
                {
                    CmbBrand.Properties.Items.Add(b.Name);
                    _brandIds.Add(b.Id);
                }
            }
            int catIdx = _categoryIds.IndexOf(currentCategoryId);
            CmbCategory.SelectedIndex = catIdx >= 0 ? catIdx : 0;

            int brandIdx = currentBrandId.HasValue ? _brandIds.IndexOf(currentBrandId.Value) : -1;
            CmbBrand.SelectedIndex = brandIdx >= 0 ? brandIdx + 1 : 0;

            var lblPrice = new LabelControl { Text = "Base Price:", Location = new System.Drawing.Point(20, 240) };
            SpinBasePrice = new SpinEdit { Value = basePrice, Location = new System.Drawing.Point(20, 260), Width = 320 };
            SpinBasePrice.Properties.MaxValue = 999999;
            SpinBasePrice.Properties.DisplayFormat.FormatString = "n2";

            ChkIsActive = new CheckEdit { Text = "Active", Checked = isActive, Location = new System.Drawing.Point(20, 295) };

            var btnSave = new SimpleButton { Text = "Save", Location = new System.Drawing.Point(160, 320), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtCode.Text) || string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show("Please fill in the code and name.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(240, 320), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblCode); this.Controls.Add(TxtCode);
            this.Controls.Add(lblName); this.Controls.Add(TxtName);
            this.Controls.Add(lblCategory); this.Controls.Add(CmbCategory);
            this.Controls.Add(lblBrand); this.Controls.Add(CmbBrand);
            this.Controls.Add(lblPrice); this.Controls.Add(SpinBasePrice);
            this.Controls.Add(ChkIsActive);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
        private string GenerateNextCode()
        {
            using (var db = new ClothesShopDBContext())
            {
                try
                {
                    var maxCode = db.Products
                        .Select(p => p.Code)
                        .AsEnumerable()
                        .Select(c => int.TryParse(c, out int n) ? n : 0)
                        .DefaultIfEmpty(0)
                        .Max();

                    return maxCode == 0 ? "10001" : (maxCode + 1).ToString();
                }
                catch
                {
                    return "10001";
                }
            }
        }
    }
}