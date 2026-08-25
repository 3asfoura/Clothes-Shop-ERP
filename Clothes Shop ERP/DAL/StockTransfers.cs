using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class StockTransfers
    {
        public StockTransfers()
        {
            StockTransferDetails = new HashSet<StockTransferDetails>();
        }

        public int Id { get; set; }
        public int FromBranchId { get; set; }
        public int ToBranchId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }

        public Users CreatedByUser { get; set; }
        public Branches FromBranch { get; set; }
        public Branches ToBranch { get; set; }
        public ICollection<StockTransferDetails> StockTransferDetails { get; set; }
    }
}
