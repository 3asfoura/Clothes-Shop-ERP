using Clothes_Shop_ERP.DAL;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public class UcDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private TableLayoutPanel _cardsPanel;
        private ChartControl _lineChart;
        private ChartControl _pieChart;

        private PopupContainerEdit _rangeEdit;
        private PopupContainerControl _rangePopup;
        private RadioGroup _rangeRadioGroup;
        private DateEdit _customFrom, _customTo;
        private DateTime _selectedFrom, _selectedTo;

        private GridControl _lowStockGrid;
        private GridView _lowStockView;
        private GridControl _recentSalesGrid;
        private GridView _recentSalesView;
        private GridControl _recentPurchaseGrid;
        private GridView _recentPurchaseView;

        public UcDashboard()
        {
            this.Dock = DockStyle.Fill;
            BuildUi();
            LoadCardsAndSideData();
            ApplySelectedRange();
        }

        private void BuildUi()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            this.Controls.Add(root);

            _cardsPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
            for (int i = 0; i < 4; i++) _cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int i = 0; i < 2; i++) _cardsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            root.Controls.Add(_cardsPanel, 0, 0);

            var middle = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            root.Controls.Add(middle, 0, 1);

            var lineGroup = new GroupControl { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_IncomeVsExpenses"), Dock = DockStyle.Fill };
            var lineLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            lineLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            lineLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var periodBar = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1 };
            BuildRangePicker();
            _rangeEdit.Anchor = AnchorStyles.Right;
            periodBar.Controls.Add(_rangeEdit, 0, 0);

            _lineChart = new ChartControl { Dock = DockStyle.Fill };

            lineLayout.Controls.Add(periodBar, 0, 0);
            lineLayout.Controls.Add(_lineChart, 0, 1);
            lineGroup.Controls.Add(lineLayout);
            middle.Controls.Add(lineGroup, 0, 0);

            var rightSide = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            rightSide.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            rightSide.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            middle.Controls.Add(rightSide, 1, 0);

            var lowStockGroup = new GroupControl { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_LowStock"), Dock = DockStyle.Fill };
            _lowStockGrid = new GridControl { Dock = DockStyle.Fill };
            _lowStockView = new GridView(_lowStockGrid);
            _lowStockGrid.MainView = _lowStockView;
            _lowStockView.OptionsBehavior.Editable = false;
            _lowStockView.OptionsView.ShowGroupPanel = false;
            lowStockGroup.Controls.Add(_lowStockGrid);
            rightSide.Controls.Add(lowStockGroup, 0, 0);

            var pieGroup = new GroupControl { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Overview"), Dock = DockStyle.Fill };
            _pieChart = new ChartControl { Dock = DockStyle.Fill };
            pieGroup.Controls.Add(_pieChart);
            rightSide.Controls.Add(pieGroup, 0, 1);

            var recentTabs = new XtraTabControl { Dock = DockStyle.Fill };

            var tabSales = new XtraTabPage { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_LatestSalesInvoices") };
            _recentSalesGrid = new GridControl { Dock = DockStyle.Fill };
            _recentSalesView = new GridView(_recentSalesGrid);
            _recentSalesGrid.MainView = _recentSalesView;
            _recentSalesView.OptionsBehavior.Editable = false;
            _recentSalesView.OptionsView.ShowGroupPanel = false;
            tabSales.Controls.Add(_recentSalesGrid);

            var tabPurchase = new XtraTabPage { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_LatestPurchaseInvoices") };
            _recentPurchaseGrid = new GridControl { Dock = DockStyle.Fill };
            _recentPurchaseView = new GridView(_recentPurchaseGrid);
            _recentPurchaseGrid.MainView = _recentPurchaseView;
            _recentPurchaseView.OptionsBehavior.Editable = false;
            _recentPurchaseView.OptionsView.ShowGroupPanel = false;
            tabPurchase.Controls.Add(_recentPurchaseGrid);

            recentTabs.TabPages.Add(tabSales);
            recentTabs.TabPages.Add(tabPurchase);
            root.Controls.Add(recentTabs, 0, 2);
        }

        private void BuildRangePicker()
        {
            _rangePopup = new PopupContainerControl { Size = new Size(260, 260) };

            _rangeRadioGroup = new RadioGroup
            {
                Location = new Point(5, 5),
                Size = new Size(250, 190),
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };
            _rangeRadioGroup.Properties.Items.AddRange(new RadioGroupItem[]
            {
                new RadioGroupItem(0, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Today")),
                new RadioGroupItem(1, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Last7Days")),
                new RadioGroupItem(2, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Last14Days")),
                new RadioGroupItem(3, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Last28Days")),
                new RadioGroupItem(4, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Last60Days")),
                new RadioGroupItem(5, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Last90Days")),
                new RadioGroupItem(6, Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Custom")),
            });
            _rangeRadioGroup.SelectedIndex = 3;
            _rangeRadioGroup.SelectedIndexChanged += RangeRadioGroup_SelectedIndexChanged;

            var lblFrom = new LabelControl { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_From"), Location = new Point(10, 200) };
            _customFrom = new DateEdit { Location = new Point(40, 197), Width = 95, Enabled = false };
            _customFrom.DateTime = DateTime.Today;

            var lblTo = new LabelControl { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_To"), Location = new Point(145, 200) };
            _customTo = new DateEdit { Location = new Point(175, 197), Width = 95, Enabled = false };
            _customTo.DateTime = DateTime.Today;

            var btnApply = new SimpleButton { Text = Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Apply"), Location = new Point(150, 225), Width = 100 };
            btnApply.Click += (s, e) => { ApplySelectedRange(); _rangeEdit.ClosePopup(); };

            _rangePopup.Controls.Add(_rangeRadioGroup);
            _rangePopup.Controls.Add(lblFrom);
            _rangePopup.Controls.Add(_customFrom);
            _rangePopup.Controls.Add(lblTo);
            _rangePopup.Controls.Add(_customTo);
            _rangePopup.Controls.Add(btnApply);

            _rangeEdit = new PopupContainerEdit { Width = 220 };
            _rangeEdit.Properties.PopupControl = _rangePopup;
            _rangeEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        private void RangeRadioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = _rangeRadioGroup.SelectedIndex == 6;
            _customFrom.Enabled = isCustom;
            _customTo.Enabled = isCustom;

            if (!isCustom)
            {
                ApplySelectedRange();
                _rangeEdit.ClosePopup();
            }
        }

        private void ApplySelectedRange()
        {
            DateTime today = DateTime.Today;
            switch (_rangeRadioGroup.SelectedIndex)
            {
                case 0: _selectedFrom = today; _selectedTo = today; break;
                case 1: _selectedFrom = today.AddDays(-6); _selectedTo = today; break;
                case 2: _selectedFrom = today.AddDays(-13); _selectedTo = today; break;
                case 3: _selectedFrom = today.AddDays(-27); _selectedTo = today; break;
                case 4: _selectedFrom = today.AddDays(-59); _selectedTo = today; break;
                case 5: _selectedFrom = today.AddDays(-89); _selectedTo = today; break;
                case 6:
                    _selectedFrom = _customFrom.DateTime.Date;
                    _selectedTo = _customTo.DateTime.Date;
                    break;
            }

            string itemText = _rangeRadioGroup.Properties.Items[_rangeRadioGroup.SelectedIndex].Description;
            _rangeEdit.Text = $"{itemText}: {_selectedFrom:dd/MM} - {_selectedTo:dd/MM/yyyy}";

            LoadLineChart();
        }

        private PanelControl MakeCard(string title, string value, Color accentColor)
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
                Location = new Point(12, 12),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            var lblValue = new LabelControl
            {
                Text = value,
                Location = new Point(12, 34),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = accentColor
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            return card;
        }

        private void LoadCardsAndSideData()
        {
            _cardsPanel.Controls.Clear();

            using (var db = new ClothesShopDBContext())
            {
                DateTime monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                decimal totalSales = db.SalesInvoices.Where(x => x.InvoiceDate >= monthStart).Sum(x => (decimal?)x.NetAmount) ?? 0;
                decimal totalPurchases = db.PurchaseInvoices.Where(x => x.InvoiceDate >= monthStart).Sum(x => (decimal?)x.TotalAmount) ?? 0;
                decimal totalIncome = db.TreasuryTransactions.Where(x => x.TransactionType == "In" && x.CreatedAt >= monthStart).Sum(x => (decimal?)x.Amount) ?? 0;
                decimal totalExpense = db.TreasuryTransactions.Where(x => x.TransactionType == "Out" && x.CreatedAt >= monthStart).Sum(x => (decimal?)x.Amount) ?? 0;
                int totalCustomers = db.Customers.Count();
                int totalSuppliers = db.Suppliers.Count();
                decimal salesReturns = db.SalesReturns.Where(x => x.ReturnDate >= monthStart).Sum(x => (decimal?)x.TotalAmount) ?? 0;
                int lowStockCount = db.BranchStock.Count(x => x.Quantity <= x.MinQuantity);

                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_TotalSalesMonth"), totalSales.ToString("n0"), Color.MediumPurple), 0, 0);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_TotalPurchasesMonth"), totalPurchases.ToString("n0"), Color.DarkOrange), 1, 0);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_TotalIncomeMonth"), totalIncome.ToString("n0"), Color.SeaGreen), 2, 0);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_TotalExpensesMonth"), totalExpense.ToString("n0"), Color.Crimson), 3, 0);

                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_CustomersCount"), totalCustomers.ToString(), Color.SteelBlue), 0, 1);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_SuppliersCount"), totalSuppliers.ToString(), Color.DarkGreen), 1, 1);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_SalesReturnsMonth"), salesReturns.ToString("n0"), Color.DeepPink), 2, 1);
                _cardsPanel.Controls.Add(MakeCard(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_LowStock"), lowStockCount.ToString(), Color.Firebrick), 3, 1);

                var lowStockItems = (from bs in db.BranchStock
                                     where bs.Quantity <= bs.MinQuantity
                                     select new
                                     {
                                         Product = bs.ProductVariant.Product.Name,
                                         Size = bs.ProductVariant.Size.Name,
                                         Color = bs.ProductVariant.Color.Name,
                                         bs.Quantity,
                                         bs.MinQuantity
                                     }).Take(10).ToList();
                _lowStockGrid.DataSource = lowStockItems;

                decimal allSales = db.SalesInvoices.Sum(x => (decimal?)x.NetAmount) ?? 0;
                decimal allPurchases = db.PurchaseInvoices.Sum(x => (decimal?)x.TotalAmount) ?? 0;
                decimal allIncome = db.TreasuryTransactions.Where(x => x.TransactionType == "In").Sum(x => (decimal?)x.Amount) ?? 0;
                decimal allExpense = db.TreasuryTransactions.Where(x => x.TransactionType == "Out").Sum(x => (decimal?)x.Amount) ?? 0;

                var pieSeries = new Series(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Overview"), ViewType.Pie);
                pieSeries.Points.Add(new SeriesPoint(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Sales"), (double)allSales));
                pieSeries.Points.Add(new SeriesPoint(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Purchases"), (double)allPurchases));
                pieSeries.Points.Add(new SeriesPoint(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Income"), (double)allIncome));
                pieSeries.Points.Add(new SeriesPoint(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Expenses"), (double)allExpense));
                pieSeries.LegendPointOptions.Pattern = "{A}: {VP:P0}";
                pieSeries.Label.TextPattern = "{A}\n{VP:P0}";

                _pieChart.Series.Clear();
                _pieChart.Series.Add(pieSeries);
                _pieChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                var recentSales = db.SalesInvoices
                    .Include(x => x.Customer)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Take(10)
                    .Select(x => new
                    {
                        x.InvoiceDate,
                        x.InvoiceNumber,
                        Customer = x.Customer != null ? x.Customer.Name : Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_CashCustomer"),
                        x.NetAmount,
                        x.PaidAmount,
                        Due = x.NetAmount - x.PaidAmount
                    }).ToList();
                _recentSalesGrid.DataSource = recentSales;

                var recentPurchases = db.PurchaseInvoices
                    .Include(x => x.Supplier)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Take(10)
                    .Select(x => new
                    {
                        x.InvoiceDate,
                        Supplier = x.Supplier.Name,
                        x.TotalAmount,
                        x.PaidAmount,
                        Due = x.TotalAmount - x.PaidAmount
                    }).ToList();
                _recentPurchaseGrid.DataSource = recentPurchases;
            }
        }

        private void LoadLineChart()
        {
            using (var db = new ClothesShopDBContext())
            {
                var incomeSeries = new Series(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Income"), ViewType.Line);
                var expenseSeries = new Series(Clothes_Shop_ERP.Localization.LocalizationManager.T("Dashboard_Expenses"), ViewType.Line);

                DateTime rangeEndExclusive = _selectedTo.AddDays(1);

                var rows = db.TreasuryTransactions
                    .Where(x => x.CreatedAt >= _selectedFrom && x.CreatedAt < rangeEndExclusive)
                    .Select(x => new { x.TransactionType, x.Amount, x.CreatedAt })
                    .ToList();

                int dayCount = (int)(_selectedTo - _selectedFrom).TotalDays + 1;
                for (int i = 0; i < dayCount; i++)
                {
                    DateTime day = _selectedFrom.AddDays(i);
                    decimal income = rows.Where(x => x.TransactionType == "In" && x.CreatedAt.Date == day.Date).Sum(x => x.Amount);
                    decimal expense = rows.Where(x => x.TransactionType == "Out" && x.CreatedAt.Date == day.Date).Sum(x => x.Amount);
                    string label = day.ToString("dd/MM");
                    incomeSeries.Points.Add(new SeriesPoint(label, (double)income));
                    expenseSeries.Points.Add(new SeriesPoint(label, (double)expense));
                }

                ((LineSeriesView)incomeSeries.View).Color = Color.MediumPurple;
                ((LineSeriesView)expenseSeries.View).Color = Color.Crimson;

                _lineChart.Series.Clear();
                _lineChart.Series.Add(incomeSeries);
                _lineChart.Series.Add(expenseSeries);
                _lineChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "UcDashboard";
            this.Size = new Size(1200, 800);
            this.ResumeLayout(false);
        }
    }
}