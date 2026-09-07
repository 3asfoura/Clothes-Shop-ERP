using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class PurchaseInvoiceDetails
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }

        public ProductVariants ProductVariant { get; set; }
        public PurchaseInvoices PurchaseInvoice { get; set; }
    }
}
