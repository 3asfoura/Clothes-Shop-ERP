using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clothes_Shop_ERP.modlestore
{
    // Shows every invoice for one customer or supplier (paid, partially paid,
    // or fully credit), with running totals — so you can see at a glance how
    // much a given customer/supplier still owes or is owed.
    public partial class UcAccountStatement : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly List<int> _partyIds = new List<int>();

        public UcAccountStatement()
        {
            InitializeComponent();
            DtFrom.DateTime = DateTime.Today.AddMonths(-3);
            DtTo.DateTime = DateTime.Today;

            CmbType.Properties.Items.Add(LocalizationManager.T("AccountStatement_TypeCustomer"));
            CmbType.Properties.Items.Add(LocalizationManager.T("AccountStatement_TypeSupplier"));
            CmbType.SelectedIndex = 0;

            ApplyLanguage();
            LoadParties();
            RunReport();
        }

        public void ApplyLanguage()
        {
            lblType.Text = LocalizationManager.T("FrmTreasuryEdit_Type");
            lblParty.Text = LocalizationManager.T("Shared_ColName");
            lblFrom.Text = LocalizationManager.T("Shared_From");
            lblTo.Text = LocalizationManager.T("Shared_To");
            btnRun.Text = LocalizationManager.T("Reports_GenerateReport");
        }

        private bool IsSupplier => CmbType.SelectedIndex == 1;

        private void LoadParties()
        {
            int previousId = _partyIds.Count > 0 && CmbParty.SelectedIndex >= 0 ? _partyIds[CmbParty.SelectedIndex] : 0;

            CmbParty.Properties.Items.Clear();
            _partyIds.Clear();

            using (var db = new ClothesShopDBContext())
            {
                if (IsSupplier)
                {
                    foreach (var s in db.Suppliers.OrderBy(x => x.Name).ToList())
                    {
                        CmbParty.Properties.Items.Add(s.Name);
                        _partyIds.Add(s.Id);
                    }
                }
                else
                {
                    foreach (var c in db.Customers.OrderBy(x => x.Name).ToList())
                    {
                        CmbParty.Properties.Items.Add(c.Name);
                        _partyIds.Add(c.Id);
                    }
                }
            }

            int idx = _partyIds.IndexOf(previousId);
            CmbParty.SelectedIndex = idx >= 0 ? idx : (_partyIds.Count > 0 ? 0 : -1);
        }

        private void RunReport()
        {
            if (CmbParty.SelectedIndex < 0)
            {
                GridResult.DataSource = null;
                LblSummary.Text = string.Format(LocalizationManager.T("AccountStatement_SummaryFmt"), 0m, 0m, 0m);
                return;
            }

            int partyId = _partyIds[CmbParty.SelectedIndex];
            DateTime from = DtFrom.DateTime.Date;
            DateTime to = DtTo.DateTime.Date.AddDays(1).AddSeconds(-1);

            using (var db = new ClothesShopDBContext())
            {
                // Both branches project into the exact same shape so the grid
                // (and its column captions below) can stay a single code path.
                var rows = IsSupplier
                    ? db.PurchaseInvoices
                        .Where(x => x.SupplierId == partyId && x.InvoiceDate >= from && x.InvoiceDate <= to)
                        .OrderByDescending(x => x.InvoiceDate)
                        .Select(x => new
                        {
                            Reference = "PUR-" + x.Id,
                            x.InvoiceDate,
                            TotalAmount = x.TotalAmount,
                            x.PaidAmount,
                            Due = x.TotalAmount - x.PaidAmount,
                            x.Status
                        }).ToList()
                    : db.SalesInvoices
                        .Where(x => x.CustomerId == partyId && x.InvoiceDate >= from && x.InvoiceDate <= to)
                        .OrderByDescending(x => x.InvoiceDate)
                        .Select(x => new
                        {
                            Reference = x.InvoiceNumber,
                            x.InvoiceDate,
                            TotalAmount = x.NetAmount,
                            x.PaidAmount,
                            Due = x.NetAmount - x.PaidAmount,
                            x.Status
                        }).ToList();

                GridResult.DataSource = rows;
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["Reference"] != null) GridViewResult.Columns["Reference"].Caption = LocalizationManager.T("Returns_ColInvoice");
                if (GridViewResult.Columns["InvoiceDate"] != null) GridViewResult.Columns["InvoiceDate"].Caption = LocalizationManager.T("AccountStatement_ColInvoiceDate");
                if (GridViewResult.Columns["TotalAmount"] != null) GridViewResult.Columns["TotalAmount"].Caption = LocalizationManager.T("Shared_TotalAmount");
                if (GridViewResult.Columns["PaidAmount"] != null) GridViewResult.Columns["PaidAmount"].Caption = LocalizationManager.T("Purchases_ColPaidAmount");
                if (GridViewResult.Columns["Due"] != null) GridViewResult.Columns["Due"].Caption = LocalizationManager.T("Dashboard_ColDue");
                if (GridViewResult.Columns["Status"] != null) GridViewResult.Columns["Status"].Caption = LocalizationManager.T("Shared_Status");

                decimal totalInvoiced = rows.Sum(x => x.TotalAmount);
                decimal totalPaid = rows.Sum(x => x.PaidAmount);
                decimal totalDue = totalInvoiced - totalPaid;
                LblSummary.Text = string.Format(LocalizationManager.T("AccountStatement_SummaryFmt"), totalInvoiced, totalPaid, totalDue);
            }
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadParties();
            RunReport();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (CmbParty.SelectedIndex < 0)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("AccountStatement_SelectPartyFirst"));
                return;
            }
            RunReport();
        }
    }
}
