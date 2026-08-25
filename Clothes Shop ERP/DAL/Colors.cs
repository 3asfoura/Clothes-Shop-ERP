using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Colors
    {
        public Colors()
        {
            ProductVariants = new HashSet<ProductVariants>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }

        public ICollection<ProductVariants> ProductVariants { get; set; }
    }
}
