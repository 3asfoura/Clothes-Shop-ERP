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
    public class UcTreasuryBalance : DevExpress.XtraEditors.XtraUserControl
    {
        private LabelControl LblTotalBalance;
        private LabelControl LblTotalsBreakdown;
        private GridControl GridResult;
        private GridView GridViewResult;

        public UcTreasuryBalance()
        {
            this.Dock = DockStyle.Fill;
            BuildUi();
            RunReport();

        }

        private void BuildUi()
        {
            var btnRefresh = new SimpleButton
            {
                Text = LocalizationManager.T("Shared_Refresh"),
                Location = new Point(20, 15),
                Width = 100
            };
            btnRefresh.Click += (s, e) => RunReport();

            LblTotalBalance = new LabelControl
            {
                Text = LocalizationManager.T("TreasuryBalance_CurrentBalance"),
                Location = new Point(140, 12),
                AutoSizeMode = LabelAutoSizeMode.None,
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            LblTotalsBreakdown = new LabelControl
            {
                Text = LocalizationManager.T("TreasuryBalance_InOutTotals"),
                Location = new Point(20, 50),
                AutoSizeMode = LabelAutoSizeMode.None,
                Size = new Size(500, 20),
                Font = new Font("Segoe UI", 9)
            };

            GridResult = new GridControl
            {
                Location = new Point(20, 85),
                Size = new Size(700, 350),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            GridViewResult = new GridView(GridResult);
            GridResult.MainView = GridViewResult;
            GridViewResult.OptionsBehavior.Editable = false;
            GridViewResult.OptionsView.ShowGroupPanel = false;
            GridViewResult.OptionsCustomization.AllowSort = false;
            GridViewResult.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Controls.Add(btnRefresh);
            this.Controls.Add(LblTotalBalance);
            this.Controls.Add(LblTotalsBreakdown);
            this.Controls.Add(GridResult);
        }

        private void RunReport()
        {
            using (var db = new ClothesShopDBContext())
            {
                var rows = db.TreasuryTransactions
                    .Include(x => x.Branch)
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
                LblTotalBalance.ForeColor = balance >= 0 ? Color.DarkGreen : Color.DarkRed;

                LblTotalsBreakdown.Text = string.Format(LocalizationManager.T("TreasuryBalance_InOutTotalsFmt"), totalIn, totalOut);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "UcTreasuryBalance";
            this.Size = new Size(760, 460);
            this.ResumeLayout(false);
        }
    }
}