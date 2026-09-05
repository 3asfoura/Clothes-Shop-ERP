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
            // ==================================================

            { "Shared_Refresh", "تحديث" },
            { "Shared_From", "من:" },
            { "Shared_To", "إلى:" },
            { "Shared_Name", "الاسم" },
            { "Shared_Phone", "الهاتف" },
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

            // ---- (New) Repeated across screens ----
            { "Shared_Success", "نجاح" },
            { "Shared_Error", "خطأ" },
            { "Shared_Warning", "تحذير" },
            { "Shared_CannotDelete", "لا يمكن الحذف" },
            { "Shared_NoXFoundWithId", "لا يوجد {0} برقم = {1}" },
            { "Shared_XAdded", "تمت إضافة {0}" },
            { "Shared_XUpdated", "تم تحديث {0}" },
            { "Shared_XDeleted", "تم حذف {0}" },
            { "Shared_XActionedPastTense", "تم {1} {0}" },
            { "Shared_ColFrom", "من" },
            { "Shared_ColTo", "إلى" },
            { "Shared_SelectProductFirst", "الرجاء اختيار منتج أولاً." },
            { "Shared_AddAtLeastOneItem", "الرجاء إضافة عنصر واحد على الأقل." },

            // ---- (New) Repeated across Add/Edit forms (Category C) ----
            { "Shared_BtnSave", "حفظ" },
            { "Shared_BtnCancel", "إلغاء" },
            { "Shared_Active", "نشط" },
            { "Shared_ColName", "الاسم:" },
            { "Shared_ColBranch", "الفرع:" },
            { "Shared_ColAddress", "العنوان:" },
            { "Shared_ColPhone", "الهاتف:" },
            { "Shared_AddItem", "إضافة عنصر:" },
            { "Shared_Qty", "الكمية:" },
            { "Shared_RemoveSelectedLine", "حذف السطر المحدد" },


            // ==================================================
            // ===== Common =====================================
            // ==================================================

            { "Common_ConfirmTitle", "تأكيد" },
            { "Common_ConfirmDelete", "حذف '{0}'؟" },
            { "Common_ConfirmAction", "{0} '{1}'؟" },


            // ==================================================
            // ===== Login ======================================
            // ==================================================

            { "Login_Title", "NOVA ERP - تسجيل الدخول" },
            { "Login_BtnLogin", "تسجيل الدخول" },
            { "Login_WelcomeBack", "مرحباً بعودتك" },
            { "Login_PleaseSignIn", "الرجاء تسجيل الدخول للمتابعة" },
            { "Login_Username", "اسم المستخدم" },
            { "Login_Password", "كلمة المرور" },
            { "Login_ColId", "الرقم" },
            { "Login_Branch", "الفرع" },
            { "Login_ColName", "اسم الفرع" },


            // ==================================================
            // ===== Main =======================================
            // ==================================================

            { "Main_Inventory", "المخزون" },
            { "Main_Products", "المنتجات" },
            { "Main_ProductVariants", "متغيرات المنتج" },
            { "Main_Categories", "الفئات" },
            { "Main_Brands", "العلامات التجارية" },
            { "Main_ColorsSizes", "الألوان والمقاسات" },
            { "Main_StockCount", "جرد المخزون" },
            { "Main_StockMovements", "حركات المخزون" },
            { "Main_BranchTransfer", "تحويل بين الفروع" },
            { "Main_Sales", "المبيعات" },
            { "Main_PointOfSale", "نقطة البيع" },
            { "Main_SalesInvoices", "فواتير المبيعات" },
            { "Main_Returns", "المرتجعات" },
            { "Main_Customers", "العملاء" },
            { "Main_Purchasing", "المشتريات" },
            { "Main_PurchaseInvoices", "فواتير المشتريات" },
            { "Main_Suppliers", "الموردون" },
            { "Main_Treasury", "الخزينة" },
            { "Main_TreasuryTransactions", "حركات الخزينة" },
            { "Main_TreasuryBalance", "رصيد الخزينة" },
            { "Main_Reports", "التقارير" },
            { "Main_SalesReport", "تقرير المبيعات" },
            { "Main_StockReport", "تقرير المخزون" },
            { "Main_ProfitReport", "تقرير الأرباح" },
            { "Main_Settings", "الإعدادات" },
            { "Main_Branches", "الفروع" },
            { "Main_UsersRoles", "المستخدمون والصلاحيات" },
            { "Main_PaymentMethods", "طرق الدفع" },
            { "Main_AuditLogs", "سجل العمليات" },
            { "Main_DarkMode", "الوضع الليلي" },


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
            { "Dashboard_Custom", "مخصص" },
            { "Dashboard_Apply", "تطبيق" },
            { "Dashboard_IncomeVsExpenses", "الإيرادات مقابل المصروفات" },
            { "Dashboard_LowStock", "عناصر منخفضة المخزون" },
            { "Dashboard_LatestSalesInvoices", "آخر فواتير المبيعات" },
            { "Dashboard_LatestPurchaseInvoices", "آخر فواتير المشتريات" },
            { "Dashboard_TotalSalesMonth", "إجمالي المبيعات (الشهر)" },
            { "Dashboard_TotalPurchasesMonth", "إجمالي المشتريات (الشهر)" },
            { "Dashboard_TotalIncomeMonth", "إجمالي الإيرادات (الشهر)" },
            { "Dashboard_TotalExpensesMonth", "إجمالي المصروفات (الشهر)" },
            { "Dashboard_CustomersCount", "عدد العملاء" },
            { "Dashboard_SuppliersCount", "عدد الموردين" },
            { "Dashboard_SalesReturnsMonth", "مرتجعات المبيعات (الشهر)" },
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
            { "POS_PickManually", "أو اختر يدوياً:" },
            { "POS_ScanBarcode", "مسح الباركود:" },
            { "POS_BtnRemoveLine", "حذف العنصر المحدد" },
            { "POS_BtnCheckout", "إتمام الشراء" },
            { "POS_Customer", "العميل:" },
            { "POS_PaymentMethod", "طريقة الدفع:" },
            { "POS_Discount", "الخصم:" },
            { "POS_Total", "الإجمالي: 0.00" },


            // ==================================================
            // ===== Returns ====================================
            // ==================================================

            { "Returns_ColInvoice", "الفاتورة" },
            { "Returns_ColReturnDate", "تاريخ الإرجاع" },


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
            { "UsersRoles_Roles", "الأدوار" },
            { "UsersRoles_ColUsername", "اسم المستخدم" },
            { "UsersRoles_ColFullName", "الاسم الكامل" },
            { "UsersRoles_ColRoleName", "اسم الدور" },


            // ==================================================
            // ===== Treasury ===================================
            // ==================================================

            { "Treasury_ColTransactionType", "نوع الحركة" },


            // ==================================================
            // ===== Treasury Balance ===========================
            // ==================================================

            { "TreasuryBalance_CurrentBalance", "الرصيد الحالي: 0.00" },
            { "TreasuryBalance_InOutTotals", "إجمالي الوارد: 0.00   |   إجمالي المنصرف: 0.00" },
            { "TreasuryBalance_CurrentBalanceFmt", "الرصيد الحالي: {0:n2}" },
            { "TreasuryBalance_InOutTotalsFmt", "إجمالي الوارد: {0:n2}   |   إجمالي المنصرف: {1:n2}" },


            // ==================================================
            // ===== Audit Logs =================================
            // ==================================================

            { "AuditLogs_Table", "الجدول:" },


            // ==================================================
            // ===== Reports ====================================
            // ==================================================

            { "Reports_Summary", "الإجمالي: 0.00  |  عدد الفواتير: 0" },
            { "Reports_GenerateReport", "إنشاء تقرير" },


            // ==================================================
            // ===== Other ======================================
            // ==================================================

            { "txtAll", "الكل" },

            // ==================================================
            // ===== (New) Additional Strings — added by audit ==
            // ==================================================

            // ----- Login -----
            { "Login_EnterCredentials", "الرجاء إدخال اسم المستخدم وكلمة المرور" },
            { "Login_SelectBranch", "الرجاء اختيار فرع" },
            { "Login_UsernameNotFound", "اسم المستخدم غير موجود" },
            { "Login_IncorrectPassword", "كلمة المرور غير صحيحة" },
            { "Login_WelcomeUser", "مرحباً، {0}" },
            { "Login_Failed", "فشل تسجيل الدخول" },
            { "Login_WelcomeTitle", "مرحباً" },

            // ----- Payment Methods -----
            { "PaymentMethods_InUse", "طريقة الدفع هذه مستخدمة في فواتير موجودة بالفعل. لا يمكن حذفها." },

            // ----- Users & Roles -----
            { "UsersRoles_UsernameTaken", "اسم المستخدم هذا مستخدم بالفعل." },
            { "UsersRoles_UserHasRecords", "قام هذا المستخدم بإنشاء فواتير أو سجلات أخرى. قم بإلغاء تنشيطه بدلاً من حذفه." },
            { "UsersRoles_RoleAssigned", "هذا الدور مسند إلى مستخدم واحد أو أكثر. أعد إسنادهم أولاً." },
            { "UsersRoles_FillUsernameFullName", "الرجاء إدخال اسم المستخدم والاسم الكامل." },
            { "UsersRoles_PasswordRequiredForNewUser", "الرجاء إدخال كلمة مرور للمستخدم الجديد." },

            // ----- Branches -----
            { "Branches_HasRelatedData", "هذا الفرع يحتوي على بيانات مرتبطة (مستخدمون، فواتير، مخزون...). قم بإزالتها أولاً." },
            { "Branches_NameRequired", "الرجاء إدخال اسم الفرع." },

            // ----- Customers & Suppliers -----
            { "Customers_HasInvoices", "هذا العميل مرتبط بفواتير مبيعات. قم بإلغاء تنشيطه بدلاً من ذلك." },
            { "Suppliers_HasInvoices", "هذا المورد مرتبط بفواتير مشتريات. قم بإلغاء تنشيطه بدلاً من ذلك." },
            { "Party_NameRequired", "الرجاء إدخال الاسم." },

            // ----- Treasury -----
            { "Treasury_ConfirmDeleteEntry", "هل تريد حذف هذا القيد؟" },
            { "Treasury_AmountGreaterThanZero", "الرجاء إدخال مبلغ أكبر من صفر." },

            // ----- Returns -----
            { "Returns_SaveFailed", "تعذر حفظ المرتجع. لم يتم تغيير أي شيء. {0}" },
            { "Returns_Recorded", "تم تسجيل المرتجع. المبلغ: {0:n2}" },
            { "Returns_SelectInvoiceAndItem", "الرجاء اختيار فاتورة وعنصر." },

            // ----- POS -----
            { "POS_EmptyCartMsg", "الرجاء إضافة عنصر واحد على الأقل قبل إتمام الشراء." },
            { "POS_SaleFailed", "تعذر إتمام عملية البيع. لم يتم خصم أي مبلغ. {0}" },
            { "POS_ProductNotFoundByBarcode", "لا يوجد منتج نشط بالباركود {0}" },
            { "POS_NotEnoughStockFor", "الكمية غير كافية لـ {0}. الرجاء التحديث والمحاولة مرة أخرى." },
            { "POS_SaleCompletedMsg", "الفاتورة {0} - الإجمالي: {1:n2}" },
            { "POS_NotFoundTitle", "غير موجود" },
            { "POS_EmptyCartTitle", "السلة فارغة" },
            { "POS_OutOfStockTitle", "نفدت الكمية" },
            { "POS_SaleCompletedTitle", "تمت عملية البيع" },

            // ----- Product Variants -----
            { "ProductVariants_BarcodeUsed", "هذا الباركود مستخدم بالفعل." },
            { "ProductVariants_CombinationExists", "هذا التوليف (نفس المنتج واللون والمقاس) موجود بالفعل." },
            { "ProductVariants_HasStockOrSales", "هذا المتغير مرتبط بمخزون أو مبيعات. قم بإلغاء تنشيطه بدلاً من ذلك." },
            { "ProductVariants_BarcodeRequired", "الرجاء إدخال باركود." },

            // ----- Categories -----
            { "Categories_HasChildren", "هذه الفئة تحتوي على منتجات أو فئات فرعية مرتبطة بها. قم بإزالتها أولاً." },

            // ----- Stock Count -----
            { "StockCount_EntryExists", "هذا المتغير لديه بالفعل قيد جرد لهذا الفرع. قم بتعديله بدلاً من ذلك." },
            { "StockCount_ConfirmDeleteEntry", "هل تريد حذف قيد الجرد هذا؟" },

            // ----- Branch Transfer -----
            { "BranchTransfer_CreatedPending", "تم إنشاء التحويل بحالة معلّق. قم بتعليمه كمكتمل بعد نقل المخزون فعلياً." },
            { "BranchTransfer_CreateFailed", "تعذر إنشاء التحويل. {0}" },
            { "BranchTransfer_Locked", "لا يمكن تعديل هذا التحويل بعد الآن." },
            { "BranchTransfer_NotEnoughStock", "الكمية غير كافية في الفرع المصدر لأحد العناصر. لم يتم إتمام التحويل." },
            { "BranchTransfer_StatusChanged", "تم تعليم التحويل كـ {0}" },
            { "BranchTransfer_UpdateFailed", "تعذر تحديث التحويل. {0}" },
            { "BranchTransfer_FromToMustDiffer", "يجب أن يكون فرع المصدر وفرع الوجهة مختلفين." },

            // ----- Brands -----
            { "Brands_LinkedToProducts", "هذه العلامة التجارية مرتبطة بمنتج واحد أو أكثر. قم بإزالتها أولاً." },

            // ----- Colors & Sizes -----
            { "ColorsSizes_ColorInUse", "هذا اللون مستخدم في متغير منتج واحد أو أكثر. قم بإزالتها أولاً." },
            { "ColorsSizes_SizeInUse", "هذا المقاس مستخدم في متغير منتج واحد أو أكثر. قم بإزالتها أولاً." },
            { "ColorsSizes_ColorNameRequired", "الرجاء إدخال اسم اللون." },
            { "ColorsSizes_SizeNameRequired", "الرجاء إدخال اسم المقاس." },

            // ----- Products -----
            { "Products_HasVariantsOrSales", "هذا المنتج مرتبط بمتغيرات أو مبيعات. قم بإلغاء تنشيطه بدلاً من ذلك." },
            { "Products_CodeNameRequired", "الرجاء إدخال الكود والاسم." },

            // ----- Purchases -----
            { "Purchases_SaveFailed", "تعذر حفظ الفاتورة. لم يتم تغيير أي شيء. {0}" },
            { "Purchases_SavedStatus", "تم حفظ الفاتورة — {0}." },
            { "Purchases_AddAtLeastOneInvoiceItem", "الرجاء إضافة عنصر واحد على الأقل إلى الفاتورة." },

            // ----- Sales Invoices -----
            { "SalesInvoices_DetailsTitle", "تفاصيل الفاتورة" },

            // ----- Stock Report -----
            { "StockReport_TotalInventoryValueFmt", "إجمالي قيمة المخزون: {0:n2}   |   عناصر منخفضة المخزون: {1}" },

            // ==================================================
            // ===== (New) Add/Edit Forms (Category C) ==========
            // ==================================================

            // ----- FrmUserEdit -----
            { "FrmUserEdit_Username", "اسم المستخدم:" },
            { "FrmUserEdit_Password", "كلمة المرور:" },
            { "FrmUserEdit_NewPasswordHint", "كلمة مرور جديدة (اتركها فارغة للاحتفاظ بالحالية):" },
            { "FrmUserEdit_FullName", "الاسم الكامل:" },
            { "FrmUserEdit_Role", "الدور:" },

            // ----- FrmTreasuryEdit -----
            { "FrmTreasuryEdit_Type", "النوع:" },
            { "FrmTreasuryEdit_Amount", "المبلغ:" },
            { "FrmTreasuryEdit_Description", "الوصف:" },

            // ----- FrmReturnEdit -----
            { "FrmReturnEdit_Invoice", "الفاتورة:" },
            { "FrmReturnEdit_ItemToReturn", "العنصر المراد إرجاعه:" },
            { "FrmReturnEdit_QuantityToReturn", "الكمية المراد إرجاعها:" },
            { "FrmReturnEdit_BtnSaveReturn", "حفظ الإرجاع" },

            // ----- FrmVariantEdit -----
            { "FrmVariantEdit_Product", "المنتج:" },
            { "FrmVariantEdit_Color", "اللون:" },
            { "FrmVariantEdit_Size", "المقاس:" },
            { "FrmVariantEdit_Barcode", "الباركود:" },
            { "FrmVariantEdit_SalePrice", "سعر البيع:" },
            { "FrmVariantEdit_CostPrice", "سعر التكلفة:" },

            // ----- FrmStockCountEdit -----
            { "FrmStockCountEdit_ProductVariant", "متغير المنتج:" },
            { "FrmStockCountEdit_Quantity", "الكمية:" },
            { "FrmStockCountEdit_MinQuantityHint", "الحد الأدنى للكمية (تنبيه إعادة الطلب):" },

            // ----- Frmstocktransferedit -----
            { "StockTransferEdit_FromBranch", "من فرع:" },
            { "StockTransferEdit_ToBranch", "إلى فرع:" },
            { "StockTransferEdit_BtnSaveTransfer", "حفظ التحويل" },

            // ----- FrmColorEdit -----
            { "FrmColorEdit_PickColor", "اختر لون:" },

            // ----- FrmSizeEdit -----
            { "FrmSizeEdit_NameHint", "الاسم (مثال: M, L, XL):" },
            { "FrmSizeEdit_SortOrder", "ترتيب العرض:" },

            // ----- FrmProductEdit -----
            { "FrmProductEdit_Code", "الكود:" },
            { "FrmProductEdit_Category", "الفئة:" },
            { "FrmProductEdit_BrandOptional", "العلامة التجارية (اختياري):" },
            { "FrmProductEdit_BasePrice", "السعر الأساسي:" },
            { "FrmProductEdit_NoneOption", "(بدون)" },

            // ----- FrmPurchaseInvoiceEdit -----
            { "FrmPurchaseInvoiceEdit_Supplier", "المورد:" },
            { "FrmPurchaseInvoiceEdit_UnitCost", "سعر الوحدة:" },
            { "FrmPurchaseInvoiceEdit_AmountPaidNow", "المبلغ المدفوع الآن:" },
            { "FrmPurchaseInvoiceEdit_PaidHint", "(اتركه 0 لشراء آجل بالكامل)" },
            { "FrmPurchaseInvoiceEdit_BtnSaveInvoice", "حفظ الفاتورة" },
            { "FrmPurchaseInvoiceEdit_TotalFmt", "الإجمالي: {0:n2}" },

            // ==================================================
            // ===== (New) Edit Form Titles ("New X" / "Editing X") =====
            // ==================================================

            { "Treasury_NewEntryTitle", "قيد خزينة جديد" },
            { "Treasury_EditingEntryTitle", "تعديل القيد" },
            { "Purchases_NewInvoiceTitle", "فاتورة مشتريات جديدة" },
            { "UsersRoles_NewUserTitle", "مستخدم جديد" },
            { "UsersRoles_EditingUserTitleFmt", "تعديل المستخدم: {0}" },
            { "Suppliers_NewTitle", "مورد جديد" },
            { "Customers_NewTitle", "عميل جديد" },
            { "Party_EditingTitleFmt", "تعديل: {0}" },
            { "Returns_NewTitle", "إرجاع جديد" },
            { "StockCount_NewEntryTitle", "قيد جرد جديد" },
            { "StockCount_EditQuantityTitle", "تعديل كمية المخزون" },
            { "ProductVariants_NewTitle", "متغير جديد" },
            { "ProductVariants_EditingTitleFmt", "تعديل: {0}" },
            { "Products_NewTitle", "منتج جديد" },
            { "Products_EditingTitleFmt", "تعديل: {0}" },
            { "ColorsSizes_NewColorTitle", "لون جديد" },
            { "ColorsSizes_EditingColorTitleFmt", "تعديل اللون: {0}" },
            { "ColorsSizes_NewSizeTitle", "مقاس جديد" },
            { "ColorsSizes_EditingSizeTitleFmt", "تعديل المقاس: {0}" },
            { "BranchTransfer_NewTitle", "تحويل مخزون جديد" },
            { "Branches_NewTitle", "فرع جديد" },
            { "Branches_EditingTitleFmt", "تعديل الفرع: {0}" },

            // ==================================================
            // ===== (New) Language Restart Prompt ==============
            // ==================================================

            { "Common_RestartForLanguage", "لازم نعيد تشغيل البرنامج عشان اللغة الجديدة تتطبق. هل عاوز اعاده تشغيل دلوقتي؟" },

            // ==================================================
            // ===== (New) Entity Names (for Shared_X* formatting) =====
            // ==================================================

            { "Products_EntityName", "المنتج" },
            { "ProductVariants_EntityName", "المتغير" },
            { "Categories_EntityName", "الفئة" },
            { "Brands_EntityName", "العلامة التجارية" },
            { "ColorsSizes_ColorEntityName", "اللون" },
            { "ColorsSizes_SizeEntityName", "المقاس" },
            { "Customers_EntityName", "العميل" },
            { "Suppliers_EntityName", "المورد" },
            { "PaymentMethods_EntityName", "طريقة الدفع" },
            { "UsersRoles_UserEntityName", "المستخدم" },
            { "UsersRoles_RoleEntityName", "الدور" },
            { "Branches_EntityName", "الفرع" },
            { "StockCount_EntityName", "قيد الجرد" },
            { "Treasury_EntityName", "قيد الخزينة" },
            { "Shared_Activate", "تفعيل" },
            { "Shared_Deactivate", "إلغاء تنشيط" },
            { "Shared_MenuNew", "جديد" },
            { "Shared_MenuEdit", "تعديل" },
            { "Shared_MenuActivateDeactivate", "تفعيل/إلغاء تنشيط" },
            { "Shared_MenuDelete", "حذف" },

            // ----- Categories (inline name prompt) -----
            { "Categories_NamePrompt", "اسم الفئة:" },
            { "Categories_NewTitle", "فئة جديدة" },
            { "Categories_EditNamePrompt", "أدخل اسم الفئة الجديد:" },
            { "Categories_EditingTitleFmt", "تعديل الفئة: {0}" },

            // ----- Brands (inline name prompt) -----
            { "Brands_NamePrompt", "اسم العلامة التجارية:" },
            { "Brands_NewTitle", "علامة تجارية جديدة" },
            { "Brands_EditNamePrompt", "أدخل اسم العلامة التجارية الجديد:" },
            { "Brands_EditingTitleFmt", "تعديل العلامة التجارية: {0}" },

            // ----- Branch Transfer (context menu) -----
            { "BranchTransfer_MenuNewTransfer", "تحويل جديد" },
            { "BranchTransfer_MenuMarkCompleted", "تعليم كمكتمل (ينقل المخزون)" },
            { "BranchTransfer_MenuCancelTransfer", "إلغاء التحويل" },

            // ----- Sales Invoices -----
            { "SalesInvoices_WalkInCustomer", "عميل نقدي" },
            { "Shared_MenuViewDetails", "عرض التفاصيل" },

            // ----- POS (additional) -----
            { "POS_WalkInCustomer", "عميل نقدي" },
            { "POS_TotalFmt", "الإجمالي: {0:n2}" },

            // ----- Purchases (additional) -----
            { "Purchases_FullyPaid", "مدفوعة بالكامل" },
            { "Purchases_PartiallyPaidFmt", "مدفوعة جزئياً ({0:n2} من {1:n2})" },

            // ----- Treasury (additional) -----
            { "FrmTreasuryEdit_TypeIn", "وارد (نقدية مستلمة)" },
            { "FrmTreasuryEdit_TypeOut", "منصرف (نقدية مدفوعة)" },

            // ----- Roles (inline name prompt) -----
            { "Roles_NamePrompt", "اسم الدور:" },
            { "Roles_NewTitle", "دور جديد" },
            { "Roles_EditNamePrompt", "أدخل اسم الدور الجديد:" },
            { "Roles_EditingTitleFmt", "تعديل الدور: {0}" },

            // ----- Payment Methods (inline name prompt) -----
            { "PaymentMethods_NamePrompt", "اسم طريقة الدفع:" },
            { "PaymentMethods_NewTitle", "طريقة دفع جديدة" },
            { "PaymentMethods_EditNamePrompt", "أدخل الاسم الجديد:" },
            { "PaymentMethods_EditingTitleFmt", "تعديل: {0}" },

            // ----- Profit Report -----
            { "ProfitReport_SummaryFmt", "الإيراد: {0:n2}  |  التكلفة: {1:n2}  |  الربح: {2:n2}  ({3:n1}%)" },
            { "Reports_SummaryFmt", "الإجمالي: {0:n2}  |  الفواتير: {1}" },

            // ----- (New) Auto-generated grid columns (data-bound screens) -----
            { "AuditLogs_ColTable", "الجدول" },
            { "AuditLogs_ColChangedAt", "تاريخ التغيير" },
            { "AuditLogs_ColRecordId", "رقم السجل" },
            { "AuditLogs_ColAction", "العملية" },
            { "AuditLogs_ColUser", "المستخدم" },
            { "AuditLogs_SystemUser", "النظام" },
            { "SalesInvoices_ColInvoiceNumber", "رقم الفاتورة" },
            { "SalesInvoices_ColCustomer", "العميل" },
            { "SalesInvoices_ColNetAmount", "الصافي" },
            { "ColorsSizes_ColHexCode", "كود اللون" },
            { "ColorsSizes_ColSortOrder", "ترتيب العرض" },
            { "TreasuryBalance_ColTotalIn", "إجمالي الوارد" },
            { "TreasuryBalance_ColTotalOut", "إجمالي المنصرف" },
            { "TreasuryBalance_ColBalance", "الرصيد" },
            { "StockReport_ColValue", "القيمة" },
            { "ProfitReport_ColQuantitySold", "الكمية المباعة" },
            { "ProfitReport_ColRevenue", "الإيراد" },
            { "ProfitReport_ColCost", "التكلفة" },
            { "ProfitReport_ColProfit", "الربح" },
            { "StockMovements_ColMovementType", "نوع الحركة" },
            { "StockMovements_ColRefType", "نوع المرجع" },
            { "StockMovements_ColRefId", "رقم المرجع" },
            { "Dashboard_ColDue", "المتبقي" },
            { "Shared_ColTotal", "الإجمالي" },
            { "Purchases_ColUnitCost", "سعر الوحدة" },
            { "POS_ColUnitPrice", "سعر الوحدة" },

            // ----- Profit Report (Net Profit) -----
            { "ProfitReport_NetSummaryFmt", "الإيراد: {0:n2}  |  التكلفة: {1:n2}  |  إجمالي الربح: {2:n2}  ({3:n1}%)\nالمصاريف العامة: {4:n2}  |  صافي الربح: {5:n2}" },
            { "ProfitReport_GeneralExpensesHint", "المصاريف العامة = القيود اليدوية في الخزينة (زي الكهرباء والإيجار) اللي مش مرتبطة بفاتورة بيع أو شراء." },

            // ==================================================
            // ===== Account Statement ==========================
            // ==================================================

            { "Main_AccountStatement", "كشف حساب" },
            { "AccountStatement_TypeCustomer", "عميل" },
            { "AccountStatement_TypeSupplier", "مورد" },
            { "AccountStatement_SelectPartyFirst", "الرجاء اختيار عميل أو مورد أولاً." },
            { "AccountStatement_ColInvoiceDate", "التاريخ" },
            { "AccountStatement_SummaryFmt", "إجمالي الفواتير: {0:n2}  |  إجمالي المدفوع: {1:n2}  |  إجمالي المتبقي: {2:n2}" },

            // ==================================================
            // ===== Day Closing Report (Z-Report) ==============
            // ==================================================

            { "Main_DayClosingReport", "إغلاق اليوم (Z-Report)" },
            { "DayClosing_Date", "التاريخ:" },
            { "DayClosing_ColMethod", "طريقة الدفع" },
            { "DayClosing_ColCount", "العدد" },
            { "DayClosing_SummaryFmt", "عدد الفواتير: {0}   |   إجمالي المبيعات: {1:n2}\nعدد المرتجعات: {2}   |   إجمالي المرتجعات: {3:n2}\nصافي المبيعات: {4:n2}\n\nالوارد - من المبيعات: {5:n2}   |   وارد آخر: {6:n2}   |   إجمالي الوارد: {7:n2}\nالمنصرف - للموردين: {8:n2}   |   مرتجعات: {9:n2}   |   مصاريف عامة: {10:n2}   |   إجمالي المنصرف: {11:n2}\n\nصافي حركة النقدية اليوم: {12:n2}" },

            // ==================================================
            // ===== Backup Settings =============================
            // ==================================================

            { "Main_BackupSettings", "النسخ الاحتياطي" },
            { "Backup_Folder", "مجلد النسخ الاحتياطي:" },
            { "Backup_BtnBrowse", "استعراض..." },
            { "Backup_BtnSave", "حفظ المجلد" },
            { "Backup_BtnBackupNow", "نسخ احتياطي الآن" },
            { "Backup_LastBackupFmt", "آخر نسخة احتياطية: {0:g}" },
            { "Backup_NeverBackedUp", "آخر نسخة احتياطية: لا يوجد" },
            { "Backup_FolderRequired", "الرجاء اختيار مجلد النسخ الاحتياطي أولاً." },
            { "Backup_Success", "تم عمل النسخة الاحتياطية بنجاح." },
            { "Backup_Failed", "فشل عمل النسخة الاحتياطية. تأكد إن SQL Server يقدر يكتب في المجلد المختار." },
            { "Backup_FolderSaved", "تم حفظ مجلد النسخ الاحتياطي." },
            { "Backup_Hint", "بيتعمل نسخ احتياطي تلقائي أول مرة يفتح فيها البرنامج كل يوم، بمجرد ما تحدد المجلد هنا." },

            // ----- Receipt printing -----
            { "Receipt_ThankYou", "شكراً لتسوقكم معنا!" },
            { "Shared_MenuPrintReceipt", "طباعة الإيصال" },

            // ==================================================
            // ===== Licensing / Activation ======================
            // ==================================================

            { "Activation_Title", "NOVA ERP - التفعيل مطلوب" },
            { "Activation_Intro", "النسخة دي لسه مش مفعّلة. ابعت كود الجهاز اللي تحت لمزوّد البرنامج عشان يديك كود التفعيل." },
            { "Activation_MachineId", "كود هذا الجهاز:" },
            { "Activation_BtnCopy", "نسخ" },
            { "Activation_LicenseKey", "كود التفعيل:" },
            { "Activation_BtnActivate", "تفعيل" },
            { "Activation_BtnExit", "خروج" },
            { "Activation_EnterKeyFirst", "الرجاء لصق كود التفعيل اللي استلمته." },
            { "Activation_Success", "تم التفعيل بنجاح. البرنامج هيكمل دلوقتي." },
            { "Activation_IdCopied", "تم نسخ كود الجهاز." },
            { "Activation_ExitConfirm", "البرنامج مش هيشتغل من غير تفعيل. تحب تخرج دلوقتي؟" },

            { "LicenseGen_Title", "مولّد أكواد التفعيل (للمزوّد فقط)" },
            { "LicenseGen_MachineId", "كود جهاز العميل:" },
            { "LicenseGen_SetExpiry", "ينتهي في:" },
            { "LicenseGen_BtnGenerate", "توليد الكود" },
            { "LicenseGen_ResultKey", "كود التفعيل (ابعته للعميل):" },
            { "LicenseGen_BtnCopy", "نسخ" },
            { "LicenseGen_EnterIdFirst", "الرجاء لصق كود جهاز العميل الأول." },
            { "LicenseGen_Copied", "تم نسخ كود التفعيل." }
        };
    }
}