using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcProfitReport : DevExpress.XtraEditors.XtraUserControl
    {
        public UcProfitReport()
        {
            InitializeComponent();
            DtFrom.DateTime = DateTime.Today.AddDays(-30);
            DtTo.DateTime = DateTime.Today;
            RunReport();
            ApplyLanguage();
            GridViewResult.OptionsView.ShowGroupPanel = false;
            GridViewResult.OptionsCustomization.AllowSort = false;
            Sett.CenterColumns(GridViewResult);
        }
        public void ApplyLanguage()
        {
            btnRun.Text = LocalizationManager.T("Reports_GenerateReport");
            lblTo.Text = LocalizationManager.T("Shared_To");
            lblFrom.Text = LocalizationManager.T("Shared_From");
            lblExpensesHintItem.Text = LocalizationManager.T("ProfitReport_GeneralExpensesHint");
        }
        private void RunReport()
        {
            DateTime from = DtFrom.DateTime.Date;
            DateTime to = DtTo.DateTime.Date.AddDays(1).AddSeconds(-1);

            using (var db = new ClothesShopDBContext())
            {
                // Every sold line within the date range, with its product name and
                // the variant's current cost price (see the note below about this).
                var soldLines = db.SalesInvoiceDetails
                    .Include(x => x.SalesInvoice)
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.SalesInvoice.InvoiceDate >= from && x.SalesInvoice.InvoiceDate <= to
                             && x.SalesInvoice.Status == "Completed")
                    .ToList();

                var grouped = soldLines
                    .GroupBy(x => x.ProductVariant.Product.Name)
                    .Select(g => new
                    {
                        Product = g.Key,
                        QuantitySold = g.Sum(x => x.Quantity),
                        Revenue = g.Sum(x => x.Total),
                        Cost = g.Sum(x => x.Quantity * x.ProductVariant.CostPrice),
                        Profit = g.Sum(x => x.Total) - g.Sum(x => x.Quantity * x.ProductVariant.CostPrice)
                    })
                    .OrderByDescending(x => x.Profit)
                    .ToList();

                GridResult.DataSource = grouped;
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["Quantity"] != null)
                {
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
                }
                if (GridViewResult.Columns["Product"] != null) GridViewResult.Columns["Product"].Caption = LocalizationManager.T("StockCount_ColProduct");
                if (GridViewResult.Columns["QuantitySold"] != null) GridViewResult.Columns["QuantitySold"].Caption = LocalizationManager.T("ProfitReport_ColQuantitySold");
                if (GridViewResult.Columns["Revenue"] != null) GridViewResult.Columns["Revenue"].Caption = LocalizationManager.T("ProfitReport_ColRevenue");
                if (GridViewResult.Columns["Cost"] != null) GridViewResult.Columns["Cost"].Caption = LocalizationManager.T("ProfitReport_ColCost");
                if (GridViewResult.Columns["Profit"] != null) GridViewResult.Columns["Profit"].Caption = LocalizationManager.T("ProfitReport_ColProfit");
                decimal totalRevenue = grouped.Sum(x => x.Revenue);
                decimal totalCost = grouped.Sum(x => x.Cost);
                decimal grossProfit = totalRevenue - totalCost;
                decimal margin = totalRevenue == 0 ? 0 : (grossProfit / totalRevenue) * 100;

                // General expenses: manual Treasury entries (electricity, rent, salaries...) that
                // aren't tied to a specific sale/purchase, so they don't show up in the per-product
                // cost above. Net profit = gross profit from goods minus these overhead costs.
                decimal generalExpenses = db.TreasuryTransactions
                    .Where(x => x.TransactionType == "Out" && x.RefType == "Manual"
                             && x.CreatedAt >= from && x.CreatedAt <= to)
                    .Sum(x => (decimal?)x.Amount) ?? 0;
                decimal netProfit = grossProfit - generalExpenses;

                LblSummary.Text = string.Format(LocalizationManager.T("ProfitReport_NetSummaryFmt"),
                    totalRevenue, totalCost, grossProfit, margin, generalExpenses, netProfit);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
