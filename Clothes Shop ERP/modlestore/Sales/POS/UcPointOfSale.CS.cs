using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesInvoiceDetailEntity = Clothes_Shop_ERP.DAL.SalesInvoiceDetails;
using SalesInvoiceEntity = Clothes_Shop_ERP.DAL.SalesInvoices;
using StockMovementEntity = Clothes_Shop_ERP.DAL.StockMovements;
using TreasuryEntity = Clothes_Shop_ERP.DAL.TreasuryTransactions;
namespace Clothes_Shop_ERP
{
    public partial class UcPointOfSale : DevExpress.XtraEditors.XtraUserControl
    {
        public class CartLine
        {
            public int ProductVariantId { get; set; }
            public string ProductDisplay { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Quantity { get; set; }
            public decimal LineTotal => UnitPrice * Quantity;
        }

        private List<int> _variantIds = new List<int>();
        private List<int?> _customerIds = new List<int?>();
        private List<int> _paymentMethodIds = new List<int>();
        private BindingList<CartLine> _cart = new BindingList<CartLine>();
        public UcPointOfSale()
        {

            InitializeComponent();
            ApplyLanguage();
            BuildUi();
            LoadLookups();
            TxtBarcode.Focus();
            GridViewCart.OptionsView.ShowGroupPanel = false;
            GridViewCart.OptionsCustomization.AllowSort = false;
            Sett.CenterColumns(GridViewCart);
        }
        public void ApplyLanguage()
        {
            btnAddManual.Text = LocalizationManager.T("POS_BtnAddManual");
            layoutControlItem2.Text = LocalizationManager.T("POS_PickManually");
            layoutControlItem1.Text = LocalizationManager.T("POS_ScanBarcode");
            btnRemoveLine.Text = LocalizationManager.T("POS_BtnRemoveLine");
            SimpleButton.Text = LocalizationManager.T("POS_BtnCheckout");
            lblCustomer.Text = LocalizationManager.T("POS_Customer");
            lblPayment.Text = LocalizationManager.T("POS_PaymentMethod");
            lblDiscount.Text = LocalizationManager.T("POS_Discount");
        }
        private void LoadLookups()
        {
            using (var db = new ClothesShopDBContext())
            {
                int branchId = FrmLogin.CurrentBranchId;

                var availableVariants = db.BranchStock
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product).ThenInclude(p => p.Category)
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Color)
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Size)
                    .Where(x => x.BranchId == branchId
                     && x.Quantity > 0
                     && x.ProductVariant.IsActive == true
                     && x.ProductVariant.Product.IsActive == true
                     && x.ProductVariant.Product.Category.IsActive == true)
                    .ToList();

                foreach (var stock in availableVariants)
                {
                    var v = stock.ProductVariant;
                    CmbVariant.Properties.Items.Add(
                        $"{v.Product.Name} - {v.Color.Name} - {v.Size.Name} - {v.Barcode} (Qty: {stock.Quantity})");
                    _variantIds.Add(v.Id);
                }

                CmbCustomer.Properties.Items.Add(LocalizationManager.T("POS_WalkInCustomer"));
                _customerIds.Add(null);
                foreach (var c in db.Customers.Where(x => x.IsActive == true).ToList())
                {
                    CmbCustomer.Properties.Items.Add(c.Name);
                    _customerIds.Add(c.Id);
                }
                CmbCustomer.SelectedIndex = 0;

