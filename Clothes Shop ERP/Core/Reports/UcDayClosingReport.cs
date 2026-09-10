using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    // End-of-day / Z-Report: sales, returns, and every Treasury cash movement for one branch/day.
    public partial class UcDayClosingReport : DevExpress.XtraEditors.XtraUserControl
    {
        private TableLayoutPanel _cardsPanel;
        private LabelControl _lblBreakdown;

        public UcDayClosingReport()
        {
            InitializeComponent();
            DtDate.DateTime = DateTime.Today;
            ApplyLanguage();
            // LayoutControl won't align btnRun with DtDate's row on its own - pinned directly instead.
            btnRun.LocationChanged += (s, e) => SyncButtonToDateRow();
            btnRun.SizeChanged += (s, e) => SyncButtonToDateRow();
            SyncButtonToDateRow();
            // Deferred to Load - PopulateColumns needs a real window handle.
            this.Load += (s, e) => { BuildSummaryUi(); RunReport(); };
        }

        private void SyncButtonToDateRow()
        {
            if (btnRun.Top == DtDate.Top && btnRun.Height == DtDate.Height) return;
            btnRun.Top = DtDate.Top;
            btnRun.Height = DtDate.Height;
        }

        // Same KPI-tile look as the Dashboard cards.
        private void BuildSummaryUi()
        {
            // AutoSize avoids drift between the Designer's DPI-scaled Size and code-set child heights.
            pnlSummaryHost.AutoSize = true;
            pnlSummaryHost.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _cardsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 160,
                ColumnCount = 3,
                RowCount = 2
            };
            for (int i = 0; i < 3; i++) _cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 3));
            for (int i = 0; i < 2; i++) _cardsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            pnlSummaryHost.Controls.Add(_cardsPanel);

            _lblBreakdown = new LabelControl
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.Gray,
                Padding = new Padding(12, 6, 12, 0)
            };
            pnlSummaryHost.Controls.Add(_lblBreakdown);
            _lblBreakdown.BringToFront();
        }

        private static PanelControl MakeCard(string title, string value, Color accentColor)
        {
            var card = new PanelControl
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(6),
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            };
            card.Appearance.BackColor = Color.White;
            card.Appearance.Options.UseBackColor = true;

            var lblTitle = new LabelControl
            {
                Text = title,
                Location = new Point(12, 10),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            var lblValue = new LabelControl
            {
                Text = value,
                Location = new Point(12, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = accentColor
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            return card;
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

                _cardsPanel.Controls.Clear();
                Color netCashColor = netCashMovement >= 0 ? Color.SeaGreen : Color.Crimson;

                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardInvoiceCount"), invoiceCount.ToString("n0"), Color.MediumPurple), 0, 0);
                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardReturnCount"), returnCount.ToString("n0"), Color.DarkOrange), 1, 0);
                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardNetSales"), netSales.ToString("n2"), Color.MediumPurple), 2, 0);
                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardTotalIn"), totalCashIn.ToString("n2"), Color.SeaGreen), 0, 1);
                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardTotalOut"), totalCashOut.ToString("n2"), Color.Crimson), 1, 1);
                _cardsPanel.Controls.Add(MakeCard(LocalizationManager.T("DayClosing_CardNetCash"), netCashMovement.ToString("n2"), netCashColor), 2, 1);

                _lblBreakdown.Text = string.Format(LocalizationManager.T("DayClosing_BreakdownFmt"),
                    cashInFromSales, otherCashIn, cashOutToSuppliers, refunds, generalExpenses);

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

     

        private void btnRun_Click_1(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}
