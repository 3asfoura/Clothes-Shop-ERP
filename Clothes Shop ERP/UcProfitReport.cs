using Clothes_Shop_ERP.DAL;
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
            GridViewResult.OptionsView.ShowGroupPanel = false;
            GridViewResult.OptionsCustomization.AllowSort = false;
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
                if (GridViewResult.Columns["Quantity"] != null)
                {
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
                }
                decimal totalRevenue = grouped.Sum(x => x.Revenue);
                decimal totalCost = grouped.Sum(x => x.Cost);
                decimal totalProfit = totalRevenue - totalCost;
                decimal margin = totalRevenue == 0 ? 0 : (totalProfit / totalRevenue) * 100;

                LblSummary.Text = $"Revenue: {totalRevenue:n2}  |  Cost: {totalCost:n2}  |  Profit: {totalProfit:n2}  ({margin:n1}%)";
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
