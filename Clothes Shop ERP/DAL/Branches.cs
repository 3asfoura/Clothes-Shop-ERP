using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Branches
    {
        public Branches()
        {
            BranchStock = new HashSet<BranchStock>();
            PurchaseInvoices = new HashSet<PurchaseInvoices>();
            SalesInvoices = new HashSet<SalesInvoices>();
            SalesReturns = new HashSet<SalesReturns>();
            StockMovements = new HashSet<StockMovements>();
            StockTransfersFromBranch = new HashSet<StockTransfers>();
            StockTransfersToBranch = new HashSet<StockTransfers>();
            TreasuryTransactions = new HashSet<TreasuryTransactions>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<BranchStock> BranchStock { get; set; }
        public ICollection<PurchaseInvoices> PurchaseInvoices { get; set; }
        public ICollection<SalesInvoices> SalesInvoices { get; set; }
        public ICollection<SalesReturns> SalesReturns { get; set; }
        public ICollection<StockMovements> StockMovements { get; set; }
        public ICollection<StockTransfers> StockTransfersFromBranch { get; set; }
        public ICollection<StockTransfers> StockTransfersToBranch { get; set; }
        public ICollection<TreasuryTransactions> TreasuryTransactions { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
