using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.modlestore;
using Clothes_Shop_ERP.modlestore.Settings.Users;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public partial class FrmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public FrmMain()
        {
            InitializeComponent();

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();
            UcDashboard dash = new UcDashboard();
            setTabPage(dash, "Dashboard", null);
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

        private void ToggleDarkMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ToggleDarkMode.Checked)
            {
                UserLookAndFeel.Default.SetSkinStyle(
                    SkinSvgPalette.WXICompact.Darkness
                );
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle(
            SkinSvgPalette.WXICompact.Default
        );
            }
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
    }
}