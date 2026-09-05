using System.Collections.Generic;

namespace Clothes_Shop_ERP.Localization
{
    public static class Lang_English
    {
        public static readonly Dictionary<string, string> Strings =
            new Dictionary<string, string>
        {
            // ==================================================
            // ===== Shared / Repeated Strings ===================
            // ==================================================

            { "Shared_Refresh", "Refresh" },
            { "Shared_From", "From:" },
            { "Shared_To", "To:" },
            { "Shared_Name", "Name" },
            { "Shared_Phone", "Phone" },
            { "Shared_Address", "Address" },
            { "Shared_IsActive", "IsActive" },
            { "Shared_Status", "Status" },
            { "Shared_Branch", "Branch" },
            { "Shared_CreatedAt", "CreatedAt" },
            { "Shared_Description", "Description" },
            { "Shared_Amount", "Amount" },
            { "Shared_TotalAmount", "TotalAmount" },
            { "Shared_Code", "Code" },
            { "Shared_Color", "Color" },
            { "Shared_Size", "Size" },

            // ---- (New) Repeated across screens ----
            { "Shared_Success", "Success" },
            { "Shared_Error", "Error" },
            { "Shared_Warning", "Warning" },
            { "Shared_NoPermissionMsg", "You don't have permission to do this." },
            { "Shared_CannotDelete", "Cannot Delete" },
            { "Shared_NoXFoundWithId", "No {0} found with Id = {1}" },
            { "Shared_XAdded", "{0} added" },
            { "Shared_XUpdated", "{0} updated" },
            { "Shared_XDeleted", "{0} deleted" },
            { "Shared_XActionedPastTense", "{0} {1}d" },
            { "Shared_ColFrom", "From" },
            { "Shared_ColTo", "To" },
            { "Shared_SelectProductFirst", "Please select a product first." },
            { "Shared_AddAtLeastOneItem", "Please add at least one item." },

            // ---- (New) Repeated across Add/Edit forms (Category C) ----
            { "Shared_BtnSave", "Save" },
            { "Shared_BtnCancel", "Cancel" },
            { "Shared_Active", "Active" },
            { "Shared_ColName", "Name:" },
            { "Shared_ColBranch", "Branch:" },
            { "Shared_ColAddress", "Address:" },
            { "Shared_ColPhone", "Phone:" },
            { "Shared_AddItem", "Add item:" },
            { "Shared_Qty", "Qty:" },
            { "Shared_RemoveSelectedLine", "Remove Selected Line" },


            // ==================================================
            // ===== Common =====================================
            // ==================================================

            { "Common_ConfirmTitle", "Confirm" },
            { "Common_ConfirmDelete", "Delete '{0}'?" },
            { "Common_ConfirmAction", "{0} '{1}'?" },


            // ==================================================
            // ===== Login ======================================
            // ==================================================

            { "Login_Title", "NOVA ERP - Login" },
            { "Login_BtnLogin", "Login" },
            { "Login_WelcomeBack", "Welcome Back" },
            { "Login_PleaseSignIn", "Please sign in to continue" },
            { "Login_Username", "Username" },
            { "Login_Password", "Password" },
            { "Login_ColId", "ID" },
            { "Login_Branch", "Branch" },
            { "Login_ColName", "Branch Name" },


            // ==================================================
            // ===== Main =======================================
            // ==================================================

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
            { "Main_TreasuryBalance", "Treasury Balance" },
            { "Main_Reports", "Reports" },
            { "Main_SalesReport", "Sales Report" },
            { "Main_StockReport", "Stock Report" },
            { "Main_ProfitReport", "Profit Report" },
            { "Main_Settings", "Settings" },
            { "Main_Branches", "Branches" },
            { "Main_UsersRoles", "Users & Roles" },
            { "Main_PaymentMethods", "Payment Methods" },
            { "Main_AuditLogs", "Audit Logs" },
            { "Main_DarkMode", "Dark Mode" },


            // ==================================================
            // ===== Dashboard ==================================
            // ==================================================

            { "Dashboard_Overview", "Overview" },
            { "Dashboard_Today", "Today" },
            { "Dashboard_Last7Days", "Last 7 days" },
            { "Dashboard_Last14Days", "Last 14 days" },
            { "Dashboard_Last28Days", "Last 28 days" },
            { "Dashboard_Last60Days", "Last 60 days" },
            { "Dashboard_Last90Days", "Last 90 days" },
            { "Dashboard_Custom", "Custom" },
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


            // ==================================================
            // ===== Products ===================================
            // ==================================================

            { "Products_ColBasePrice", "BasePrice" },


            // ==================================================
            // ===== Product Variants ===========================
            // ==================================================

            { "ProductVariants_ColProductName", "ProductName" },
            { "ProductVariants_ColBarcode", "Barcode" },
            { "ProductVariants_ColSalePrice", "SalePrice" },
            { "ProductVariants_ColCostPrice", "CostPrice" },


            // ==================================================
            // ===== Colors & Sizes ==============================
            // ==================================================

            { "ColorsSizes_Colors", "Colors" },
            { "ColorsSizes_Sizes", "Sizes" },


            // ==================================================
            // ===== Stock Count =================================
            // ==================================================

            { "StockCount_ColProduct", "Product" },
            { "StockCount_ColQuantity", "Quantity" },
            { "StockCount_ColMinQuantity", "MinQuantity" },


            // ==================================================
            // ===== POS ========================================
            // ==================================================

            { "POS_BtnAddManual", "Add" },
            { "POS_PickManually", "Or pick manually:" },
            { "POS_ScanBarcode", "Scan barcode :" },
            { "POS_BtnRemoveLine", "Remove Selected Item" },
            { "POS_BtnCheckout", "Checkout" },
            { "POS_Customer", "Customer:" },
            { "POS_PaymentMethod", "Payment Method:" },
            { "POS_Discount", "Discount:" },
            { "POS_Total", "Total: 0.00" },


            // ==================================================
            // ===== Returns ====================================
            // ==================================================

            { "Returns_ColInvoice", "Invoice" },
            { "Returns_ColReturnDate", "ReturnDate" },


            // ==================================================
            // ===== Purchases ==================================
            // ==================================================

            { "Purchases_ColSupplier", "Supplier" },
            { "Purchases_ColInvoiceDate", "InvoiceDate" },
            { "Purchases_ColPaidAmount", "PaidAmount" },


            // ==================================================
            // ===== Users & Roles ===============================
            // ==================================================

            { "UsersRoles_Users", "Users" },
            { "UsersRoles_Roles", "Roles" },
            { "UsersRoles_ColUsername", "Username" },
            { "UsersRoles_ColFullName", "FullName" },
            { "UsersRoles_ColRoleName", "RoleName" },


            // ==================================================
            // ===== Treasury ===================================
            // ==================================================

            { "Treasury_ColTransactionType", "TransactionType" },


            // ==================================================
            // ===== Treasury Balance ===========================
            // ==================================================

            { "TreasuryBalance_CurrentBalance", "Current Balance: 0.00" },
            { "TreasuryBalance_InOutTotals", "Total In: 0.00   |   Total Out: 0.00" },
            { "TreasuryBalance_CurrentBalanceFmt", "Current Balance: {0:n2}" },
            { "TreasuryBalance_InOutTotalsFmt", "Total In: {0:n2}   |   Total Out: {1:n2}" },


            // ==================================================
            // ===== Audit Logs =================================
            // ==================================================

            { "AuditLogs_Table", "Table:" },


            // ==================================================
            // ===== Reports ====================================
            // ==================================================

            { "Reports_Summary", "Total: 0.00  |  Invoices: 0" },
            { "Reports_GenerateReport", "Generate Report" },


            // ==================================================
            // ===== Other ======================================
            // ==================================================

            { "txtAll", "All" },

            // ==================================================
            // ===== (New) Additional Strings — added by audit ==
            // ==================================================

            // ----- Login -----
            { "Login_EnterCredentials", "Please enter your username and password" },
            { "Login_SelectBranch", "Please select a branch" },
            { "Login_UsernameNotFound", "Username not found" },
            { "Login_IncorrectPassword", "Incorrect password" },
            { "Login_WelcomeUser", "Welcome, {0}" },
            { "Login_Failed", "Login Failed" },
            { "Login_WelcomeTitle", "Welcome" },

            // ----- Payment Methods -----
            { "PaymentMethods_InUse", "This payment method is used by existing invoices. It can't be removed." },

            // ----- Users & Roles -----
            { "UsersRoles_UsernameTaken", "This username is already taken." },
            { "UsersRoles_UserHasRecords", "This user created invoices or other records. Deactivate instead of deleting." },
            { "UsersRoles_CannotDeleteSelf", "You can't delete the account you're currently logged in with." },
            { "UsersRoles_CannotDeactivateSelf", "You can't deactivate the account you're currently logged in with." },
            { "UsersRoles_CannotDeactivateLastUser", "This is the last active user account - deactivating or deleting it would leave nobody able to log in." },
            { "UsersRoles_RoleAssigned", "This role is assigned to one or more users. Reassign them first." },
            { "UsersRoles_FillUsernameFullName", "Please fill in username and full name." },
            { "UsersRoles_PasswordRequiredForNewUser", "Please enter a password for the new user." },

            // ----- Branches -----
            { "Branches_HasRelatedData", "This branch has related data (users, invoices, stock...). Remove those first." },
            { "Branches_NameRequired", "Please enter a branch name." },

            // ----- Customers & Suppliers -----
            { "Customers_HasInvoices", "This customer has sales invoices linked to them. Deactivate instead." },
            { "Suppliers_HasInvoices", "This supplier has purchase invoices linked to it. Deactivate instead." },
            { "Party_NameRequired", "Please enter a name." },

            // ----- Treasury -----
            { "Treasury_ConfirmDeleteEntry", "Delete this entry?" },
            { "Treasury_AmountGreaterThanZero", "Please enter an amount greater than zero." },

            // ----- Returns -----
            { "Returns_SaveFailed", "Could not save the return. Nothing was changed. {0}" },
            { "Returns_Recorded", "Return recorded. Amount: {0:n2}" },
            { "Returns_SelectInvoiceAndItem", "Please select an invoice and an item." },

            // ----- Purchase Returns -----
            { "Main_PurchaseReturns", "Purchase Returns" },
            { "FrmPurchaseReturnEdit_Invoice", "Purchase Invoice:" },
            { "PurchaseReturns_NewTitle", "New Purchase Return" },
            { "PurchaseReturns_NotEnoughStock", "Not enough stock on hand to return this quantity." },
            { "PurchaseReturns_Recorded", "Return to supplier recorded. Amount: {0:n2}" },
            { "PurchaseReturns_SaveFailed", "Could not save the return. Nothing was changed. {0}" },

            // ----- POS -----
            { "POS_EmptyCartMsg", "Please add at least one item before checking out." },
            { "POS_DiscountExceedsTotal", "The discount can't be more than the cart total." },
            { "POS_SaleFailed", "Could not complete the sale. Nothing was charged. {0}" },
            { "POS_ProductNotFoundByBarcode", "No active product with barcode {0}" },
            { "POS_NotEnoughStockFor", "Not enough stock for {0}. Please refresh and try again." },
            { "POS_SaleCompletedMsg", "Invoice {0} - Total: {1:n2}" },
            { "POS_NotFoundTitle", "Not Found" },
            { "POS_EmptyCartTitle", "Empty Cart" },
            { "POS_OutOfStockTitle", "Out of Stock" },
            { "POS_SaleCompletedTitle", "Sale Completed" },

            // ----- Product Variants -----
            { "ProductVariants_BarcodeUsed", "This barcode is already used." },
            { "ProductVariants_CombinationExists", "This exact combination (same product, color, and size) already exists." },
            { "ProductVariants_HasStockOrSales", "This variant has stock or sales linked to it. Deactivate instead." },
            { "ProductVariants_BarcodeRequired", "Please enter a barcode." },

            // ----- Categories -----
            { "Categories_HasChildren", "This category has products or sub-categories linked to it. Remove those first." },

            // ----- Stock Count -----
            { "StockCount_EntryExists", "This variant already has a stock entry for this branch. Edit it instead." },
            { "StockCount_ConfirmDeleteEntry", "Delete this stock entry?" },

            // ----- Branch Transfer -----
            { "BranchTransfer_CreatedPending", "Transfer created as Pending. Mark it Completed once the stock has actually moved." },
            { "BranchTransfer_CreateFailed", "Could not create the transfer. {0}" },
            { "BranchTransfer_Locked", "This transfer can no longer be changed." },
            { "BranchTransfer_NotEnoughStock", "Not enough stock at the source branch for one of the items. Transfer not completed." },
            { "BranchTransfer_StatusChanged", "Transfer marked as {0}" },
            { "BranchTransfer_UpdateFailed", "Could not update the transfer. {0}" },
            { "BranchTransfer_FromToMustDiffer", "From and To branches must be different." },

            // ----- Brands -----
            { "Brands_LinkedToProducts", "This brand is linked to one or more products. Remove those first." },

            // ----- Colors & Sizes -----
            { "ColorsSizes_ColorInUse", "This color is used by one or more product variants. Remove those first." },
            { "ColorsSizes_SizeInUse", "This size is used by one or more product variants. Remove those first." },
            { "ColorsSizes_ColorNameRequired", "Please enter a color name." },
            { "ColorsSizes_SizeNameRequired", "Please enter a size name." },

            // ----- Products -----
            { "Products_HasVariantsOrSales", "This product has variants or sales linked to it. Deactivate instead." },
            { "Products_CodeNameRequired", "Please fill in the code and name." },

            // ----- Purchases -----
            { "Purchases_SaveFailed", "Could not save the invoice. Nothing was changed. {0}" },
            { "Purchases_SavedStatus", "Invoice saved — {0}." },
            { "Purchases_AddAtLeastOneInvoiceItem", "Please add at least one item to the invoice." },

            // ----- Sales Invoices -----
            { "SalesInvoices_DetailsTitle", "Invoice Details" },

            // ----- Stock Report -----
            { "StockReport_TotalInventoryValueFmt", "Total Inventory Value: {0:n2}   |   Low Stock Items: {1}" },

            // ==================================================
            // ===== (New) Add/Edit Forms (Category C) ==========
            // ==================================================

            // ----- FrmUserEdit -----
            { "FrmUserEdit_Username", "Username:" },
            { "FrmUserEdit_Password", "Password:" },
            { "FrmUserEdit_NewPasswordHint", "New Password (leave empty to keep current):" },
            { "FrmUserEdit_FullName", "Full Name:" },
            { "FrmUserEdit_Role", "Role:" },

            // ----- FrmTreasuryEdit -----
            { "FrmTreasuryEdit_Type", "Type:" },
            { "FrmTreasuryEdit_Amount", "Amount:" },
            { "FrmTreasuryEdit_Description", "Description:" },

            // ----- FrmReturnEdit -----
            { "FrmReturnEdit_Invoice", "Invoice:" },
            { "FrmReturnEdit_ItemToReturn", "Item to return:" },
            { "FrmReturnEdit_QuantityToReturn", "Quantity to return:" },
            { "FrmReturnEdit_BtnSaveReturn", "Save Return" },

            // ----- FrmVariantEdit -----
            { "FrmVariantEdit_Product", "Product:" },
            { "FrmVariantEdit_Color", "Color:" },
            { "FrmVariantEdit_Size", "Size:" },
            { "FrmVariantEdit_Barcode", "Barcode:" },
            { "FrmVariantEdit_SalePrice", "Sale Price:" },
            { "FrmVariantEdit_CostPrice", "Cost Price:" },

            // ----- FrmStockCountEdit -----
            { "FrmStockCountEdit_ProductVariant", "Product Variant:" },
            { "FrmStockCountEdit_Quantity", "Quantity:" },
            { "FrmStockCountEdit_MinQuantityHint", "Minimum Quantity (reorder alert):" },

            // ----- Frmstocktransferedit -----
            { "StockTransferEdit_FromBranch", "From Branch:" },
            { "StockTransferEdit_ToBranch", "To Branch:" },
            { "StockTransferEdit_BtnSaveTransfer", "Save Transfer" },

            // ----- FrmColorEdit -----
            { "FrmColorEdit_PickColor", "Pick a color:" },

            // ----- FrmSizeEdit -----
            { "FrmSizeEdit_NameHint", "Name (e.g. M, L, XL):" },
            { "FrmSizeEdit_SortOrder", "Sort Order:" },

            // ----- FrmProductEdit -----
            { "FrmProductEdit_Code", "Code:" },
            { "FrmProductEdit_Category", "Category:" },
            { "FrmProductEdit_BrandOptional", "Brand (optional):" },
            { "FrmProductEdit_BasePrice", "Base Price:" },
            { "FrmProductEdit_NoneOption", "(None)" },

            // ----- FrmPurchaseInvoiceEdit -----
            { "FrmPurchaseInvoiceEdit_Supplier", "Supplier:" },
            { "FrmPurchaseInvoiceEdit_UnitCost", "Unit Cost:" },
            { "FrmPurchaseInvoiceEdit_AmountPaidNow", "Amount Paid Now:" },
            { "FrmPurchaseInvoiceEdit_PaidHint", "(Leave as 0 for a fully credit/unpaid purchase)" },
            { "FrmPurchaseInvoiceEdit_BtnSaveInvoice", "Save Invoice" },
            { "FrmPurchaseInvoiceEdit_TotalFmt", "Total: {0:n2}" },

            // ==================================================
            // ===== (New) Edit Form Titles ("New X" / "Editing X") =====
            // ==================================================

            { "Treasury_NewEntryTitle", "New Treasury Entry" },
            { "Treasury_EditingEntryTitle", "Editing Entry" },
            { "Purchases_NewInvoiceTitle", "New Purchase Invoice" },
            { "UsersRoles_NewUserTitle", "New User" },
            { "UsersRoles_EditingUserTitleFmt", "Editing User: {0}" },
            { "Suppliers_NewTitle", "New Supplier" },
            { "Customers_NewTitle", "New Customer" },
            { "Party_EditingTitleFmt", "Editing: {0}" },
            { "Returns_NewTitle", "New Return" },
            { "StockCount_NewEntryTitle", "New Stock Entry" },
            { "StockCount_EditQuantityTitle", "Edit Stock Quantity" },
            { "ProductVariants_NewTitle", "New Variant" },
            { "ProductVariants_EditingTitleFmt", "Editing: {0}" },
            { "Products_NewTitle", "New Product" },
            { "Products_EditingTitleFmt", "Editing: {0}" },
            { "ColorsSizes_NewColorTitle", "New Color" },
            { "ColorsSizes_EditingColorTitleFmt", "Editing Color: {0}" },
            { "ColorsSizes_NewSizeTitle", "New Size" },
            { "ColorsSizes_EditingSizeTitleFmt", "Editing Size: {0}" },
            { "BranchTransfer_NewTitle", "New Stock Transfer" },
            { "Branches_NewTitle", "New Branch" },
            { "Branches_EditingTitleFmt", "Editing Branch: {0}" },

            // ==================================================
            // ===== (New) Language Restart Prompt ==============
            // ==================================================

            { "Common_RestartForLanguage", "The app needs to restart to apply the new language. Restart now?" },

            // ==================================================
            // ===== (New) Entity Names (for Shared_X* formatting) =====
            // ==================================================

            { "Products_EntityName", "Product" },
            { "ProductVariants_EntityName", "Variant" },
            { "Categories_EntityName", "Category" },
            { "Brands_EntityName", "Brand" },
            { "ColorsSizes_ColorEntityName", "Color" },
            { "ColorsSizes_SizeEntityName", "Size" },
            { "Customers_EntityName", "Customer" },
            { "Suppliers_EntityName", "Supplier" },
            { "PaymentMethods_EntityName", "Payment method" },
            { "UsersRoles_UserEntityName", "User" },
            { "UsersRoles_RoleEntityName", "Role" },
            { "Branches_EntityName", "Branch" },
            { "Branches_CannotDeactivateLast", "This is the only branch left - the app needs at least one to work." },
            { "StockCount_EntityName", "Stock entry" },
            { "Treasury_EntityName", "Treasury entry" },
            { "Shared_Activate", "Activate" },
            { "Shared_Deactivate", "Deactivate" },
            { "Shared_MenuNew", "New" },
            { "Shared_MenuEdit", "Edit" },
            { "Shared_MenuActivateDeactivate", "Activate/Deactivate" },
            { "Shared_MenuDelete", "Delete" },

            // ----- Categories (inline name prompt) -----
            { "Categories_NamePrompt", "Category name:" },
            { "Categories_NewTitle", "New Category" },
            { "Categories_EditNamePrompt", "Enter new category name:" },
            { "Categories_EditingTitleFmt", "Editing Category: {0}" },

            // ----- Brands (inline name prompt) -----
            { "Brands_NamePrompt", "Brand name:" },
            { "Brands_NewTitle", "New Brand" },
            { "Brands_EditNamePrompt", "Enter new brand name:" },
            { "Brands_EditingTitleFmt", "Editing Brand: {0}" },

            // ----- Branch Transfer (context menu) -----
            { "BranchTransfer_MenuNewTransfer", "New Transfer" },
            { "BranchTransfer_MenuMarkCompleted", "Mark Completed (moves the stock)" },
            { "BranchTransfer_MenuCancelTransfer", "Cancel Transfer" },

            // ----- Sales Invoices -----
            { "SalesInvoices_WalkInCustomer", "Walk-in" },
            { "Shared_MenuViewDetails", "View Details" },

            // ----- POS (additional) -----
            { "POS_WalkInCustomer", "Walk-in Customer" },
            { "POS_TotalFmt", "Total: {0:n2}" },

            // ----- Purchases (additional) -----
            { "Purchases_FullyPaid", "fully paid" },
            { "Purchases_PartiallyPaidFmt", "partially paid ({0:n2} of {1:n2})" },

            // ----- Treasury (additional) -----
            { "FrmTreasuryEdit_TypeIn", "In (Cash received)" },
            { "FrmTreasuryEdit_TypeOut", "Out (Cash paid)" },

            // ----- Roles (inline name prompt) -----
            { "Roles_NamePrompt", "Role name:" },
            { "Roles_NewTitle", "New Role" },
            { "Roles_EditNamePrompt", "Enter new role name:" },
            { "Roles_EditingTitleFmt", "Editing Role: {0}" },
            { "Roles_NameRequired", "Please enter a role name." },
            { "Roles_CannotDeleteProtected", "This role can't be deleted - it's the account of last resort and always keeps full access." },
            { "Roles_ProtectedAdminNote", "This role always has full access to every screen and can't be restricted:" },
            { "Roles_PermissionsGroupTitle", "Screen permissions:" },
            { "Roles_ColScreen", "Screen" },
            { "Roles_ColPermission", "Permission" },
            { "Permission_None", "None" },
            { "Permission_Read", "Read" },
            { "Permission_Write", "Read and Write" },

            // ----- Payment Methods (inline name prompt) -----
            { "PaymentMethods_NamePrompt", "Payment method name:" },
            { "PaymentMethods_NewTitle", "New Payment Method" },
            { "PaymentMethods_EditNamePrompt", "Enter new name:" },
            { "PaymentMethods_EditingTitleFmt", "Editing: {0}" },

            // ----- Profit Report -----
            { "ProfitReport_SummaryFmt", "Revenue: {0:n2}  |  Cost: {1:n2}  |  Profit: {2:n2}  ({3:n1}%)" },
            { "Reports_SummaryFmt", "Total: {0:n2}  |  Invoices: {1}" },

            // ----- (New) Auto-generated grid columns (data-bound screens) -----
            { "AuditLogs_ColTable", "Table" },
            { "AuditLogs_ColChangedAt", "Changed At" },
            { "AuditLogs_ColRecordId", "Record Id" },
            { "AuditLogs_ColAction", "Action" },
            { "AuditLogs_ColUser", "User" },
            { "AuditLogs_SystemUser", "System" },
            { "SalesInvoices_ColInvoiceNumber", "Invoice Number" },
            { "SalesInvoices_ColCustomer", "Customer" },
            { "SalesInvoices_ColNetAmount", "Net Amount" },
            { "ColorsSizes_ColHexCode", "Hex Code" },
            { "ColorsSizes_ColSortOrder", "Sort Order" },
            { "TreasuryBalance_ColTotalIn", "Total In" },
            { "TreasuryBalance_ColTotalOut", "Total Out" },
            { "TreasuryBalance_ColBalance", "Balance" },
            { "StockReport_ColValue", "Value" },
            { "ProfitReport_ColQuantitySold", "Quantity Sold" },
            { "ProfitReport_ColRevenue", "Revenue" },
            { "ProfitReport_ColCost", "Cost" },
            { "ProfitReport_ColProfit", "Profit" },
            { "StockMovements_ColMovementType", "Movement Type" },
            { "StockMovements_ColRefType", "Reference Type" },
            { "StockMovements_ColRefId", "Reference Id" },
            { "Dashboard_ColDue", "Due" },
            { "Shared_ColTotal", "Total" },
            { "Purchases_ColUnitCost", "Unit Cost" },
            { "POS_ColUnitPrice", "Unit Price" },

            // ----- Profit Report (Net Profit) -----
            { "ProfitReport_NetSummaryFmt", "Revenue: {0:n2}  |  Cost: {1:n2}  |  Gross Profit: {2:n2}  ({3:n1}%)\nGeneral Expenses: {4:n2}  |  Net Profit: {5:n2}" },
            { "ProfitReport_GeneralExpensesHint", "General Expenses = manual Treasury entries (e.g. electricity, rent) that are not tied to any purchase or sale." },

            // ==================================================
            // ===== Account Statement ==========================
            // ==================================================

            { "Main_AccountStatement", "Account Statement" },
            { "AccountStatement_TypeCustomer", "Customer" },
            { "AccountStatement_TypeSupplier", "Supplier" },
            { "AccountStatement_SelectPartyFirst", "Please select a customer or supplier first." },
            { "AccountStatement_ColInvoiceDate", "Date" },
            { "AccountStatement_SummaryFmt", "Total Invoiced: {0:n2}  |  Total Paid: {1:n2}  |  Total Due: {2:n2}" },

            // ==================================================
            // ===== Day Closing Report (Z-Report) ==============
            // ==================================================

            { "Main_DayClosingReport", "Day Closing (Z-Report)" },
            { "DayClosing_Date", "Date:" },
            { "DayClosing_ColMethod", "Payment Method" },
            { "DayClosing_ColCount", "Count" },
            { "DayClosing_SummaryFmt", "Invoices: {0}   |   Total Sales: {1:n2}\nReturns: {2}   |   Total Returns: {3:n2}\nNet Sales: {4:n2}\n\nCash In - From Sales: {5:n2}   |   Other: {6:n2}   |   Total In: {7:n2}\nCash Out - To Suppliers: {8:n2}   |   Refunds: {9:n2}   |   General Expenses: {10:n2}   |   Total Out: {11:n2}\n\nNet Cash Movement Today: {12:n2}" },

            // ==================================================
            // ===== Backup Settings =============================
            // ==================================================

            { "Main_BackupSettings", "Backup" },
            { "Backup_Folder", "Backup Folder:" },
            { "Backup_BtnBrowse", "Browse..." },
            { "Backup_BtnSave", "Save Folder" },
            { "Backup_BtnBackupNow", "Backup Now" },
            { "Backup_BtnSaveAs", "Save Database As..." },
            { "Backup_FileFilter", "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*" },
            { "Backup_LastBackupFmt", "Last backup: {0:g}" },
            { "Backup_NeverBackedUp", "Last backup: never" },
            { "Backup_FolderRequired", "Please choose a backup folder first." },
            { "Backup_Success", "Backup completed successfully." },
            { "Backup_Failed", "Backup failed. Check that SQL Server can write to the chosen folder." },
            { "Backup_FolderSaved", "Backup folder saved." },
            { "Backup_Hint", "A daily backup runs automatically the first time the app opens each day, once a folder is set here." },

            // ----- Receipt printing -----
            { "Receipt_ThankYou", "Thank you for shopping with us!" },
            { "Receipt_InvoiceLabel", "Invoice:" },
            { "Receipt_CashierLabel", "Cashier:" },
            { "Receipt_ItemsLabel", "Items:" },
            { "Receipt_UnitsLabel", "units" },
            { "Receipt_SubtotalLabel", "Subtotal:" },
            { "Receipt_TotalLabel", "TOTAL:" },
            { "Shared_MenuPrintReceipt", "Print Receipt" },

            // ==================================================
            // ===== Licensing / Activation ======================
            // ==================================================

            { "Activation_Title", "NOVA ERP - Activation Required" },
            { "Activation_Intro", "This copy of the program is not activated yet. Send the computer ID below to your software provider to get a license key." },
            { "Activation_MachineId", "This Computer's ID:" },
            { "Activation_BtnCopy", "Copy" },
            { "Activation_LicenseKey", "License Key:" },
            { "Activation_BtnActivate", "Activate" },
            { "Activation_BtnExit", "Exit" },
            { "Activation_EnterKeyFirst", "Please paste the license key you received." },
            { "Activation_Success", "Activated successfully. The program will now continue." },
            { "Activation_IdCopied", "Computer ID copied to the clipboard." },
            { "Activation_ExitConfirm", "The program can't run without activation. Exit now?" },

            { "LicenseGen_Title", "License Key Generator (Vendor Only)" },
            { "LicenseGen_MachineId", "Customer's Machine ID:" },
            { "LicenseGen_SetExpiry", "Expires on:" },
            { "LicenseGen_BtnGenerate", "Generate Key" },
            { "LicenseGen_ResultKey", "License Key (send this to the customer):" },
            { "LicenseGen_BtnCopy", "Copy" },
            { "LicenseGen_EnterIdFirst", "Please paste the customer's Machine ID first." },
            { "LicenseGen_Copied", "License key copied to the clipboard." }
        };
    }
}
