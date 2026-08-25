using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class PurchaseInvoices
    {
        public PurchaseInvoices()
        {
            PurchaseInvoiceDetails = new HashSet<PurchaseInvoiceDetails>();
        }

        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int BranchId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Status { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
        public Suppliers Supplier { get; set; }
        public ICollection<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
    }
}
