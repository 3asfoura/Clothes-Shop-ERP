using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Categories
    {
        public Categories()
        {
            InverseParentCategory = new HashSet<Categories>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsActive { get; set; }

        public Categories ParentCategory { get; set; }
        public ICollection<Categories> InverseParentCategory { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
