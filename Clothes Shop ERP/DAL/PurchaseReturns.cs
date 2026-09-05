using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class PurchaseReturns
    {
        public PurchaseReturns()
        {
            PurchaseReturnDetails = new HashSet<PurchaseReturnDetails>();
        }

        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int BranchId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
        public PurchaseInvoices PurchaseInvoice { get; set; }
        public ICollection<PurchaseReturnDetails> PurchaseReturnDetails { get; set; }
    }
}
