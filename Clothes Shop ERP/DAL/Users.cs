using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Users
    {
        public Users()
        {
            PurchaseInvoices = new HashSet<PurchaseInvoices>();
            PurchaseReturns = new HashSet<PurchaseReturns>();
            SalesInvoices = new HashSet<SalesInvoices>();
            SalesReturns = new HashSet<SalesReturns>();
            StockMovements = new HashSet<StockMovements>();
            StockTransfers = new HashSet<StockTransfers>();
            TreasuryTransactions = new HashSet<TreasuryTransactions>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public int? BranchId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Branches Branch { get; set; }
        public Roles Role { get; set; }
        public ICollection<PurchaseInvoices> PurchaseInvoices { get; set; }
        public ICollection<PurchaseReturns> PurchaseReturns { get; set; }
        public ICollection<SalesInvoices> SalesInvoices { get; set; }
        public ICollection<SalesReturns> SalesReturns { get; set; }
        public ICollection<StockMovements> StockMovements { get; set; }
        public ICollection<StockTransfers> StockTransfers { get; set; }
        public ICollection<TreasuryTransactions> TreasuryTransactions { get; set; }
    }
}
