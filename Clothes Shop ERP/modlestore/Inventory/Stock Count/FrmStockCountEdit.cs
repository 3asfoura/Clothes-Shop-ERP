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
using Microsoft.EntityFrameworkCore;
namespace Clothes_Shop_ERP
{
    public partial class FrmStockCountEdit : DevExpress.XtraEditors.XtraForm
    {
        public decimal Quantity => (decimal)SpinQuantity.Value;
        public decimal MinQuantity => (decimal)SpinMinQuantity.Value;
        public int ProductVariantId => _variantIds[CmbVariant.SelectedIndex];
        public int BranchId => _branchIds[CmbBranch.SelectedIndex];

        private ComboBoxEdit CmbVariant, CmbBranch;
        private SpinEdit SpinQuantity, SpinMinQuantity;
        private List<int> _variantIds = new List<int>();
        private List<int> _branchIds = new List<int>();
        public FrmStockCountEdit()
        {
            InitializeComponent();
        }
        public FrmStockCountEdit(string title, bool isEditMode, int currentVariantId = 0, int currentBranchId = 0,
            decimal quantity = 0, decimal minQuantity = 0)
        {
            this.Text = title;
            this.Width = 380;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblVariant = new LabelControl { Text = "Product Variant:", Location = new System.Drawing.Point(20, 20) };
            CmbVariant = new ComboBoxEdit { Location = new System.Drawing.Point(20, 40), Width = 320, Enabled = !isEditMode };
            CmbVariant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblBranch = new LabelControl { Text = "Branch:", Location = new System.Drawing.Point(20, 75) };
            CmbBranch = new ComboBoxEdit { Location = new System.Drawing.Point(20, 95), Width = 320, Enabled = !isEditMode };
            CmbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            using (var db = new ClothesShopDBContext())
            {
                foreach (var v in db.ProductVariants
    .Include(x => x.Product).ThenInclude(p => p.Category)
    .Where(x => x.IsActive == true
             && x.Product.IsActive == true
             && x.Product.Category.IsActive == true)   
    .ToList())
                {
                    CmbVariant.Properties.Items.Add($"{v.Product.Name} - {v.Barcode}");
                    _variantIds.Add(v.Id);
                }
                foreach (var b in db.Branches.ToList())
                {
                    CmbBranch.Properties.Items.Add(b.Name);
                    _branchIds.Add(b.Id);
                }
            }
            int vIdx = _variantIds.IndexOf(currentVariantId);
            CmbVariant.SelectedIndex = vIdx >= 0 ? vIdx : 0;
            int bIdx = _branchIds.IndexOf(currentBranchId);
            CmbBranch.SelectedIndex = bIdx >= 0 ? bIdx : 0;

            var lblQty = new LabelControl { Text = "Quantity:", Location = new System.Drawing.Point(20, 130) };
            SpinQuantity = new SpinEdit { Value = quantity, Location = new System.Drawing.Point(20, 150), Width = 320 };
            SpinQuantity.Properties.MaxValue = 999999;

            var lblMinQty = new LabelControl { Text = "Minimum Quantity (reorder alert):", Location = new System.Drawing.Point(20, 185) };
            SpinMinQuantity = new SpinEdit { Value = minQuantity, Location = new System.Drawing.Point(20, 205), Width = 320 };
            SpinMinQuantity.Properties.MaxValue = 999999;

            var btnSave = new SimpleButton { Text = "Save", Location = new System.Drawing.Point(160, 240), DialogResult = DialogResult.OK };
            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(240, 240), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblVariant); this.Controls.Add(CmbVariant);
            this.Controls.Add(lblBranch); this.Controls.Add(CmbBranch);
            this.Controls.Add(lblQty); this.Controls.Add(SpinQuantity);
            this.Controls.Add(lblMinQty); this.Controls.Add(SpinMinQuantity);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}