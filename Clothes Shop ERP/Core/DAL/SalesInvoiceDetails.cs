using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class SalesInvoiceDetails
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }

        public ProductVariants ProductVariant { get; set; }
        public SalesInvoices SalesInvoice { get; set; }
    }
}
