using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Clothes_Shop_ERP.modlestore
{
    // End-of-day / Z-Report: everything that happened at this branch on one
    // calendar day - sales, returns, and every cash movement in the Treasury,
    // broken down by where it came from or went to.
    public partial class UcDayClosingReport : DevExpress.XtraEditors.XtraUserControl
    {
        public UcDayClosingReport()
        {
            InitializeComponent();
            DtDate.DateTime = DateTime.Today;
            ApplyLanguage();
            RunReport();
        }

        public void ApplyLanguage()
        {
            lblDate.Text = LocalizationManager.T("DayClosing_Date");
            btnRun.Text = LocalizationManager.T("Reports_GenerateReport");
        }

        private void RunReport()
        {
            DateTime from = DtDate.DateTime.Date;
            DateTime to = from.AddDays(1).AddSeconds(-1);
            int branchId = FrmLogin.CurrentBranchId;

            using (var db = new ClothesShopDBContext())
            {
                var sales = db.SalesInvoices
                    .Include(x => x.PaymentMethod)
                    .Where(x => x.BranchId == branchId && x.InvoiceDate >= from && x.InvoiceDate <= to
                             && x.Status == "Completed")
                    .ToList();

                var returns = db.SalesReturns
                    .Where(x => x.BranchId == branchId && x.ReturnDate >= from && x.ReturnDate <= to)
                    .ToList();

                var treasury = db.TreasuryTransactions
                    .Where(x => x.BranchId == branchId && x.CreatedAt >= from && x.CreatedAt <= to)
                    .ToList();

                int invoiceCount = sales.Count;
                decimal totalSales = sales.Sum(x => x.NetAmount);
                int returnCount = returns.Count;
                decimal totalReturns = returns.Sum(x => x.TotalAmount);
                decimal netSales = totalSales - totalReturns;

                decimal cashInFromSales = treasury.Where(x => x.TransactionType == "In" && x.RefType == "SalesInvoice").Sum(x => x.Amount);
                decimal otherCashIn = treasury.Where(x => x.TransactionType == "In" && x.RefType != "SalesInvoice").Sum(x => x.Amount);
                decimal totalCashIn = cashInFromSales + otherCashIn;

                decimal cashOutToSuppliers = treasury.Where(x => x.TransactionType == "Out" && x.RefType == "PurchaseInvoice").Sum(x => x.Amount);
                decimal refunds = treasury.Where(x => x.TransactionType == "Out" && x.RefType == "SalesReturn").Sum(x => x.Amount);
                decimal generalExpenses = treasury.Where(x => x.TransactionType == "Out" && x.RefType == "Manual").Sum(x => x.Amount);
                decimal totalCashOut = cashOutToSuppliers + refunds + generalExpenses;

                decimal netCashMovement = totalCashIn - totalCashOut;

                LblSummary.Text = string.Format(LocalizationManager.T("DayClosing_SummaryFmt"),
                    invoiceCount, totalSales, returnCount, totalReturns, netSales,
                    cashInFromSales, otherCashIn, totalCashIn,
                    cashOutToSuppliers, refunds, generalExpenses, totalCashOut,
                    netCashMovement);

                var byMethod = sales
                    .GroupBy(x => x.PaymentMethod != null ? x.PaymentMethod.Name : "-")
                    .Select(g => new { Method = g.Key, Count = g.Count(), Total = g.Sum(x => x.NetAmount) })
                    .OrderByDescending(x => x.Total)
                    .ToList();

                GridResult.DataSource = byMethod;
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["Method"] != null) GridViewResult.Columns["Method"].Caption = LocalizationManager.T("DayClosing_ColMethod");
                if (GridViewResult.Columns["Count"] != null) GridViewResult.Columns["Count"].Caption = LocalizationManager.T("DayClosing_ColCount");
                if (GridViewResult.Columns["Total"] != null) GridViewResult.Columns["Total"].Caption = LocalizationManager.T("Shared_ColTotal");
                Sett.CenterColumns(GridViewResult);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
