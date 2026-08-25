using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class SalesInvoices
    {
        public SalesInvoices()
        {
            SalesInvoiceDetails = new HashSet<SalesInvoiceDetails>();
            SalesReturns = new HashSet<SalesReturns>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public int BranchId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string Status { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
        public Customers Customer { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ICollection<SalesInvoiceDetails> SalesInvoiceDetails { get; set; }
        public ICollection<SalesReturns> SalesReturns { get; set; }
    }
}
