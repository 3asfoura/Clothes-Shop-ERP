using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class TreasuryTransactions
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string RefType { get; set; }
        public int? RefId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
    }
}
