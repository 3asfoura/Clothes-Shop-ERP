using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            PurchaseInvoices = new HashSet<PurchaseInvoices>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<PurchaseInvoices> PurchaseInvoices { get; set; }
    }
}
