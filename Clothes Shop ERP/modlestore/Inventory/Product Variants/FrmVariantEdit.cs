using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
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
    public partial class FrmVariantEdit : DevExpress.XtraEditors.XtraForm
    {
        public string Barcode => TxtBarcode.Text.Trim();
        public decimal SalePrice => (decimal)SpinSalePrice.Value;
        public decimal CostPrice => (decimal)SpinCostPrice.Value;
        public bool IsActive => ChkIsActive.Checked;
        public int ProductId => _productIds[CmbProduct.SelectedIndex];
        public int ColorId => _colorIds[CmbColor.SelectedIndex];
        public int SizeId => _sizeIds[CmbSize.SelectedIndex];

        private TextEdit TxtBarcode;
        private SpinEdit SpinSalePrice, SpinCostPrice;
        private CheckEdit ChkIsActive;
        private ComboBoxEdit CmbProduct, CmbColor, CmbSize;
        private List<int> _productIds = new List<int>();
        private List<int> _colorIds = new List<int>();
        private List<int> _sizeIds = new List<int>();
        private bool _isEditMode;

        public FrmVariantEdit()
        {
            InitializeComponent();
        }
        private string GenerateNextBarcode()
        {
            using (var db = new ClothesShopDBContext())
            {
                try
                {
                    var maxBarcode = db.ProductVariants
                        .Select(v => v.Barcode)
                        .AsEnumerable()
                        .Select(b => long.TryParse(b, out long n) ? n : 0)
                        .DefaultIfEmpty(0)
                        .Max();

                    return maxBarcode == 0 ? "1000001" : (maxBarcode + 1).ToString();
                }
                catch
                {
                    return "1000001";
                }
            }
        }
        public FrmVariantEdit(string title, string barcode = "", decimal salePrice = 0, decimal costPrice = 0,
           bool isActive = true, int currentProductId = 0, int currentColorId = 0, int currentSizeId = 0)
        {
            _isEditMode = currentProductId != 0;   // if we're editing, an existing product is already selected

            this.Text = title;
            this.Width = 380;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblProduct = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_Product"), Location = new System.Drawing.Point(20, 20) };
            CmbProduct = new ComboBoxEdit { Location = new System.Drawing.Point(20, 40), Width = 320 };
            CmbProduct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblColor = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_Color"), Location = new System.Drawing.Point(20, 75) };
            CmbColor = new ComboBoxEdit { Location = new System.Drawing.Point(20, 95), Width = 150 };
            CmbColor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblSize = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_Size"), Location = new System.Drawing.Point(190, 75) };
            CmbSize = new ComboBoxEdit { Location = new System.Drawing.Point(190, 95), Width = 150 };
            CmbSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            using (var db = new ClothesShopDBContext())
            {
                foreach (var p in db.Products
    .Include(x => x.Category)
    .Where(x => x.IsActive == true && x.Category.IsActive == true)   
    .ToList())
                {
                    CmbProduct.Properties.Items.Add($"{p.Code} - {p.Name}");
                    _productIds.Add(p.Id);
                }
                foreach (var c in db.Colors.ToList())
                {
                    CmbColor.Properties.Items.Add(c.Name);
                    _colorIds.Add(c.Id);
                }
                foreach (var sz in db.Sizes.OrderBy(x => x.SortOrder).ToList())
                {
                    CmbSize.Properties.Items.Add(sz.Name);
                    _sizeIds.Add(sz.Id);
                }
            }

            int pIdx = _productIds.IndexOf(currentProductId);
            CmbProduct.SelectedIndex = pIdx >= 0 ? pIdx : 0;
            int cIdx = _colorIds.IndexOf(currentColorId);
            CmbColor.SelectedIndex = cIdx >= 0 ? cIdx : 0;
            int sIdx = _sizeIds.IndexOf(currentSizeId);
            CmbSize.SelectedIndex = sIdx >= 0 ? sIdx : 0;

            var lblBarcode = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_Barcode"), Location = new System.Drawing.Point(20, 130) };
            TxtBarcode = new TextEdit { Text = barcode, Location = new System.Drawing.Point(20, 150), Width = 320 };

            var lblSalePrice = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_SalePrice"), Location = new System.Drawing.Point(20, 185) };
            SpinSalePrice = new SpinEdit { Value = salePrice, Location = new System.Drawing.Point(20, 205), Width = 320 };
            SpinSalePrice.Properties.MaxValue = 999999;
            SpinSalePrice.Properties.DisplayFormat.FormatString = "n2";

            var lblCostPrice = new LabelControl { Text = LocalizationManager.T("FrmVariantEdit_CostPrice"), Location = new System.Drawing.Point(20, 240) };
            SpinCostPrice = new SpinEdit { Value = costPrice, Location = new System.Drawing.Point(20, 260), Width = 320 };
            SpinCostPrice.Properties.MaxValue = 999999;
            SpinCostPrice.Properties.DisplayFormat.FormatString = "n2";

            ChkIsActive = new CheckEdit { Text = LocalizationManager.T("Shared_Active"), Checked = isActive, Location = new System.Drawing.Point(20, 295) };

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shared_BtnSave"), Location = new System.Drawing.Point(160, 330), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtBarcode.Text))
                {
                    XtraMessageBox.Show(LocalizationManager.T("ProductVariants_BarcodeRequired"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(240, 330), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblProduct); this.Controls.Add(CmbProduct);
            this.Controls.Add(lblColor); this.Controls.Add(CmbColor);
            this.Controls.Add(lblSize); this.Controls.Add(CmbSize);
            this.Controls.Add(lblBarcode); this.Controls.Add(TxtBarcode);
            this.Controls.Add(lblSalePrice); this.Controls.Add(SpinSalePrice);
            this.Controls.Add(lblCostPrice); this.Controls.Add(SpinCostPrice);
            this.Controls.Add(ChkIsActive);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

        
            CmbProduct.SelectedIndexChanged += (s, e) =>
            {
                if (!_isEditMode)
                    TxtBarcode.Text = GenerateNextBarcode();

                using (var db = new ClothesShopDBContext())
                {
                    int productId = _productIds[CmbProduct.SelectedIndex];
                    var basePrice = db.Products
                        .Where(p => p.Id == productId)
                        .Select(p => p.BasePrice)
                        .FirstOrDefault();

                    SpinSalePrice.Value = basePrice;
                }
            };


            if (!_isEditMode && _productIds.Count > 0)
            {
                TxtBarcode.Text = GenerateNextBarcode();  

                using (var db = new ClothesShopDBContext())
                {
                    var basePrice = db.Products
                        .Where(p => p.Id == _productIds[CmbProduct.SelectedIndex])
                        .Select(p => p.BasePrice)
                        .FirstOrDefault();

                    SpinSalePrice.Value = basePrice;
                }
            }
        }
    }
}