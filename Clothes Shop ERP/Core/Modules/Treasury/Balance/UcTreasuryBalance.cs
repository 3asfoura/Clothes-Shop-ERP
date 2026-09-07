using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcTreasuryBalance : DevExpress.XtraEditors.XtraUserControl
    {
        public UcTreasuryBalance()
        {
            InitializeComponent();
            BtnRefresh.Text = LocalizationManager.T("Shared_Refresh");
            BtnRefresh.Click += (s, e) => RunReport();
            GridResult.MouseUp += (s, e) =>
            {
                if (e.Button != MouseButtons.Right) return;
                var menu = new ContextMenuStrip();
                menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s2, ev) => Sett.ExportGrid(GridResult, LocalizationManager.T("Main_TreasuryBalance")));
                menu.Show(GridResult, e.Location);
            };
            Sett.CenterColumns(GridViewResult);
            RunReport();
        }

        private void RunReport()
        {
            using (var db = new ClothesShopDBContext())
            {
                var rows = db.TreasuryTransactions
                    .Include(x => x.Branch)
                    .Where(x => !PermissionManager.BranchRestricted || x.BranchId == FrmLogin.CurrentBranchId)
                    .Select(x => new
                    {
                        Branch = x.Branch.Name,
                        x.TransactionType,
                        x.Amount
                    })
                    .ToList();

                var perBranch = rows
                    .GroupBy(x => x.Branch)
                    .Select(g => new
                    {
                        Branch = g.Key,
                        TotalIn = g.Where(x => x.TransactionType == "In").Sum(x => x.Amount),
                        TotalOut = g.Where(x => x.TransactionType == "Out").Sum(x => x.Amount),
                        Balance = g.Where(x => x.TransactionType == "In").Sum(x => x.Amount)
                                - g.Where(x => x.TransactionType == "Out").Sum(x => x.Amount)
                    })
                    .OrderBy(x => x.Branch)
                    .ToList();

                GridResult.DataSource = perBranch;
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["Branch"] != null) GridViewResult.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
                if (GridViewResult.Columns["TotalIn"] != null) GridViewResult.Columns["TotalIn"].Caption = LocalizationManager.T("TreasuryBalance_ColTotalIn");
                if (GridViewResult.Columns["TotalOut"] != null) GridViewResult.Columns["TotalOut"].Caption = LocalizationManager.T("TreasuryBalance_ColTotalOut");
                if (GridViewResult.Columns["Balance"] != null) GridViewResult.Columns["Balance"].Caption = LocalizationManager.T("TreasuryBalance_ColBalance");

                decimal totalIn = rows.Where(x => x.TransactionType == "In").Sum(x => x.Amount);
                decimal totalOut = rows.Where(x => x.TransactionType == "Out").Sum(x => x.Amount);
                decimal balance = totalIn - totalOut;

                LblTotalBalance.Text = string.Format(LocalizationManager.T("TreasuryBalance_CurrentBalanceFmt"), balance);
                LblTotalBalance.AppearanceItemCaption.ForeColor = balance >= 0 ? Color.DarkGreen : Color.DarkRed;
                LblTotalBalance.AppearanceItemCaption.Options.UseForeColor = true;

                LblTotalsBreakdown.Text = string.Format(LocalizationManager.T("TreasuryBalance_InOutTotalsFmt"), totalIn, totalOut);
            }
        }
    }
}