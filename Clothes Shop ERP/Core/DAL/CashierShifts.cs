using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class CashierShifts
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public DateTime OpenedAt { get; set; }
        public decimal OpeningFloat { get; set; }
        public DateTime? ClosedAt { get; set; }
        public decimal? ExpectedCash { get; set; }
        public decimal? CountedCash { get; set; }
        public decimal? Difference { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public Branches Branch { get; set; }
        public Users User { get; set; }
    }
}
