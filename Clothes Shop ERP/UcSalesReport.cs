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
    public partial class UcSalesReport : DevExpress.XtraEditors.XtraUserControl
    {
        public void ApplyLanguage()
        {
            LblSummary.Text = LocalizationManager.T("Reports_Summary");
            btnRun.Text = LocalizationManager.T("Reports_GenerateReport");
            lblTo.Text = LocalizationManager.T("Reports_To");
            lblFrom.Text = LocalizationManager.T("Reports_From");
        }
        public UcSalesReport()
        {
            InitializeComponent();
            DtFrom.DateTime = DateTime.Today.AddDays(-30);
            DtTo.DateTime = DateTime.Today;
            RunReport();
            ApplyLanguage();
            GridViewResult.OptionsView.ShowGroupPanel = false;
            GridViewResult.OptionsCustomization.AllowSort = false;


        }
        private void RunReport()
        {
            DateTime from = DtFrom.DateTime.Date;
            DateTime to = DtTo.DateTime.Date.AddDays(1).AddSeconds(-1);   // include the whole "To" day

            using (var db = new ClothesShopDBContext())
            {
                var invoices = db.SalesInvoices
                    .Include(x => x.Branch)
                    .Where(x => x.InvoiceDate >= from && x.InvoiceDate <= to)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new
                    {
                        x.InvoiceNumber,
                        Branch = x.Branch.Name,
                        x.InvoiceDate,
                        x.NetAmount
                    })
                    .ToList();

                GridResult.DataSource = invoices;

                decimal total = invoices.Sum(x => x.NetAmount);
                LblSummary.Text = $"Total: {total:n2}  |  Invoices: {invoices.Count}";
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
