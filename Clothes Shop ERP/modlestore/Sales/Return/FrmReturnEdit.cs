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
    public partial class FrmReturnEdit : DevExpress.XtraEditors.XtraForm
    {
        public int SalesInvoiceId => _invoiceIds[CmbInvoice.SelectedIndex];
        public int ProductVariantId => _lineVariantIds[CmbLine.SelectedIndex];
        public decimal UnitPrice => _lineUnitPrices[CmbLine.SelectedIndex];
        public decimal Quantity => (decimal)SpinQuantity.Value;

        private ComboBoxEdit CmbInvoice, CmbLine;
        private SpinEdit SpinQuantity;
        private List<int> _invoiceIds = new List<int>();
        private List<int> _lineVariantIds = new List<int>();
        private List<decimal> _lineUnitPrices = new List<decimal>();
        private List<decimal> _lineMaxQty = new List<decimal>();
        public FrmReturnEdit()
        {
            InitializeComponent();
        }
        public FrmReturnEdit(string title)
        {
            this.Text = title;
            this.Width = 400;
            this.Height = 260;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblInvoice = new LabelControl { Text = "Invoice:", Location = new System.Drawing.Point(20, 20) };
            CmbInvoice = new ComboBoxEdit { Location = new System.Drawing.Point(20, 40), Width = 340 };
            CmbInvoice.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblLine = new LabelControl { Text = "Item to return:", Location = new System.Drawing.Point(20, 75) };
            CmbLine = new ComboBoxEdit { Location = new System.Drawing.Point(20, 95), Width = 340 };
            CmbLine.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var lblQty = new LabelControl { Text = "Quantity to return:", Location = new System.Drawing.Point(20, 130) };
            SpinQuantity = new SpinEdit { Location = new System.Drawing.Point(20, 150), Width = 340, Value = 1 };
            SpinQuantity.Properties.MinValue = 1;

            using (var db = new ClothesShopDBContext())
            {
                var invoices = db.SalesInvoices
                .Where(x => x.BranchId == FrmLogin.CurrentBranchId)
                .OrderByDescending(x => x.InvoiceDate)
                .Take(50)
                .ToList();
                foreach (var inv in invoices)
                {
                    CmbInvoice.Properties.Items.Add($"{inv.InvoiceNumber} - {inv.InvoiceDate:d}");
                    _invoiceIds.Add(inv.Id);
                }
            }

            CmbInvoice.SelectedIndexChanged += (s, e) => LoadInvoiceLines();
            CmbLine.SelectedIndexChanged += (s, e) =>
            {
                if (CmbLine.SelectedIndex >= 0)
                    SpinQuantity.Properties.MaxValue = _lineMaxQty[CmbLine.SelectedIndex];
            };

            if (_invoiceIds.Count > 0)
            {
                CmbInvoice.SelectedIndex = 0;
                LoadInvoiceLines();
            }

            var btnSave = new SimpleButton { Text = "Save Return", Location = new System.Drawing.Point(180, 185), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                if (CmbInvoice.SelectedIndex < 0 || CmbLine.SelectedIndex < 0)
                {
                    XtraMessageBox.Show("Please select an invoice and an item.");
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(280, 185), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblInvoice); this.Controls.Add(CmbInvoice);
            this.Controls.Add(lblLine); this.Controls.Add(CmbLine);
            this.Controls.Add(lblQty); this.Controls.Add(SpinQuantity);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void LoadInvoiceLines()
        {
            CmbLine.Properties.Items.Clear();
            _lineVariantIds.Clear();
            _lineUnitPrices.Clear();
            _lineMaxQty.Clear();

            if (CmbInvoice.SelectedIndex < 0) return;
            int invoiceId = _invoiceIds[CmbInvoice.SelectedIndex];

            using (var db = new ClothesShopDBContext())
            {
                var lines = db.SalesInvoiceDetails
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.SalesInvoiceId == invoiceId)
                    .ToList();

                foreach (var l in lines)
                {
                
                    decimal alreadyReturned = db.SalesReturns
                        .Where(r => r.SalesInvoiceId == invoiceId)
                        .SelectMany(r => r.SalesReturnDetails)
                        .Where(d => d.ProductVariantId == l.ProductVariantId)
                        .Sum(d => (decimal?)d.Quantity) ?? 0;

                    decimal remaining = l.Quantity - alreadyReturned;

                    if (remaining <= 0) continue;   

                    CmbLine.Properties.Items.Add(
                        $"{l.ProductVariant.Product.Name} ({l.ProductVariant.Barcode})  Remaining: {remaining} of {l.Quantity}");
                    _lineVariantIds.Add(l.ProductVariantId);
                    _lineUnitPrices.Add(l.UnitPrice);
                    _lineMaxQty.Add(remaining);   
                }
            }
            if (CmbLine.Properties.Items.Count > 0) CmbLine.SelectedIndex = 0;
        }
    }
}