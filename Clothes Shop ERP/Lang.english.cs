using System.Collections.Generic;

namespace Clothes_Shop_ERP.Localization
{
    public static class Lang_English
    {
        public static readonly Dictionary<string, string> Strings = new Dictionary<string, string>
        {
            // ===== Common =====
            { "Common_ConfirmTitle", "Confirm" },
            { "Common_ConfirmDelete", "Delete '{0}'?" },
            { "Common_ConfirmAction", "{0} '{1}'?" },
            { "Common_Refresh", "Refresh" },
            { "Common_From", "From:" },
            { "Common_To", "To:" },
            { "Common_Name", "Name" },
            { "Common_Phone", "Phone" },
            { "Common_Address", "Address" },
            { "Common_IsActive", "IsActive" },
            { "Common_Status", "Status" },
            { "Common_Branch", "Branch" },
            { "Common_CreatedAt", "CreatedAt" },
            { "Common_Description", "Description" },
            { "Common_Amount", "Amount" },
            { "Common_TotalAmount", "TotalAmount" },
            { "Common_Code", "Code" },
            { "Common_Color", "Color" },
            { "Common_Size", "Size" },

            // ===== Login =====
            { "Login_Title", "NOVA ERP - Login" },
            { "Login_BtnLogin", "Login" },
            { "Login_WelcomeBack", "Welcome Back" },
            { "Login_PleaseSignIn", "Please sign in to continue" },
            { "Login_Branch", "Branch" },
            { "Login_Username", "Username" },
            { "Login_Password", "Password" },
            { "Login_ColId", "ID" },
            { "Login_ColName", "Name" },

            // ===== Main =====
            { "Main_Inventory", "Inventory" },
            { "Main_Products", "Products" },
            { "Main_ProductVariants", "Product Variants" },
            { "Main_Categories", "Categories" },
            { "Main_Brands", "Brands" },
            { "Main_ColorsSizes", "Colors & Sizes" },
            { "Main_StockCount", "Stock Count" },
            { "Main_StockMovements", "Stock Movements" },
            { "Main_BranchTransfer", "Branch Transfer" },
            { "Main_Sales", "Sales" },
            { "Main_PointOfSale", "Point of Sale" },
            { "Main_SalesInvoices", "Sales Invoices" },
            { "Main_Returns", "Returns" },
            { "Main_Customers", "Customers" },
            { "Main_Purchasing", "Purchases" },
            { "Main_PurchaseInvoices", "Purchase Invoices" },
            { "Main_Suppliers", "Suppliers" },
            { "Main_Treasury", "Treasury" },
            { "Main_TreasuryTransactions", "Treasury Transactions" },
            { "Main_TreasuryBalance", "TreasuryBalance" },
            { "Main_Reports", "Reports" },
            { "Main_SalesReport", "Sales Report" },
            { "Main_StockReport", "Stock Report" },
            { "Main_ProfitReport", "Profit Report" },
            { "Main_Settings", "Settings" },
            { "Main_Branches", "Branches" },
            { "Main_UsersRoles", "Users & Roles" },
            { "Main_PaymentMethods", "Payment Methods" },
            { "Main_AuditLogs", "AuditLogs" },
            { "Main_DarkMode", "Dark Mode" },

            // ===== Dashboard =====
            { "Dashboard_Overview", "Overview" },
            { "Dashboard_Today", "Today" },
            { "Dashboard_Last7Days", "Last 7 days" },
            { "Dashboard_Last14Days", "Last 14 days" },
            { "Dashboard_Last28Days", "Last 28 days" },
            { "Dashboard_Last60Days", "Last 60 days" },
            { "Dashboard_Last90Days", "Last 90 days" },
            { "Dashboard_Custom", "Custom" },
            { "Dashboard_From", "From:" },
            { "Dashboard_To", "To:" },
            { "Dashboard_Apply", "Apply" },
            { "Dashboard_IncomeVsExpenses", "Income vs Expenses" },
            { "Dashboard_LowStock", "Low Stock Items" },
            { "Dashboard_LatestSalesInvoices", "Latest Sales Invoices" },
            { "Dashboard_LatestPurchaseInvoices", "Latest Purchase Invoices" },
            { "Dashboard_TotalSalesMonth", "Total Sales (Month)" },
            { "Dashboard_TotalPurchasesMonth", "Total Purchases (Month)" },
            { "Dashboard_TotalIncomeMonth", "Total Income (Month)" },
            { "Dashboard_TotalExpensesMonth", "Total Expenses (Month)" },
            { "Dashboard_CustomersCount", "Number of Customers" },
            { "Dashboard_SuppliersCount", "Number of Suppliers" },
            { "Dashboard_SalesReturnsMonth", "Sales Returns (Month)" },
            { "Dashboard_Sales", "Sales" },
            { "Dashboard_Purchases", "Purchases" },
            { "Dashboard_Income", "Income" },
            { "Dashboard_Expenses", "Expenses" },
            { "Dashboard_CashCustomer", "Cash Customer" },

            // ===== Products =====
            { "Products_ColCode", "Code" },
            { "Products_ColName", "Name" },
            { "Products_ColBasePrice", "BasePrice" },
            { "Products_ColIsActive", "IsActive" },

            // ===== ProductVariants =====
            { "ProductVariants_ColProductName", "ProductName" },
            { "ProductVariants_ColColor", "Color" },
            { "ProductVariants_ColSize", "Size" },
            { "ProductVariants_ColBarcode", "Barcode" },
            { "ProductVariants_ColSalePrice", "SalePrice" },
            { "ProductVariants_ColCostPrice", "CostPrice" },
            { "ProductVariants_ColIsActive", "IsActive" },

            // ===== Categories =====
            { "Categories_ColName", "Name" },
            { "Categories_ColIsActive", "IsActive" },

            // ===== Brands =====
            { "Brands_ColName", "Name" },

            // ===== ColorsSizes =====
            { "ColorsSizes_Colors", "Colors" },
            { "ColorsSizes_Sizes", "Sizes" },

            // ===== StockCount =====
            { "StockCount_ColProduct", "Product" },
            { "StockCount_ColBranch", "Branch" },
            { "StockCount_ColQuantity", "Quantity" },
            { "StockCount_ColMinQuantity", "MinQuantity" },

            // ===== BranchTransfer =====
            { "BranchTransfer_ColFrom", "From" },
            { "BranchTransfer_ColTo", "To" },
            { "BranchTransfer_ColStatus", "Status" },
            { "BranchTransfer_ColCreatedAt", "CreatedAt" },

            // ===== POS =====
            { "POS_BtnAddManual", "Add" },
            { "POS_PickManually", "Or pick manually:" },
            { "POS_ScanBarcode", "Scan barcode :" },
            { "POS_BtnRemoveLine", "Remove Selected Item" },
            { "POS_BtnCheckout", "Checkout" },
            { "POS_Customer", "Customer:" },
            { "POS_PaymentMethod", "Payment Method:" },
            { "POS_Discount", "Discount:" },
            { "POS_Total", "Total: 0.00" },

            // ===== Returns =====
            { "Returns_ColInvoice", "Invoice" },
            { "Returns_ColBranch", "Branch" },
            { "Returns_ColReturnDate", "ReturnDate" },
            { "Returns_ColTotalAmount", "TotalAmount" },

            // ===== Customers =====
            { "Customers_ColName", "Name" },
            { "Customers_ColPhone", "Phone" },
            { "Customers_ColAddress", "Address" },
            { "Customers_ColIsActive", "IsActive" },

            // ===== Suppliers =====
            { "Suppliers_ColName", "Name" },
            { "Suppliers_ColPhone", "Phone" },
            { "Suppliers_ColAddress", "Address" },
            { "Suppliers_ColIsActive", "IsActive" },

            // ===== Purchases =====
            { "Purchases_ColSupplier", "Supplier" },
            { "Purchases_ColBranch", "Branch" },
            { "Purchases_ColInvoiceDate", "InvoiceDate" },
            { "Purchases_ColTotalAmount", "TotalAmount" },
            { "Purchases_ColPaidAmount", "PaidAmount" },
            { "Purchases_ColStatus", "Status" },

            // ===== PaymentMethods =====
            { "PaymentMethods_ColName", "Name" },

            // ===== UsersRoles =====
            { "UsersRoles_Users", "Users" },
            { "UsersRoles_Roles", "Roles" },
            { "UsersRoles_ColUsername", "Username" },
            { "UsersRoles_ColFullName", "FullName" },
            { "UsersRoles_ColRoleName", "RoleName" },
            { "UsersRoles_ColIsActive", "IsActive" },
            { "UsersRoles_ColRole_Name", "Name" },

            // ===== Branches =====
            { "Branches_ColName", "Name" },
            { "Branches_ColAddress", "Address" },
            { "Branches_ColPhone", "Phone" },
            { "Branches_ColIsActive", "IsActive" },

            // ===== Treasury =====
            { "Treasury_ColBranch", "Branch" },
            { "Treasury_ColTransactionType", "TransactionType" },
            { "Treasury_ColAmount", "Amount" },
            { "Treasury_ColDescription", "Description" },
            { "Treasury_ColCreatedAt", "CreatedAt" },

            // ===== TreasuryBalance =====
            { "TreasuryBalance_Refresh", "Refresh" },
            { "TreasuryBalance_CurrentBalance", "Current Balance: 0.00" },
            { "TreasuryBalance_InOutTotals", "Total In: 0.00   |   Total Out: 0.00" },
            { "TreasuryBalance_CurrentBalanceFmt", "Current Balance: {balance:n2}" },
            { "TreasuryBalance_InOutTotalsFmt", "Total In: {totalIn:n2}   |   Total Out: {totalOut:n2}" },

            // ===== AuditLogs =====
            { "AuditLogs_From", "From:" },
            { "AuditLogs_To", "To:" },
            { "AuditLogs_Table", "Table:" },
            { "AuditLogs_Refresh", "Refresh" },

            // ===== Reports =====
            { "Reports_Summary", "Total: 0.00  |  Invoices: 0" },
            { "Reports_GenerateReport", "Generate Report" },
            { "Reports_To", "To:" },
            { "Reports_From", "From:" },

        };
    }
}