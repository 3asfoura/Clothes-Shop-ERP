using Clothes_Shop_ERP.DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class UcProfitReport2 : DevExpress.XtraEditors.XtraUserControl
    {
        private DateEdit DtFrom, DtTo;
        private GridControl GridResult;
        private GridView GridViewResult;
        private LabelControl LblSummary;

        public UcProfitReport2()
        {
            this.Dock = DockStyle.Fill;
            BuildUi();
            RunReport();
        }

        private void BuildUi()
        {
            var lblFrom = new LabelControl { Text = "From:", Location = new System.Drawing.Point(20, 15) };
            DtFrom = new DateEdit { Location = new System.Drawing.Point(20, 35), Width = 150, DateTime = DateTime.Today.AddDays(-30) };

            var lblTo = new LabelControl { Text = "To:", Location = new System.Drawing.Point(190, 15) };
            DtTo = new DateEdit { Location = new System.Drawing.Point(190, 35), Width = 150, DateTime = DateTime.Today };

            var btnRun = new SimpleButton { Text = "Generate Report", Location = new System.Drawing.Point(360, 35), Width = 150 };
            btnRun.Click += (s, e) => RunReport();

            GridResult = new GridControl { Location = new System.Drawing.Point(20, 75), Size = new System.Drawing.Size(700, 350) };
            GridViewResult = new GridView(GridResult);
            GridResult.MainView = GridViewResult;
            GridViewResult.OptionsBehavior.Editable = false;

            LblSummary = new LabelControl
            {
                Text = "Revenue: 0.00  |  Cost: 0.00  |  Profit: 0.00  (0.0%)",
                Location = new System.Drawing.Point(20, 435),
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
            };

            this.Controls.Add(lblFrom); this.Controls.Add(DtFrom);
            this.Controls.Add(lblTo); this.Controls.Add(DtTo);
            this.Controls.Add(btnRun);
            this.Controls.Add(GridResult);
            this.Controls.Add(LblSummary);
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

                decimal totalRevenue = grouped.Sum(x => x.Revenue);
                decimal totalCost = grouped.Sum(x => x.Cost);
                decimal totalProfit = totalRevenue - totalCost;
                decimal margin = totalRevenue == 0 ? 0 : (totalProfit / totalRevenue) * 100;

                LblSummary.Text = $"Revenue: {totalRevenue:n2}  |  Cost: {totalCost:n2}  |  Profit: {totalProfit:n2}  ({margin:n1}%)";
            }
        }
    }
}