                foreach (var p in db.PaymentMethods.ToList())
                {
                    CmbPaymentMethod.Properties.Items.Add(p.Name);
                    _paymentMethodIds.Add(p.Id);
                }
                if (_paymentMethodIds.Count > 0) CmbPaymentMethod.SelectedIndex = 0;
            }
        }

        private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string code = TxtBarcode.Text.Trim();
            TxtBarcode.Text = "";
            if (string.IsNullOrEmpty(code)) return;

            using (var db = new ClothesShopDBContext())
            {
                var variant = db.ProductVariants
      .Include(x => x.Product).ThenInclude(p => p.Category)
      .FirstOrDefault(v => v.Barcode == code
                         && v.IsActive == true
                         && v.Product.IsActive == true
                         && v.Product.Category.IsActive == true);   

                if (variant == null)
                {
                    Sett.MsgBlue(LocalizationManager.T("POS_NotFoundTitle"), string.Format(LocalizationManager.T("POS_ProductNotFoundByBarcode"), code));
                    return;
                }

                AddToCart(variant.Id, 1);
            }
        }
        private void AddToCart(int variantId, decimal quantity)
        {
            var existing = _cart.FirstOrDefault(l => l.ProductVariantId == variantId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                GridViewCart.RefreshData();
            }
            else
            {
                using (var db = new ClothesShopDBContext())
                {
                    var variant = db.ProductVariants.Include(x => x.Product).First(v => v.Id == variantId);
                    _cart.Add(new CartLine
                    {
                        ProductVariantId = variant.Id,
                        ProductDisplay = $"{variant.Product.Name} ({variant.Barcode})",
                        UnitPrice = variant.SalePrice,
                        Quantity = quantity
                    });
                }
            }
            RefreshTotal();
        }

        private void RefreshTotal()
        {
            decimal subTotal = _cart.Sum(l => l.LineTotal);
            decimal net = subTotal - (decimal)SpinDiscount.Value;
            LblTotal.Text = string.Format(LocalizationManager.T("POS_TotalFmt"), net);
        }

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanEdit("PointOfSale"))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shared_NoPermissionMsg"));
                return;
            }

            if (_cart.Count == 0)
            {
                Sett.MsgBlue(LocalizationManager.T("POS_EmptyCartTitle"), LocalizationManager.T("POS_EmptyCartMsg"));
                return;
            }

            decimal subTotal = _cart.Sum(l => l.LineTotal);
            decimal discount = (decimal)SpinDiscount.Value;

            if (discount > subTotal)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("POS_DiscountExceedsTotal"));
                return;
            }

            decimal netTotal = subTotal - discount;
            int branchId = FrmLogin.CurrentBranchId;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    // Safely decrement stock for every line first — if any line fails
                    // because stock ran out, roll back everything and stop.
                    foreach (var line in _cart)
                    {
                        int rowsAffected = db.Database.ExecuteSqlCommand(
                            "UPDATE BranchStock SET Quantity = Quantity - {0} WHERE ProductVariantId = {1} AND BranchId = {2} AND Quantity >= {0}",
                            line.Quantity, line.ProductVariantId, branchId);

                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            Sett.MsgBlue(LocalizationManager.T("POS_OutOfStockTitle"), string.Format(LocalizationManager.T("POS_NotEnoughStockFor"), line.ProductDisplay));
                            return;
                        }
                    }

                    var invoice = new SalesInvoiceEntity
                    {
                        InvoiceNumber = "INV" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        CustomerId = _customerIds[CmbCustomer.SelectedIndex],
                        BranchId = branchId,
                        InvoiceDate = DateTime.Now,
                        TotalAmount = subTotal,
                        DiscountAmount = discount,
                        TaxAmount = 0,
                        NetAmount = netTotal,
                        PaidAmount = netTotal,
                        PaymentMethodId = _paymentMethodIds[CmbPaymentMethod.SelectedIndex],
                        Status = "Completed",
                        CreatedByUserId = FrmLogin.CurrentUserId
                    };
                    db.SalesInvoices.Add(invoice);
                    db.SaveChanges();   // generates invoice.Id for the lines below

                    foreach (var line in _cart)
                    {
                        db.SalesInvoiceDetails.Add(new SalesInvoiceDetailEntity
                        {
                            SalesInvoiceId = invoice.Id,
                            ProductVariantId = line.ProductVariantId,
                            Quantity = line.Quantity,
                            UnitPrice = line.UnitPrice,
                            DiscountAmount = 0,
                            Total = line.LineTotal
                        });

                        db.StockMovements.Add(new StockMovementEntity
                        {
                            ProductVariantId = line.ProductVariantId,
                            BranchId = branchId,
                            MovementType = "Sale",
                            Quantity = -line.Quantity,
                            RefType = "SalesInvoice",
                            RefId = invoice.Id,
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = FrmLogin.CurrentUserId
                        });
                    }

                    db.TreasuryTransactions.Add(new TreasuryEntity
                    {
                        BranchId = branchId,
                        TransactionType = "In",
                        Amount = netTotal,
                        Description = $"Sale - {invoice.InvoiceNumber}",
                        RefType = "SalesInvoice",
                        RefId = invoice.Id,
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgGreen(LocalizationManager.T("POS_SaleCompletedTitle"), string.Format(LocalizationManager.T("POS_SaleCompletedMsg"), invoice.InvoiceNumber, netTotal));

                    var branchInfo = db.Branches.Where(b => b.Id == branchId)
                        .Select(b => new { b.Name, b.Address, b.Phone }).FirstOrDefault();
                    var receipt = new ReceiptData
                    {
                        ShopName = branchInfo?.Name,
                        ShopAddress = branchInfo?.Address,
                        ShopPhone = branchInfo?.Phone,
                        InvoiceNumber = invoice.InvoiceNumber,
                        Date = invoice.InvoiceDate,
                        Customer = CmbCustomer.Text,
                        Cashier = FrmLogin.CurrentUserFullName,
                        PaymentMethod = CmbPaymentMethod.Text,
                        SubTotal = subTotal,
                        Discount = discount,
                        NetTotal = netTotal,
                        Lines = _cart.Select(l => new ReceiptLine
                        {
                            Product = l.ProductDisplay,
                            Quantity = l.Quantity,
                            UnitPrice = l.UnitPrice,
                            LineTotal = l.LineTotal
                        }).ToList()
                    };
                    ReceiptPrinter.Print(receipt);

                    _cart.Clear();
                    SpinDiscount.Value = 0;
                    RefreshTotal();
                    TxtBarcode.Focus();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("POS_SaleFailed"), ex.Message));
                }
            }
        }

        private void UcPointOfSale_Load(object sender, EventArgs e)
        {

        }

        private void btnAddManual_Click(object sender, EventArgs e)
        {
            if (CmbVariant.SelectedIndex < 0) return;
            AddToCart(_variantIds[CmbVariant.SelectedIndex], (decimal)SpinManualQty.Value);
        }

        private void btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (GridViewCart.FocusedRowHandle < 0) return;
            var line = GridViewCart.GetFocusedRow() as CartLine;
            if (line != null) { _cart.Remove(line); RefreshTotal(); }
        }
        private void BuildUi()
        {

            GridCart.DataSource = _cart;
            if (GridViewCart.Columns["Quantity"] != null)
            {
                GridViewCart.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GridViewCart.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
                GridViewCart.Columns["Quantity"].Caption = LocalizationManager.T("StockCount_ColQuantity");
            }
            if (GridViewCart.Columns["ProductDisplay"] != null)
            {
                GridViewCart.Columns["ProductDisplay"].Caption = LocalizationManager.T("StockCount_ColProduct");
                GridViewCart.Columns["ProductDisplay"].OptionsColumn.AllowEdit = false;
            }
            if (GridViewCart.Columns["UnitPrice"] != null)
            {
                GridViewCart.Columns["UnitPrice"].Caption = LocalizationManager.T("POS_ColUnitPrice");
                GridViewCart.Columns["UnitPrice"].OptionsColumn.AllowEdit = false;
            }
            if (GridViewCart.Columns["LineTotal"] != null)
            {
                GridViewCart.Columns["LineTotal"].Caption = LocalizationManager.T("Shared_ColTotal");
                GridViewCart.Columns["LineTotal"].OptionsColumn.AllowEdit = false;
            }

            // The grid itself is editable so a scanned/added line's quantity can be
            // corrected directly (e.g. scanned once but meant 3) - every other
            // column stays locked via AllowEdit above.
            GridViewCart.OptionsBehavior.Editable = true;
            GridViewCart.CellValueChanged += (s, e) =>
            {
                if (e.Column == GridViewCart.Columns["Quantity"]) RefreshTotal();
            };

            RefreshTotal();

            SpinDiscount.ValueChanged += (s, e) => RefreshTotal();

        }
        private void Loop_Tick(object sender, EventArgs e)
        {

        }
    }
}
