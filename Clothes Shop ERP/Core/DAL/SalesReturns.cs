using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class SalesReturns
    {
        public SalesReturns()
        {
            SalesReturnDetails = new HashSet<SalesReturnDetails>();
        }

        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public int BranchId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
        public SalesInvoices SalesInvoice { get; set; }
        public ICollection<SalesReturnDetails> SalesReturnDetails { get; set; }
    }
}
