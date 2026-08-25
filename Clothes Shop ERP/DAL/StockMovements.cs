using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class StockMovements
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public int BranchId { get; set; }
        public string MovementType { get; set; }
        public decimal Quantity { get; set; }
        public string RefType { get; set; }
        public int? RefId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }

        public Branches Branch { get; set; }
        public Users CreatedByUser { get; set; }
        public ProductVariants ProductVariant { get; set; }
    }
}
