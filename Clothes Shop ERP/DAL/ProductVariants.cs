using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class ProductVariants
    {
        public ProductVariants()
        {
            BranchStock = new HashSet<BranchStock>();
            PurchaseInvoiceDetails = new HashSet<PurchaseInvoiceDetails>();
            SalesInvoiceDetails = new HashSet<SalesInvoiceDetails>();
            SalesReturnDetails = new HashSet<SalesReturnDetails>();
            StockMovements = new HashSet<StockMovements>();
            StockTransferDetails = new HashSet<StockTransferDetails>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string Barcode { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool? IsActive { get; set; }

        public Colors Color { get; set; }
        public Products Product { get; set; }
        public Sizes Size { get; set; }
        public ICollection<BranchStock> BranchStock { get; set; }
        public ICollection<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
        public ICollection<SalesInvoiceDetails> SalesInvoiceDetails { get; set; }
        public ICollection<SalesReturnDetails> SalesReturnDetails { get; set; }
        public ICollection<StockMovements> StockMovements { get; set; }
        public ICollection<StockTransferDetails> StockTransferDetails { get; set; }
    }
}
