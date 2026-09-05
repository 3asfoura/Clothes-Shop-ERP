using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class PurchaseReturnDetails
    {
        public int Id { get; set; }
        public int PurchaseReturnId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }

        public ProductVariants ProductVariant { get; set; }
        public PurchaseReturns PurchaseReturn { get; set; }
    }
}
