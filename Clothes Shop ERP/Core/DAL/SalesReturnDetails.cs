using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class SalesReturnDetails
    {
        public int Id { get; set; }
        public int SalesReturnId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        public ProductVariants ProductVariant { get; set; }
        public SalesReturns SalesReturn { get; set; }
    }
}
