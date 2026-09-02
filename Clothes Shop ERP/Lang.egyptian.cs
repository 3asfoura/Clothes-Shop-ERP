using System.Collections.Generic;

namespace Clothes_Shop_ERP.Localization
{
    public static class Lang_Arabic
    {
        public static readonly Dictionary<string, string> Strings =
            new Dictionary<string, string>
        {
            // ==================================================
            // ===== Shared / Repeated Strings ===================
            // ===== الكلمات المستخدمة في أكتر من مكان ============
            // ==================================================

            { "Shared_Refresh", "تحديث" },
            { "Shared_From", "من:" },
            { "Shared_To", "إلى:" },
            { "Shared_Name", "الاسم" },
            { "Shared_Phone", "رقم الهاتف" },
            { "Shared_Address", "العنوان" },
            { "Shared_IsActive", "نشط" },
            { "Shared_Status", "الحالة" },
            { "Shared_Branch", "الفرع" },
            { "Shared_CreatedAt", "تاريخ الإنشاء" },
            { "Shared_Description", "الوصف" },
            { "Shared_Amount", "المبلغ" },
            { "Shared_TotalAmount", "إجمالي المبلغ" },
            { "Shared_Code", "الكود" },
            { "Shared_Color", "اللون" },
            { "Shared_Size", "المقاس" },


            // ==================================================
            // ===== Common =====================================
            // ==================================================

            { "Common_ConfirmTitle", "تأكيد" },
            { "Common_ConfirmDelete", "هل تريد حذف '{0}'؟" },
            { "Common_ConfirmAction", "هل تريد {0} '{1}'؟" },


            // ==================================================
            // ===== Login ======================================
            // ==================================================

            { "Login_Title", "NOVA ERP - تسجيل الدخول" },
            { "Login_BtnLogin", "تسجيل الدخول" },
            { "Login_WelcomeBack", "أهلًا بعودتك" },
            { "Login_PleaseSignIn", "سجل دخولك للمتابعة" },
            { "Login_Username", "اسم المستخدم" },
            { "Login_Password", "كلمة المرور" },
            { "Login_ColId", "المعرف" },


            // ==================================================
            // ===== Main =======================================
            // ==================================================

            { "Main_Inventory", "المخزون" },
            { "Main_Products", "المنتجات" },
            { "Main_ProductVariants", "تفاصيل المنتجات" },
            { "Main_Categories", "الأقسام" },
            { "Main_Brands", "العلامات التجارية" },
            { "Main_ColorsSizes", "الألوان والمقاسات" },
            { "Main_StockCount", "جرد المخزون" },
            { "Main_StockMovements", "حركة المخزون" },
            { "Main_BranchTransfer", "تحويل بين الفروع" },
            { "Main_Sales", "المبيعات" },
            { "Main_PointOfSale", "نقطة البيع" },
            { "Main_SalesInvoices", "فواتير المبيعات" },
            { "Main_Returns", "المرتجعات" },
            { "Main_Customers", "العملاء" },
            { "Main_Purchasing", "المشتريات" },
            { "Main_PurchaseInvoices", "فواتير المشتريات" },
            { "Main_Suppliers", "الموردون" },
            { "Main_Treasury", "الخزنة" },
            { "Main_TreasuryTransactions", "حركات الخزنة" },
            { "Main_TreasuryBalance", "رصيد الخزنة" },
            { "Main_Reports", "التقارير" },
            { "Main_SalesReport", "تقرير المبيعات" },
            { "Main_StockReport", "تقرير المخزون" },
            { "Main_ProfitReport", "تقرير الأرباح" },
            { "Main_Settings", "الإعدادات" },
            { "Main_Branches", "الفروع" },
            { "Main_UsersRoles", "المستخدمون والصلاحيات" },
            { "Main_PaymentMethods", "طرق الدفع" },
            { "Main_AuditLogs", "سجل العمليات" },
            { "Main_DarkMode", "الوضع الداكن" },


            // ==================================================
            // ===== Dashboard ==================================
            // ==================================================

            { "Dashboard_Overview", "نظرة عامة" },
            { "Dashboard_Today", "اليوم" },
            { "Dashboard_Last7Days", "آخر 7 أيام" },
            { "Dashboard_Last14Days", "آخر 14 يوم" },
            { "Dashboard_Last28Days", "آخر 28 يوم" },
            { "Dashboard_Last60Days", "آخر 60 يوم" },
            { "Dashboard_Last90Days", "آخر 90 يوم" },
            { "Dashboard_Custom", "تحديد فترة" },
            { "Dashboard_Apply", "تطبيق" },
            { "Dashboard_IncomeVsExpenses", "الإيرادات والمصروفات" },
            { "Dashboard_LowStock", "المنتجات قليلة المخزون" },
            { "Dashboard_LatestSalesInvoices", "أحدث فواتير المبيعات" },
            { "Dashboard_LatestPurchaseInvoices", "أحدث فواتير المشتريات" },
            { "Dashboard_TotalSalesMonth", "إجمالي مبيعات الشهر" },
            { "Dashboard_TotalPurchasesMonth", "إجمالي مشتريات الشهر" },
            { "Dashboard_TotalIncomeMonth", "إجمالي إيرادات الشهر" },
            { "Dashboard_TotalExpensesMonth", "إجمالي مصروفات الشهر" },
            { "Dashboard_CustomersCount", "عدد العملاء" },
            { "Dashboard_SuppliersCount", "عدد الموردين" },
            { "Dashboard_SalesReturnsMonth", "مرتجعات المبيعات للشهر" },
            { "Dashboard_Sales", "المبيعات" },
            { "Dashboard_Purchases", "المشتريات" },
            { "Dashboard_Income", "الإيرادات" },
            { "Dashboard_Expenses", "المصروفات" },
            { "Dashboard_CashCustomer", "عميل نقدي" },


            // ==================================================
            // ===== Products ===================================
            // ==================================================

            { "Products_ColBasePrice", "السعر الأساسي" },


            // ==================================================
            // ===== Product Variants ===========================
            // ==================================================

            { "ProductVariants_ColProductName", "اسم المنتج" },
            { "ProductVariants_ColBarcode", "الباركود" },
            { "ProductVariants_ColSalePrice", "سعر البيع" },
            { "ProductVariants_ColCostPrice", "سعر التكلفة" },


            // ==================================================
            // ===== Colors & Sizes ==============================
            // ==================================================

            { "ColorsSizes_Colors", "الألوان" },
            { "ColorsSizes_Sizes", "المقاسات" },


            // ==================================================
            // ===== Stock Count =================================
            // ==================================================

            { "StockCount_ColProduct", "المنتج" },
            { "StockCount_ColQuantity", "الكمية" },
            { "StockCount_ColMinQuantity", "الحد الأدنى للكمية" },


            // ==================================================
            // ===== POS ========================================
            // ==================================================

            { "POS_BtnAddManual", "إضافة" },
            { "POS_PickManually", "أو اختر المنتج يدويًا:" },
            { "POS_ScanBarcode", "امسح الباركود:" },
            { "POS_BtnRemoveLine", "حذف المنتج المحدد" },
            { "POS_BtnCheckout", "إتمام البيع" },
            { "POS_Customer", "العميل:" },
            { "POS_PaymentMethod", "طريقة الدفع:" },
            { "POS_Discount", "الخصم:" },
            { "POS_Total", "الإجمالي: 0.00" },


            // ==================================================
            // ===== Returns ====================================
            // ==================================================

            { "Returns_ColInvoice", "الفاتورة" },
            { "Returns_ColReturnDate", "تاريخ المرتجع" },


            // ==================================================
            // ===== Purchases ==================================
            // ==================================================

            { "Purchases_ColSupplier", "المورد" },
            { "Purchases_ColInvoiceDate", "تاريخ الفاتورة" },
            { "Purchases_ColPaidAmount", "المبلغ المدفوع" },


            // ==================================================
            // ===== Users & Roles ===============================
            // ==================================================

            { "UsersRoles_Users", "المستخدمون" },
            { "UsersRoles_Roles", "الصلاحيات" },
            { "UsersRoles_ColUsername", "اسم المستخدم" },
            { "UsersRoles_ColFullName", "الاسم بالكامل" },
            { "UsersRoles_ColRoleName", "اسم الصلاحية" },


            // ==================================================
            // ===== Treasury ===================================
            // ==================================================

            { "Treasury_ColTransactionType", "نوع العملية" },


            // ==================================================
            // ===== Treasury Balance ===========================
            // ==================================================

            { "TreasuryBalance_CurrentBalance", "الرصيد الحالي: 0.00" },
            { "TreasuryBalance_InOutTotals", "إجمالي الداخل: 0.00 | إجمالي الخارج: 0.00" },
            { "TreasuryBalance_CurrentBalanceFmt", "الرصيد الحالي: {balance:n2}" },
            { "TreasuryBalance_InOutTotalsFmt", "إجمالي الداخل: {totalIn:n2} | إجمالي الخارج: {totalOut:n2}" },


            // ==================================================
            // ===== Audit Logs =================================
            // ==================================================

            { "AuditLogs_Table", "الجدول:" },


            // ==================================================
            // ===== Reports ====================================
            // ==================================================

            { "Reports_Summary", "الإجمالي: 0.00 | عدد الفواتير: 0" },
            { "Reports_GenerateReport", "إنشاء التقرير" },


            // ==================================================
            // ===== Other ======================================
            // ==================================================

            { "txtAll", "الكل" }
        };
    }
}