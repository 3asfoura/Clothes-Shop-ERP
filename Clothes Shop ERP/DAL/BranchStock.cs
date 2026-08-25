using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class BranchStock
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public int BranchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal MinQuantity { get; set; }

        public Branches Branch { get; set; }
        public ProductVariants ProductVariant { get; set; }
    }
}
