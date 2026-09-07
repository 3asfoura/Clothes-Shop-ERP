using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Sizes
    {
        public Sizes()
        {
            ProductVariants = new HashSet<ProductVariants>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public ICollection<ProductVariants> ProductVariants { get; set; }
    }
}
