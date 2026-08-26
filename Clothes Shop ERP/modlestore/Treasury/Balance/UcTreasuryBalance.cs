using Clothes_Shop_ERP.DAL;
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
                Text = "تحديث",
                Location = new Point(20, 15),
                Width = 100
            };
            btnRefresh.Click += (s, e) => RunReport();

            LblTotalBalance = new LabelControl
            {
                Text = "الرصيد الحالي: 0.00",
                Location = new Point(140, 12),
                AutoSizeMode = LabelAutoSizeMode.None,
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            LblTotalsBreakdown = new LabelControl
            {
                Text = "إجمالي الداخل: 0.00   |   إجمالي الخارج: 0.00",
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

                decimal totalIn = rows.Where(x => x.TransactionType == "In").Sum(x => x.Amount);
                decimal totalOut = rows.Where(x => x.TransactionType == "Out").Sum(x => x.Amount);
                decimal balance = totalIn - totalOut;

                LblTotalBalance.Text = $"الرصيد الحالي: {balance:n2}";
                LblTotalBalance.ForeColor = balance >= 0 ? Color.DarkGreen : Color.DarkRed;

                LblTotalsBreakdown.Text = $"إجمالي الداخل: {totalIn:n2}   |   إجمالي الخارج: {totalOut:n2}";
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