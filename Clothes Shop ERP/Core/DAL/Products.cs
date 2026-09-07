using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Products
    {
        public Products()
        {
            ProductVariants = new HashSet<ProductVariants>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public decimal BasePrice { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Brands Brand { get; set; }
        public Categories Category { get; set; }
        public ICollection<ProductVariants> ProductVariants { get; set; }
    }
}
