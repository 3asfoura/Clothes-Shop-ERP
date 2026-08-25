using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class PaymentMethods
    {
        public PaymentMethods()
        {
            SalesInvoices = new HashSet<SalesInvoices>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SalesInvoices> SalesInvoices { get; set; }
    }
}
