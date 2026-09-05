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
            btnRun.Text = LocalizationManager.T("Reports_GenerateReport");
            lblTo.Text = LocalizationManager.T("Shared_To");
            lblFrom.Text = LocalizationManager.T("Shared_From");
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
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["InvoiceNumber"] != null) GridViewResult.Columns["InvoiceNumber"].Caption = LocalizationManager.T("SalesInvoices_ColInvoiceNumber");
                if (GridViewResult.Columns["Branch"] != null) GridViewResult.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
                if (GridViewResult.Columns["InvoiceDate"] != null) GridViewResult.Columns["InvoiceDate"].Caption = LocalizationManager.T("Purchases_ColInvoiceDate");
                if (GridViewResult.Columns["NetAmount"] != null) GridViewResult.Columns["NetAmount"].Caption = LocalizationManager.T("SalesInvoices_ColNetAmount");

                decimal total = invoices.Sum(x => x.NetAmount);
                LblSummary.Text = string.Format(LocalizationManager.T("Reports_SummaryFmt"), total, invoices.Count);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
