using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using Clothes_Shop_ERP.modlestore;
using Clothes_Shop_ERP.modlestore.Settings.Users;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public partial class FrmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        // Auto-lock: re-shows the login screen after this many minutes with no
        // mouse/keyboard activity anywhere on the PC - not just this app. Reads
        // the same Windows-wide idle counter screensavers use (GetLastInputInfo),
        // so it's accurate no matter which control, popup or dialog has focus.
        private const int IdleLockMinutes = 5;
        private bool _isLocked;

        public FrmMain()
        {

            InitializeComponent();
            DarkModeToggle();
            ComboLanguage.EditValue = LocalizationManager.CurrentLanguage.ToString();
            ApplyLanguage();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static TimeSpan GetSystemIdleTime()
        {
            var info = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };
            GetLastInputInfo(ref info);
            return TimeSpan.FromMilliseconds((uint)Environment.TickCount - info.dwTime);
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (_isLocked) return;
            if (GetSystemIdleTime().TotalMinutes >= IdleLockMinutes)
            {
                _isLocked = true;
                new FrmLogin().ShowDialog(this);
                _isLocked = false;
            }
        }
        // Hides sidebar entries the current role has no access to (PermissionLevel
        // "None"). Screens the role can at least Read stay visible here - the
        // Read-vs-Write (read-only) restriction is enforced inside each screen.
        private void ApplyPermissions()
        {
            var screenElements = new System.Collections.Generic.Dictionary<string, DevExpress.XtraBars.Navigation.AccordionControlElement>
            {
                { "Products", ElementProducts },
                { "ProductVariants", ElementProductVariants },
                { "Categories", ElementCategories },
                { "Brands", ElementBrands },
                { "ColorsSizes", ElementColors_Sizes },
                { "StockCount", ElementStock_Count },
                { "StockMovements", ElementStock_Movements },
                { "BranchTransfer", ElementBranch_Transfer },
                { "PointOfSale", ElementPoint_of_Sale },
                { "SalesInvoices", ElementSales_Invoices },
                { "Returns", ElementReturns },
                { "Customers", ElementCustomers },
                { "PurchaseInvoices", ElementPurchase },
                { "PurchaseReturns", ElementPurchaseReturns },
                { "Suppliers", ElementSuppliers },
                { "Treasury", ElementTreasury },
                { "TreasuryBalance", ElementTreasuryBalance },
                { "SalesReport", ElementSalesReport1 },
                { "StockReport", ElementStockReport },
                { "ProfitReport", ElementProfitReport },
                { "AccountStatement", ElementAccountStatement },
                { "DayClosingReport", ElementDayClosingReport },
                { "Branches", ElementBranches },
                { "UsersRoles", ElementUsers_Roles },
                { "PaymentMethods", ElementPaymentMethods },
                { "AuditLogs", ElementAuditLogs },
                { "BackupSettings", ElementBackupSettings },
            };

            foreach (var pair in screenElements)
            {
                pair.Value.Visible = PermissionManager.CanView(pair.Key);
            }
        }
        public void ApplyLanguage()
        {
            ElementInventory.Text = LocalizationManager.T("Main_Inventory");
            ElementProducts.Text = LocalizationManager.T("Main_Products");
            ElementProductVariants.Text = LocalizationManager.T("Main_ProductVariants");
            ElementCategories.Text = LocalizationManager.T("Main_Categories");
            ElementBrands.Text = LocalizationManager.T("Main_Brands");
            ElementColors_Sizes.Text = LocalizationManager.T("Main_ColorsSizes");
            ElementStock_Count.Text = LocalizationManager.T("Main_StockCount");
            ElementStock_Movements.Text = LocalizationManager.T("Main_StockMovements");
            ElementBranch_Transfer.Text = LocalizationManager.T("Main_BranchTransfer");
            ElementBranch_Sales.Text = LocalizationManager.T("Main_Sales");
            ElementPoint_of_Sale.Text = LocalizationManager.T("Main_PointOfSale");
            ElementSales_Invoices.Text = LocalizationManager.T("Main_SalesInvoices");
            ElementReturns.Text = LocalizationManager.T("Main_Returns");
            ElementCustomers.Text = LocalizationManager.T("Main_Customers");
            ElementPurchasing.Text = LocalizationManager.T("Main_Purchasing");
            ElementPurchase.Text = LocalizationManager.T("Main_PurchaseInvoices");
            ElementPurchaseReturns.Text = LocalizationManager.T("Main_PurchaseReturns");
            ElementSuppliers.Text = LocalizationManager.T("Main_Suppliers");
            _ElementTreasury.Text = LocalizationManager.T("Main_Treasury");
            ElementTreasury.Text = LocalizationManager.T("Main_TreasuryTransactions");
            ElementTreasuryBalance.Text = LocalizationManager.T("Main_TreasuryBalance");
            ElementReports.Text = LocalizationManager.T("Main_Reports");
            ElementSalesReport1.Text = LocalizationManager.T("Main_SalesReport");
            ElementStockReport.Text = LocalizationManager.T("Main_StockReport");
            ElementProfitReport.Text = LocalizationManager.T("Main_ProfitReport");
            ElementAccountStatement.Text = LocalizationManager.T("Main_AccountStatement");
            ElementDayClosingReport.Text = LocalizationManager.T("Main_DayClosingReport");
            ElementSettings.Text = LocalizationManager.T("Main_Settings");
            ElementBranches.Text = LocalizationManager.T("Main_Branches");
            ElementUsers_Roles.Text = LocalizationManager.T("Main_UsersRoles");
            ElementPaymentMethods.Text = LocalizationManager.T("Main_PaymentMethods");
            ElementAuditLogs.Text = LocalizationManager.T("Main_AuditLogs");
            ElementBackupSettings.Text = LocalizationManager.T("Main_BackupSettings");
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();
            ApplyPermissions();

            UcDashboard dash = new UcDashboard();
            setTabPage(dash, "Dashboard", null);

            // Runs on a background thread so a slow backup never freezes the UI;
            // BackupManager itself no-ops quietly if no folder is configured yet
            // or a backup already ran today.
            System.Threading.Tasks.Task.Run(() => BackupManager.RunBackupIfDue());

        }
        void setTabPage(UserControl formObject, string FrmText, SvgImage image)
        {
            foreach (var item in TabsControls.TabPages.ToList())
            {
                if (item.Text == FrmText)
                {
                    TabsControls.SelectedTabPage = item;
                    return;
                }

            }
            TabsControls.TabPages.Add(FrmText);
            formObject.Dock = DockStyle.Fill;
            var tc = TabsControls.TabPages.Last();
            tc.Controls.Add(formObject);
            TabsControls.SelectedTabPage = tc;
            TabsControls.SelectedTabPage.ImageOptions.SvgImage = image;
            TabsControls.SelectedTabPage.ImageOptions.SvgImageSize = new Size(20, 20);

        }

        private void ElementProducts_Click(object sender, EventArgs e)
        {
            UcProducts frm = new UcProducts();
            setTabPage(frm, ElementProducts.Text, ElementProducts.ImageOptions.SvgImage);
        }

        private void ElementCategories_Click(object sender, EventArgs e)
        {
            UcCategories frm = new UcCategories();
            setTabPage(frm, ElementCategories.Text, ElementCategories.ImageOptions.SvgImage);
        }

        private void ElementPurchasing_Click(object sender, EventArgs e)
        {

        }

        private void ElementUsers_Roles_Click(object sender, EventArgs e)
        {
            UcUsers_Roles frm = new UcUsers_Roles();
            setTabPage(frm, ElementUsers_Roles.Text, ElementUsers_Roles.ImageOptions.SvgImage);
        }

        private void ElementBranches_Click(object sender, EventArgs e)
        {
            UcBranches frm = new UcBranches();
            setTabPage(frm, ElementBranches.Text, ElementBranches.ImageOptions.SvgImage);
        }

        private void TabsControls_CloseButtonClick(object sender, EventArgs e)
        {
            if (TabsControls.SelectedTabPage != TabsControls.TabPages[0])
            {
                TabsControls.TabPages.Remove(TabsControls.SelectedTabPage);
                TabsControls.SelectedTabPage = TabsControls.TabPages.Last();

            }
            else
            {
                // msg
            }
        }

        private void ElementBrands_Click(object sender, EventArgs e)
        {
            UcBrands frm = new UcBrands();
            setTabPage(frm, ElementBrands.Text, ElementBrands.ImageOptions.SvgImage);
        }

        private void ElementColors_Sizes_Click(object sender, EventArgs e)
        {
            UcColorsSizes frm = new UcColorsSizes();
            setTabPage(frm, ElementColors_Sizes.Text, ElementColors_Sizes.ImageOptions.SvgImage);
        }

        private void ElementStock_Movements_Click(object sender, EventArgs e)
        {
            UcStockMovements frm = new UcStockMovements();
            setTabPage(frm, ElementStock_Movements.Text, ElementStock_Movements.ImageOptions.SvgImage);
        }

        private void ElementStock_Count_Click(object sender, EventArgs e)
        {
            UcStockCount frm = new UcStockCount();
            setTabPage(frm, ElementStock_Count.Text, ElementStock_Count.ImageOptions.SvgImage);
        }

        private void ElementBranch_Transfer_Click(object sender, EventArgs e)
        {
            UcBranchTransfer frm = new UcBranchTransfer();
            setTabPage(frm, ElementBranch_Transfer.Text, ElementBranch_Transfer.ImageOptions.SvgImage);
        }

        private void ElementProductVariants_Click(object sender, EventArgs e)
        {
            UcProductVariants frm = new UcProductVariants();
            setTabPage(frm, ElementProductVariants.Text, ElementProductVariants.ImageOptions.SvgImage);
        }

        private void ElementSuppliers_Click(object sender, EventArgs e)
        {
            UcSuppliers frm = new UcSuppliers();
            setTabPage(frm, ElementSuppliers.Text, ElementSuppliers.ImageOptions.SvgImage);
        }

        private void ElementCustomers_Click(object sender, EventArgs e)
        {
            UcCustomers frm = new UcCustomers();
            setTabPage(frm, ElementCustomers.Text, ElementCustomers.ImageOptions.SvgImage);
        }

        private void ElementTreasury_Click(object sender, EventArgs e)
        {
            UcTreasuryTransactions frm = new UcTreasuryTransactions();
            setTabPage(frm, ElementTreasury.Text, ElementTreasury.ImageOptions.SvgImage);
        }

        private void ElementPurchase_Click(object sender, EventArgs e)
        {
            UcPurchaseInvoices frm = new UcPurchaseInvoices();
            setTabPage(frm, ElementPurchase.Text, ElementPurchase.ImageOptions.SvgImage);
        }

        private void ElementPurchaseReturns_Click(object sender, EventArgs e)
        {
            Clothes_Shop_ERP.modlestore.UcPurchaseReturns frm = new Clothes_Shop_ERP.modlestore.UcPurchaseReturns();
            setTabPage(frm, ElementPurchaseReturns.Text, ElementPurchaseReturns.ImageOptions.SvgImage);
        }

        private void ElementPoint_of_Sale_Click(object sender, EventArgs e)
        {
            UcPointOfSale frm = new UcPointOfSale();
            setTabPage(frm, ElementPoint_of_Sale.Text, ElementPoint_of_Sale.ImageOptions.SvgImage);
        }

        private void ElementStockReport_Click(object sender, EventArgs e)
        {
            UcStockReport frm = new UcStockReport();
            setTabPage(frm, ElementStockReport.Text, ElementStockReport.ImageOptions.SvgImage);
        }

        private void ElementSales_Invoices_Click(object sender, EventArgs e)
        {
            UcSalesInvoices frm = new UcSalesInvoices();
            setTabPage(frm, ElementSales_Invoices.Text, ElementSales_Invoices.ImageOptions.SvgImage);
        }

        private void ElementReturns_Click(object sender, EventArgs e)
        {
            UcReturns frm = new UcReturns();
            setTabPage(frm, ElementReturns.Text, ElementReturns.ImageOptions.SvgImage);
        }

        private void ElementPaymentMethods_Click(object sender, EventArgs e)
        {
            UcPaymentMethods frm = new UcPaymentMethods();
            setTabPage(frm, ElementPaymentMethods.Text, ElementPaymentMethods.ImageOptions.SvgImage);
        }

        private void ElementProfitReport_Click(object sender, EventArgs e)
        {
            UcProfitReport frm = new UcProfitReport();
            setTabPage(frm, ElementProfitReport.Text, ElementProfitReport.ImageOptions.SvgImage);
        }

        private void ElementSalesReport1_Click(object sender, EventArgs e)
        {
            UcSalesReport frm = new UcSalesReport();
            setTabPage(frm, ElementSalesReport1.Text, ElementSalesReport1.ImageOptions.SvgImage);

        }

        private void ElementAccountStatement_Click(object sender, EventArgs e)
        {
            UcAccountStatement frm = new UcAccountStatement();
            setTabPage(frm, ElementAccountStatement.Text, ElementAccountStatement.ImageOptions.SvgImage);
        }

        private void ElementDayClosingReport_Click(object sender, EventArgs e)
        {
            UcDayClosingReport frm = new UcDayClosingReport();
            setTabPage(frm, ElementDayClosingReport.Text, ElementDayClosingReport.ImageOptions.SvgImage);
        }
        bool DarkMode = false;
        private void ToggleDarkMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ElementTreasuryBalance_Click(object sender, EventArgs e)
        {
            UcTreasuryBalance frm = new UcTreasuryBalance();
            setTabPage(frm, ElementTreasuryBalance.Text, ElementTreasuryBalance.ImageOptions.SvgImage);
        }

        private void ElementAuditLogs_Click(object sender, EventArgs e)
        {
            UcAuditLogs frm = new UcAuditLogs();
            setTabPage(frm, ElementAuditLogs.Text, ElementAuditLogs.ImageOptions.SvgImage);

        }

        private void ElementBackupSettings_Click(object sender, EventArgs e)
        {
            UcBackupSettings frm = new UcBackupSettings();
            setTabPage(frm, ElementBackupSettings.Text, ElementBackupSettings.ImageOptions.SvgImage);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DarkModeToggle();
        }
        void DarkModeToggle()
        {

            if (DarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.WXICompact.Default);
                barButtonItem1.ImageOptions.SvgImage = Properties.Resources.icons8_dark_mode_50;
                DarkMode = false;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.WXICompact.Darkness);
                barButtonItem1.ImageOptions.SvgImage = Properties.Resources.icons8_sun_50;
                DarkMode = true;
            }
        }

        

        private void ComboLanguage_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ComboLanguage_EditValueChanged_1(object sender, EventArgs e)
        {
            string selected = ComboLanguage.EditValue as string;

            AppLanguage newLanguage = selected == "English"
                ? AppLanguage.English
                : AppLanguage.Egyptian;

            if (newLanguage == LocalizationManager.CurrentLanguage)
                return;

            LocalizationManager.CurrentLanguage = newLanguage;
            LocalizationManager.SaveLanguagePreference();

            string title = LocalizationManager.T("Common_ConfirmTitle");
            string message = LocalizationManager.T("Common_RestartForLanguage");

            var result = XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}