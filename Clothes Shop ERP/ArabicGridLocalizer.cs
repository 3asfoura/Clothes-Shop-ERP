using DevExpress.Utils.Filtering.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;

    // =========================================================
    // Arabic Grid Localizer
    // =========================================================
    public class ArabicGridLocalizer : GridLocalizer
    {
        public override string Language
        {
            get { return "Arabic"; }
        }

        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                // =========================
                // General
                // =========================

                case GridStringId.FileIsNotFoundError:
                    return "الملف مش موجود";

                case GridStringId.ColumnViewExceptionMessage:
                    return "حصل خطأ أثناء عرض البيانات";

                case GridStringId.CustomizationCaption:
                    return "تخصيص الأعمدة";

                case GridStringId.CustomizationColumns:
                    return "الأعمدة";

                case GridStringId.CustomizationBands:
                    return "مجموعات الأعمدة";

                case GridStringId.FilterPanelCustomizeButton:
                    return "تخصيص التصفية";


                // =========================
                // Filter Popup
                // =========================

                case GridStringId.PopupFilterAll:
                    return "الكل";

                case GridStringId.PopupFilterCustom:
                    return "تصفية مخصصة...";

                case GridStringId.PopupFilterBlanks:
                    return "الخانات الفاضية";

                case GridStringId.PopupFilterNonBlanks:
                    return "الخانات اللي فيها بيانات";


                // =========================
                // Custom Filter
                // =========================

                case GridStringId.CustomFilterDialogFormCaption:
                    return "التصفية المخصصة";

                case GridStringId.CustomFilterDialogCaption:
                    return "تصفية حسب العمود ده";

                case GridStringId.CustomFilterDialogRadioAnd:
                    return "و";

                case GridStringId.CustomFilterDialogRadioOr:
                    return "أو";

                case GridStringId.CustomFilterDialogOkButton:
                    return "موافق";

                case GridStringId.CustomFilterDialogClearFilter:
                    return "مسح التصفية";

                case GridStringId.CustomFilterDialog2FieldCheck:
                    return "استخدم حقلين للتصفية";

                case GridStringId.CustomFilterDialogCancelButton:
                    return "إلغاء";

                case GridStringId.CustomFilterDialogEmptyValue:
                    return "(فاضي)";

                case GridStringId.CustomFilterDialogEmptyOperator:
                    return "اختار الشرط";

                case GridStringId.CustomFilterDialogHint:
                    return "اختار الشرط والقيمة اللي عايز تصفي بيها";


                // =========================
                // Messages
                // =========================

                case GridStringId.WindowErrorCaption:
                    return "خطأ";

                case GridStringId.WindowWarningCaption:
                    return "تنبيه";


                // =========================
                // Footer Summary
                // =========================

                case GridStringId.MenuFooterSum:
                    return "المجموع";

                case GridStringId.MenuFooterMin:
                    return "أقل قيمة";

                case GridStringId.MenuFooterMax:
                    return "أعلى قيمة";

                case GridStringId.MenuFooterCount:
                    return "العدد";

                case GridStringId.MenuFooterAverage:
                    return "المتوسط";

                case GridStringId.MenuFooterNone:
                    return "من غير ملخص";

                case GridStringId.MenuFooterSumFormat:
                    return "المجموع: {0}";

                case GridStringId.MenuFooterMinFormat:
                    return "أقل قيمة: {0}";

                case GridStringId.MenuFooterMaxFormat:
                    return "أعلى قيمة: {0}";

                case GridStringId.MenuFooterCountFormat:
                    return "العدد: {0}";

                case GridStringId.MenuFooterAverageFormat:
                    return "المتوسط: {0}";

                case GridStringId.MenuFooterCustomFormat:
                    return "تنسيق مخصص";

                case GridStringId.MenuFooterCountGroupFormat:
                    return "العدد: {0}";


                // =========================
                // Column Menu
                // =========================

                case GridStringId.MenuColumnSortAscending:
                    return "ترتيب من الأصغر للأكبر";

                case GridStringId.MenuColumnSortDescending:
                    return "ترتيب من الأكبر للأصغر";

                case GridStringId.MenuColumnShowColumn:
                    return "إظهار العمود";

                case GridStringId.MenuColumnRemoveColumn:
                    return "إخفاء العمود";

                case GridStringId.MenuColumnGroup:
                    return "تجميع حسب العمود ده";

                case GridStringId.MenuColumnUnGroup:
                    return "إلغاء التجميع";

                case GridStringId.MenuColumnColumnCustomization:
                    return "اختيار الأعمدة";

                case GridStringId.MenuColumnBandCustomization:
                    return "اختيار مجموعات الأعمدة";

                case GridStringId.MenuColumnBestFit:
                    return "أفضل عرض";

                case GridStringId.MenuColumnFilter:
                    return "تصفية";

                case GridStringId.MenuColumnShowAutoFilter:
                    return "إظهار التصفية التلقائية";

                case GridStringId.MenuColumnClearFilter:
                    return "مسح التصفية";

                case GridStringId.MenuColumnBestFitAllColumns:
                    return "أفضل عرض لكل الأعمدة";

                case GridStringId.MenuColumnResetGroupSummarySort:
                    return "إلغاء ترتيب ملخص المجموعة";

                case GridStringId.MenuColumnGroupSummarySortFormat:
                    return "ترتيب المجموعات حسب الملخص";

                case GridStringId.MenuColumnSumSummaryTypeDescription:
                    return "المجموع";

                case GridStringId.MenuColumnMinSummaryTypeDescription:
                    return "أقل قيمة";

                case GridStringId.MenuColumnMaxSummaryTypeDescription:
                    return "أعلى قيمة";

                case GridStringId.MenuColumnCountSummaryTypeDescription:
                    return "العدد";

                case GridStringId.MenuColumnAverageSummaryTypeDescription:
                    return "المتوسط";

                case GridStringId.MenuColumnCustomSummaryTypeDescription:
                    return "ملخص مخصص";

                case GridStringId.MenuColumnSortGroupBySummaryMenu:
                    return "ترتيب المجموعات حسب الملخص";

                case GridStringId.MenuColumnGroupIntervalMenu:
                    return "تجميع حسب";

                case GridStringId.MenuColumnGroupIntervalNone:
                    return "من غير تجميع";

                case GridStringId.MenuColumnGroupIntervalDay:
                    return "اليوم";

                case GridStringId.MenuColumnGroupIntervalMonth:
                    return "الشهر";

                case GridStringId.MenuColumnGroupIntervalYear:
                    return "السنة";

                case GridStringId.MenuColumnGroupIntervalSmart:
                    return "تلقائي";

                case GridStringId.MenuColumnGroupSummaryEditor:
                    return "محرر ملخص المجموعة";

                case GridStringId.MenuColumnExpressionEditor:
                    return "محرر التعبير";

                case GridStringId.MenuColumnConditionalFormatting:
                    return "تنسيق شرطي";

                case GridStringId.MenuColumnFilterMode:
                    return "طريقة التصفية";

                case GridStringId.MenuColumnFilterModeValue:
                    return "حسب القيمة";

                case GridStringId.MenuColumnFilterModeDisplayText:
                    return "حسب النص الظاهر";


                // =========================
                // Group Rows
                // =========================

                case GridStringId.MenuGroupRowExpand:
                    return "فتح المجموعة";

                case GridStringId.MenuGroupRowCollapse:
                    return "قفل المجموعة";


                // =========================
                // Group Panel
                // =========================

                case GridStringId.MenuGroupPanelFullExpand:
                    return "فتح كل المجموعات";

                case GridStringId.MenuGroupPanelFullCollapse:
                    return "قفل كل المجموعات";

                case GridStringId.MenuGroupPanelClearGrouping:
                    return "إلغاء تجميع كل الأعمدة";

                case GridStringId.MenuGroupPanelShow:
                    return "إظهار مربع التجميع";

                case GridStringId.MenuGroupPanelHide:
                    return "إخفاء مربع التجميع";

                case GridStringId.MenuColumnGroupBox:
                    return "مربع التجميع";


                // =========================
                // Grid
                // =========================

                case GridStringId.GridGroupPanelText:
                    return "اسحب عنوان العمود هنا عشان تجمع البيانات";

                case GridStringId.GridNewRowText:
                    return "اضغط هنا عشان تضيف سجل جديد";

                case GridStringId.GridOutlookIntervals:
                    return "النهارده|بكرة|الأسبوع ده|الأسبوع الجاي|الشهر ده|الشهر الجاي|السنة دي|السنة الجاية";


                // =========================
                // Filter Builder
                // =========================

                case GridStringId.FilterBuilderOkButton:
                    return "موافق";

                case GridStringId.FilterBuilderCancelButton:
                    return "إلغاء";

                case GridStringId.FilterBuilderApplyButton:
                    return "تطبيق";

                case GridStringId.FilterBuilderCaption:
                    return "منشئ التصفية";


                // =========================
                // Customization
                // =========================

                case GridStringId.CustomizationFormColumnHint:
                    return "اسحب العمود هنا عشان تضيفه للجدول";

                case GridStringId.CustomizationFormBandHint:
                    return "اسحب مجموعة الأعمدة هنا";


                // =========================
                // Find / Search
                // =========================

                case GridStringId.FindControlFindButton:
                    return "بحث";

                case GridStringId.FindControlClearButton:
                    return "مسح";

                case GridStringId.FindControlNextButton:
                    return "التالي";

                case GridStringId.FindControlPrevButton:
                    return "السابق";

                case GridStringId.FindNullPrompt:
                    return "دور هنا...";


                // =========================
                // Search Lookup
                // =========================

                case GridStringId.SearchLookUpMissingRows:
                    return "في بيانات ناقصة";

                case GridStringId.SearchLookUpAddNewButton:
                    return "إضافة جديد";


                // =========================
                // Footer Menu
                // =========================

                case GridStringId.MenuFooterAddSummaryItem:
                    return "إضافة ملخص";

                case GridStringId.MenuFooterClearSummaryItems:
                    return "مسح كل الملخصات";

                case GridStringId.MenuFooterShow:
                    return "إظهار شريط الملخص";

                case GridStringId.MenuFooterHide:
                    return "إخفاء شريط الملخص";

                case GridStringId.MenuFooterMode:
                    return "طريقة عرض الملخص";

                case GridStringId.MenuFooterAllRows:
                    return "كل الصفوف";

                case GridStringId.MenuFooterSelection:
                    return "الصفوف المحددة";

                case GridStringId.MenuFooterMixed:
                    return "مختلط";


                // =========================
                // Splitter
                // =========================

                case GridStringId.MenuShowSplitItem:
                    return "إظهار الفاصل";

                case GridStringId.MenuHideSplitItem:
                    return "إخفاء الفاصل";


                // =========================
                // Edit Form
                // =========================

                case GridStringId.ServerRequestError:
                    return "حصل خطأ أثناء الاتصال بالسيرفر";

                case GridStringId.EditFormUpdateButton:
                    return "حفظ";

                case GridStringId.EditFormCancelButton:
                    return "إلغاء";

                case GridStringId.EditFormCancelMessage:
                    return "عايز تلغي التعديلات؟";

                case GridStringId.EditFormSaveMessage:
                    return "عايز تحفظ التعديلات؟";


                // =========================
                // Checkbox
                // =========================

                case GridStringId.CheckboxSelectorColumnCaption:
                    return "تحديد";


                // =========================
                // Auto Filter
                // =========================

                case GridStringId.MenuColumnAutoFilterRowHide:
                    return "إخفاء صف التصفية التلقائية";

                case GridStringId.MenuColumnAutoFilterRowShow:
                    return "إظهار صف التصفية التلقائية";

                case GridStringId.MenuColumnFindFilterHide:
                    return "إخفاء لوحة البحث";

                case GridStringId.MenuColumnFindFilterShow:
                    return "إظهار لوحة البحث";

                case GridStringId.MenuColumnFilterEditor:
                    return "محرر التصفية";


                default:
                    return base.GetLocalizedString(id);
            }
        }
    }

    public class ArabicFilterLocalizer : FilterUIElementLocalizer
    {
        public override string GetLocalizedString(FilterUIElementLocalizerStringId id)
        {
            switch (id)
            {
                // =========================
                // Filter Categories
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFiltersNumericName:
                    return "تصفية الأرقام";

                case FilterUIElementLocalizerStringId.CustomUIFiltersNumericDescription:
                    return "تصفية القيم الرقمية";

                case FilterUIElementLocalizerStringId.CustomUIFiltersDateTimeName:
                    return "تصفية التاريخ";

                case FilterUIElementLocalizerStringId.CustomUIFiltersDateTimeDescription:
                    return "تصفية البيانات حسب التاريخ";

                case FilterUIElementLocalizerStringId.CustomUIFiltersDurationName:
                    return "تصفية المدة";

                case FilterUIElementLocalizerStringId.CustomUIFiltersDurationDescription:
                    return "تصفية القيم حسب المدة";

                case FilterUIElementLocalizerStringId.CustomUIFiltersTextName:
                    return "تصفية النصوص";

                case FilterUIElementLocalizerStringId.CustomUIFiltersTextDescription:
                    return "تصفية البيانات حسب النص";

                case FilterUIElementLocalizerStringId.CustomUIFiltersBooleanName:
                    return "تصفية نعم / لا";

                case FilterUIElementLocalizerStringId.CustomUIFiltersBooleanDescription:
                    return "تصفية القيم حسب نعم أو لا";

                case FilterUIElementLocalizerStringId.CustomUIFiltersEnumName:
                    return "التصفية";

                case FilterUIElementLocalizerStringId.CustomUIFiltersEnumDescription:
                    return "تصفية حسب الاختيارات المتاحة";


                // =========================
                // Basic Operators
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterEqualsName:
                    return "يساوي";

                case FilterUIElementLocalizerStringId.CustomUIFilterEqualsDescription:
                    return "بيساوي قيمة معينة";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEqualName:
                    return "مش بيساوي";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEqualDescription:
                    return "مش بيساوي قيمة معينة";

                case FilterUIElementLocalizerStringId.CustomUIFilterBetweenName:
                    return "بين";

                case FilterUIElementLocalizerStringId.CustomUIFilterBetweenDescription:
                    return "القيمة بين قيمتين";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNullName:
                    return "فاضي";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNullDescription:
                    return "الخانة فاضية";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNotNullName:
                    return "مش فاضي";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNotNullDescription:
                    return "الخانة فيها قيمة";


                // =========================
                // Greater / Less
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanName:
                    return "أكبر من";

                case FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanDescription:
                    return "أكبر من قيمة معينة";

                case FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanOrEqualToName:
                    return "أكبر من أو يساوي";

                case FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanOrEqualToDescription:
                    return "أكبر من أو بيساوي قيمة معينة";

                case FilterUIElementLocalizerStringId.CustomUIFilterLessThanName:
                    return "أقل من";

                case FilterUIElementLocalizerStringId.CustomUIFilterLessThanDescription:
                    return "أقل من قيمة معينة";

                case FilterUIElementLocalizerStringId.CustomUIFilterLessThanOrEqualToName:
                    return "أقل من أو يساوي";

                case FilterUIElementLocalizerStringId.CustomUIFilterLessThanOrEqualToDescription:
                    return "أقل من أو بيساوي قيمة معينة";


                // =========================
                // Top / Bottom
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterTopNName:
                    return "أعلى قيم";

                case FilterUIElementLocalizerStringId.CustomUIFilterTopNDescription:
                    return "أعلى القيم";

                case FilterUIElementLocalizerStringId.CustomUIFilterBottomNName:
                    return "أقل قيم";

                case FilterUIElementLocalizerStringId.CustomUIFilterBottomNDescription:
                    return "أقل القيم";


                // =========================
                // Sequence
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierItemsName:
                    return "عدد";

                case FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierItemsDescription:
                    return "حسب عدد العناصر";

                case FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierPercentsName:
                    return "نسبة مئوية";

                case FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierPercentsDescription:
                    return "حسب النسبة المئوية";


                // =========================
                // Average
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterAboveAverageName:
                    return "أعلى من المتوسط";

                case FilterUIElementLocalizerStringId.CustomUIFilterAboveAverageDescription:
                    return "القيم اللي أعلى من المتوسط";

                case FilterUIElementLocalizerStringId.CustomUIFilterBelowAverageName:
                    return "أقل من المتوسط";

                case FilterUIElementLocalizerStringId.CustomUIFilterBelowAverageDescription:
                    return "القيم اللي أقل من المتوسط";


                // =========================
                // Range
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterInRangeName:
                    return "جوا النطاق";

                case FilterUIElementLocalizerStringId.CustomUIFilterInRangeDescription:
                    return "القيم اللي جوا النطاق";


                // =========================
                // Text
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterBeginsWithName:
                    return "بيبدأ بـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterBeginsWithDescription:
                    return "النص بيبدأ بكلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterEndsWithName:
                    return "بينتهي بـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterEndsWithDescription:
                    return "النص بينتهي بكلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginsWithName:
                    return "مش بيبدأ بـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginsWithDescription:
                    return "النص مش بيبدأ بكلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndsWithName:
                    return "مش بينتهي بـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndsWithDescription:
                    return "النص مش بينتهي بكلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterContainsName:
                    return "بيحتوي على";

                case FilterUIElementLocalizerStringId.CustomUIFilterContainsDescription:
                    return "النص بيحتوي على كلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotContainName:
                    return "مش بيحتوي على";

                case FilterUIElementLocalizerStringId.CustomUIFilterDoesNotContainDescription:
                    return "النص مش بيحتوي على كلمة أو حرف معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsBlankName:
                    return "فاضي";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsBlankDescription:
                    return "الخانة فاضية أو مفيهاش بيانات";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNotBlankName:
                    return "مش فاضي";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsNotBlankDescription:
                    return "الخانة فيها بيانات";

                case FilterUIElementLocalizerStringId.CustomUIFilterLikeName:
                    return "مطابق لـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterLikeDescription:
                    return "مطابق لنمط معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterNotLikeName:
                    return "مش مطابق لـ";

                case FilterUIElementLocalizerStringId.CustomUIFilterNotLikeDescription:
                    return "مش مطابق لنمط معين";


                // =========================
                // Dates
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterIsSameDayName:
                    return "نفس اليوم";

                case FilterUIElementLocalizerStringId.CustomUIFilterIsSameDayDescription:
                    return "في نفس التاريخ";

                case FilterUIElementLocalizerStringId.CustomUIFilterBeforeName:
                    return "قبل";

                case FilterUIElementLocalizerStringId.CustomUIFilterBeforeDescription:
                    return "قبل تاريخ معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterAfterName:
                    return "بعد";

                case FilterUIElementLocalizerStringId.CustomUIFilterAfterDescription:
                    return "بعد تاريخ معين";

                case FilterUIElementLocalizerStringId.CustomUIFilterInDateRangeName:
                    return "جوا فترة زمنية";

                case FilterUIElementLocalizerStringId.CustomUIFilterInDateRangeDescription:
                    return "التاريخ موجود جوا فترة معينة";


                // =========================
                // Relative Dates
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterTomorrowName:
                    return "بكرة";

                case FilterUIElementLocalizerStringId.CustomUIFilterTomorrowDescription:
                    return "تاريخ بكرة";

                case FilterUIElementLocalizerStringId.CustomUIFilterTodayName:
                    return "النهارده";

                case FilterUIElementLocalizerStringId.CustomUIFilterTodayDescription:
                    return "تاريخ النهارده";

                case FilterUIElementLocalizerStringId.CustomUIFilterYesterdayName:
                    return "امبارح";

                case FilterUIElementLocalizerStringId.CustomUIFilterYesterdayDescription:
                    return "تاريخ امبارح";


                // =========================
                // Weeks
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterNextWeekName:
                    return "الأسبوع الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterNextWeekDescription:
                    return "الأسبوع الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisWeekName:
                    return "الأسبوع ده";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisWeekDescription:
                    return "الأسبوع الحالي";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastWeekName:
                    return "الأسبوع اللي فات";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastWeekDescription:
                    return "الأسبوع اللي فات";


                // =========================
                // Months
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterNextMonthName:
                    return "الشهر الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterNextMonthDescription:
                    return "الشهر الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisMonthName:
                    return "الشهر ده";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisMonthDescription:
                    return "الشهر الحالي";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastMonthName:
                    return "الشهر اللي فات";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastMonthDescription:
                    return "الشهر اللي فات";


                // =========================
                // Quarters
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterNextQuarterName:
                    return "الربع الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterNextQuarterDescription:
                    return "الربع الجاي";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisQuarterName:
                    return "الربع ده";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisQuarterDescription:
                    return "الربع الحالي";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastQuarterName:
                    return "الربع اللي فات";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastQuarterDescription:
                    return "الربع اللي فات";


                // =========================
                // Years
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterNextYearName:
                    return "السنة الجاية";

                case FilterUIElementLocalizerStringId.CustomUIFilterNextYearDescription:
                    return "السنة الجاية";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisYearName:
                    return "السنة دي";

                case FilterUIElementLocalizerStringId.CustomUIFilterThisYearDescription:
                    return "السنة الحالية";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastYearName:
                    return "السنة اللي فاتت";

                case FilterUIElementLocalizerStringId.CustomUIFilterLastYearDescription:
                    return "السنة اللي فاتت";

                case FilterUIElementLocalizerStringId.CustomUIFilterYearToDateName:
                    return "من أول السنة لحد دلوقتي";

                case FilterUIElementLocalizerStringId.CustomUIFilterYearToDateDescription:
                    return "من بداية السنة لحد النهارده";


                // =========================
                // Date Periods
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterDatePeriodsName:
                    return "فترات زمنية";

                case FilterUIElementLocalizerStringId.CustomUIFilterDatePeriodsDescription:
                    return "فترات تاريخ شائعة";

                case FilterUIElementLocalizerStringId.CustomUIFilterAllDatesInThePeriodName:
                    return "كل التواريخ في الفترة";

                case FilterUIElementLocalizerStringId.CustomUIFilterAllDatesInThePeriodDescription:
                    return "التواريخ اللي جوا الفترة";


                // =========================
                // Quarters
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter1Name:
                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter1Description:
                    return "الربع الأول";

                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter2Name:
                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter2Description:
                    return "الربع التاني";

                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter3Name:
                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter3Description:
                    return "الربع التالت";

                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter4Name:
                case FilterUIElementLocalizerStringId.CustomUIFilterQuarter4Description:
                    return "الربع الرابع";


                // =========================
                // Months
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterJanuaryName:
                case FilterUIElementLocalizerStringId.CustomUIFilterJanuaryDescription:
                    return "يناير";

                case FilterUIElementLocalizerStringId.CustomUIFilterFebruaryName:
                case FilterUIElementLocalizerStringId.CustomUIFilterFebruaryDescription:
                    return "فبراير";

                case FilterUIElementLocalizerStringId.CustomUIFilterMarchName:
                case FilterUIElementLocalizerStringId.CustomUIFilterMarchDescription:
                    return "مارس";

                case FilterUIElementLocalizerStringId.CustomUIFilterAprilName:
                case FilterUIElementLocalizerStringId.CustomUIFilterAprilDescription:
                    return "أبريل";

                case FilterUIElementLocalizerStringId.CustomUIFilterMayName:
                case FilterUIElementLocalizerStringId.CustomUIFilterMayDescription:
                    return "مايو";

                case FilterUIElementLocalizerStringId.CustomUIFilterJuneName:
                case FilterUIElementLocalizerStringId.CustomUIFilterJuneDescription:
                    return "يونيو";

                case FilterUIElementLocalizerStringId.CustomUIFilterJulyName:
                case FilterUIElementLocalizerStringId.CustomUIFilterJulyDescription:
                    return "يوليو";

                case FilterUIElementLocalizerStringId.CustomUIFilterAugustName:
                case FilterUIElementLocalizerStringId.CustomUIFilterAugustDescription:
                    return "أغسطس";

                case FilterUIElementLocalizerStringId.CustomUIFilterSeptemberName:
                case FilterUIElementLocalizerStringId.CustomUIFilterSeptemberDescription:
                    return "سبتمبر";

                case FilterUIElementLocalizerStringId.CustomUIFilterOctoberName:
                case FilterUIElementLocalizerStringId.CustomUIFilterOctoberDescription:
                    return "أكتوبر";

                case FilterUIElementLocalizerStringId.CustomUIFilterNovemberName:
                case FilterUIElementLocalizerStringId.CustomUIFilterNovemberDescription:
                    return "نوفمبر";

                case FilterUIElementLocalizerStringId.CustomUIFilterDecemberName:
                case FilterUIElementLocalizerStringId.CustomUIFilterDecemberDescription:
                    return "ديسمبر";


                // =========================
                // Other
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFilterNoneName:
                    return "اختار واحد";

                case FilterUIElementLocalizerStringId.CustomUIFilterNoneDescription:
                    return "اختار نوع التصفية";

                case FilterUIElementLocalizerStringId.CustomUIFilterCustomName:
                    return "تصفية مخصصة";

                case FilterUIElementLocalizerStringId.CustomUIFilterCustomDescription:
                    return "شرطين أو أكتر باستخدام و / أو";

                case FilterUIElementLocalizerStringId.CustomUIFilterUserName:
                    return "التصفيات المحفوظة";

                case FilterUIElementLocalizerStringId.CustomUIFilterUserDescription:
                    return "التصفيات الجاهزة";


                // =========================
                // Prompts
                // =========================

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptChooseOne:
                    return "اختار واحد...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterADate:
                    return "اكتب التاريخ...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterADuration:
                    return "اكتب المدة...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectAValue:
                    return "اختار قيمة...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterAValue:
                    return "اكتب القيمة...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectADate:
                    return "اختار التاريخ...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectADuration:
                    return "اختار المدة...";

                case FilterUIElementLocalizerStringId.CustomUINullValuePromptSearchControl:
                    return "اكتب كلمة تدور عليها...";


                // =========================
                // Labels
                // =========================

                case FilterUIElementLocalizerStringId.CustomUIFirstLabel:
                    return "الأول";

                case FilterUIElementLocalizerStringId.CustomUISecondLabel:
                    return "التاني";

                case FilterUIElementLocalizerStringId.FilteringUITabValues:
                    return "القيم";

                case FilterUIElementLocalizerStringId.FilteringUITabGroups:
                    return "المجموعات";

                case FilterUIElementLocalizerStringId.FilteringUIClearFilter:
                    return "مسح التصفية";

                case FilterUIElementLocalizerStringId.FilteringUIClose:
                    return "قفل";

                case FilterUIElementLocalizerStringId.FilteringUISearchByYearCaption:
                    return "دور بالسنة";

                case FilterUIElementLocalizerStringId.FilteringUISearchByMonthCaption:
                    return "دور بالشهر";

                case FilterUIElementLocalizerStringId.FilteringUISearchByDayCaption:
                    return "دور باليوم";

                case FilterUIElementLocalizerStringId.FilteringUIMoreButtonCaption:
                    return "عرض أكتر";

                case FilterUIElementLocalizerStringId.FilteringUIFewerButtonCaption:
                    return "عرض أقل";

                case FilterUIElementLocalizerStringId.CustomUIValueLabel:
                    return "القيمة";

                case FilterUIElementLocalizerStringId.CustomUITypeLabel:
                    return "النوع";


                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
    public class ArabicEditorsLocalizer : Localizer
    {
        public override string Language
        {
            get { return "Arabic"; }
        }

        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                // =========================================================
                // General
                // =========================================================

                case StringId.None:
                    return "";

                case StringId.CaptionError:
                    return "خطأ";

                case StringId.InvalidValueText:
                    return "القيمة غير صحيحة";

                case StringId.CheckChecked:
                    return "محدد";

                case StringId.CheckUnchecked:
                    return "غير محدد";

                case StringId.CheckIndeterminate:
                    return "غير محدد";

                case StringId.SearchControlNullValuePrompt:
                    return "بحث...";

                case StringId.SearchControlSearchByMemberAny:
                    return "البحث في كل الحقول";

                case StringId.DateEditToday:
                    return "اليوم";

                case StringId.DateEditClear:
                    return "مسح";

                case StringId.OK:
                    return "موافق";

                case StringId.Cancel:
                    return "إلغاء";


                // =========================================================
                // Data Navigator
                // =========================================================

                case StringId.NavigatorFirstButtonHint:
                    return "السجل الأول";

                case StringId.NavigatorPreviousButtonHint:
                    return "السجل السابق";

                case StringId.NavigatorPreviousPageButtonHint:
                    return "الصفحة السابقة";

                case StringId.NavigatorNextButtonHint:
                    return "السجل التالي";

                case StringId.NavigatorNextPageButtonHint:
                    return "الصفحة التالية";

                case StringId.NavigatorLastButtonHint:
                    return "السجل الأخير";

                case StringId.NavigatorAppendButtonHint:
                    return "إضافة سجل";

                case StringId.NavigatorRemoveButtonHint:
                    return "حذف السجل";

                case StringId.NavigatorEditButtonHint:
                    return "تعديل السجل";

                case StringId.NavigatorEndEditButtonHint:
                    return "حفظ التعديل";

                case StringId.NavigatorCancelEditButtonHint:
                    return "إلغاء التعديل";

                case StringId.NavigatorTextStringFormat:
                    return "السجل {0} من {1}";


                // =========================================================
                // Picture Edit
                // =========================================================

                case StringId.PictureEditMenuCut:
                    return "قص";

                case StringId.PictureEditMenuCopy:
                    return "نسخ";

                case StringId.PictureEditMenuPaste:
                    return "لصق";

                case StringId.PictureEditMenuDelete:
                    return "حذف";

                case StringId.PictureEditMenuLoad:
                    return "تحميل صورة";

                case StringId.PictureEditMenuSave:
                    return "حفظ الصورة";

                case StringId.PictureEditOpenFileFilter:
                    return "ملفات الصور|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff";

                case StringId.PictureEditSaveFileFilter:
                    return "ملفات الصور|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff";

                case StringId.PictureEditOpenFileTitle:
                    return "فتح صورة";

                case StringId.PictureEditSaveFileTitle:
                    return "حفظ الصورة";

                case StringId.PictureEditOpenFileError:
                    return "تعذر فتح الصورة";

                case StringId.PictureEditOpenFileErrorCaption:
                    return "خطأ في فتح الصورة";

                case StringId.PictureEditCopyImageError:
                    return "تعذر نسخ الصورة";

                case StringId.LookUpEditValueIsNull:
                    return "القيمة فارغة";

                case StringId.LookUpInvalidEditValueType:
                    return "نوع القيمة غير صحيح";

                case StringId.LookUpColumnDefaultName:
                    return "القيمة";

                case StringId.MaskBoxValidateError:
                    return "القيمة غير صحيحة";

                case StringId.UnknownPictureFormat:
                    return "تنسيق الصورة غير معروف";

                case StringId.DataEmpty:
                    return "لا توجد بيانات";

                case StringId.NotValidArrayLength:
                    return "حجم البيانات غير صحيح";

                case StringId.ImagePopupEmpty:
                    return "لا توجد صورة";

                case StringId.ImagePopupPicture:
                    return "الصورة";


                // =========================================================
                // Colors
                // =========================================================

                case StringId.ColorTabCustom:
                    return "مخصص";

                case StringId.ColorTabWeb:
                    return "ألوان الويب";

                case StringId.ColorTabSystem:
                    return "ألوان النظام";

                case StringId.ColorTabWebSafeColors:
                    return "ألوان الويب الآمنة";

                case StringId.ColorPickPopupAutomaticItemCaption:
                    return "تلقائي";

                case StringId.ColorPickPopupThemeColorsGroupCaption:
                    return "ألوان النسق";

                case StringId.ColorPickPopupStandardColorsGroupCaption:
                    return "الألوان الأساسية";

                case StringId.ColorPickPopupRecentColorsGroupCaption:
                    return "الألوان الأخيرة";

                case StringId.ColorPickPopupMoreColorsItemCaption:
                    return "ألوان أخرى";

                case StringId.ColorPickHueAxisName:
                    return "درجة اللون";

                case StringId.ColorPickSaturationAxisName:
                    return "التشبع";

                case StringId.ColorPickLuminanceAxisName:
                    return "الإضاءة";

                case StringId.ColorPickBrightnessAxisName:
                    return "السطوع";

                case StringId.ColorPickOpacityAxisName:
                    return "الشفافية";

                case StringId.ColorPickRedValidationMsg:
                    return "قيمة الأحمر غير صحيحة";

                case StringId.ColorPickGreenValidationMsg:
                    return "قيمة الأخضر غير صحيحة";

                case StringId.ColorPickBlueValidationMsg:
                    return "قيمة الأزرق غير صحيحة";

                case StringId.ColorPickOpacityValidationMsg:
                    return "قيمة الشفافية غير صحيحة";

                case StringId.ColorPickColorHexValidationMsg:
                    return "رمز اللون غير صحيح";

                case StringId.ColorPickHueValidationMsg:
                    return "قيمة درجة اللون غير صحيحة";

                case StringId.ColorPickSaturationValidationMsg:
                    return "قيمة التشبع غير صحيحة";

                case StringId.ColorPickBrightValidationMsg:
                    return "قيمة السطوع غير صحيحة";


                // =========================================================
                // Calculator
                // =========================================================

                case StringId.CalcButtonMC:
                    return "مسح الذاكرة";

                case StringId.CalcButtonMR:
                    return "استدعاء الذاكرة";

                case StringId.CalcButtonMS:
                    return "حفظ في الذاكرة";

                case StringId.CalcButtonMx:
                    return "الذاكرة";

                case StringId.CalcButtonSqrt:
                    return "الجذر التربيعي";

                case StringId.CalcButtonBack:
                    return "حذف";

                case StringId.CalcButtonCE:
                    return "مسح الإدخال";

                case StringId.CalcButtonC:
                    return "مسح";

                case StringId.CalcError:
                    return "خطأ في العملية";


                // =========================================================
                // Tab Headers
                // =========================================================

                case StringId.TabHeaderButtonPrev:
                    return "السابق";

                case StringId.TabHeaderButtonNext:
                    return "التالي";

                case StringId.TabHeaderButtonUp:
                    return "لأعلى";

                case StringId.TabHeaderButtonDown:
                    return "لأسفل";

                case StringId.TabHeaderButtonClose:
                    return "إغلاق";

                case StringId.TabHeaderButtonPin:
                    return "تثبيت";

                case StringId.TabHeaderButtonUnpin:
                    return "إلغاء التثبيت";

                case StringId.TabHeaderSelectorButton:
                    return "اختيار الصفحة";


                // =========================================================
                // TextEdit Menu
                // =========================================================

                case StringId.TextEditMenuUndo:
                    return "تراجع";

                case StringId.TextEditMenuCut:
                    return "قص";

                case StringId.TextEditMenuCopy:
                    return "نسخ";

                case StringId.TextEditMenuPaste:
                    return "لصق";

                case StringId.TextEditMenuDelete:
                    return "حذف";

                case StringId.TextEditMenuSelectAll:
                    return "تحديد الكل";


                // =========================================================
                // Filter Editor
                // =========================================================

                case StringId.FilterEditorTabText:
                    return "النص";

                case StringId.FilterEditorTabVisual:
                    return "مرئي";

                case StringId.FilterShowAll:
                    return "إظهار الكل";

                case StringId.FilterGroupAnd:
                    return "و";

                case StringId.FilterGroupNotAnd:
                    return "ليس و";

                case StringId.FilterGroupNotOr:
                    return "ليس أو";

                case StringId.FilterGroupOr:
                    return "أو";

                case StringId.FilterClauseAnyOf:
                    return "أي من";

                case StringId.FilterClauseBeginsWith:
                    return "يبدأ بـ";

                case StringId.FilterClauseBetween:
                    return "بين";

                case StringId.FilterClauseBetweenAnd:
                    return "بين و";

                case StringId.FilterClauseContains:
                    return "يحتوي على";

                case StringId.FilterClauseEndsWith:
                    return "ينتهي بـ";

                case StringId.FilterClauseEquals:
                    return "يساوي";

                case StringId.FilterClauseGreater:
                    return "أكبر من";

                case StringId.FilterClauseGreaterOrEqual:
                    return "أكبر من أو يساوي";

                case StringId.FilterClauseInRange:
                    return "داخل النطاق";

                case StringId.FilterClauseNotInRange:
                    return "خارج النطاق";

                case StringId.FilterClauseInRangeFrom:
                    return "من";

                case StringId.FilterClauseInRangeTo:
                    return "إلى";

                case StringId.FilterClauseIsNotNull:
                    return "ليست فارغة";

                case StringId.FilterClauseIsNull:
                    return "فارغة";

                case StringId.FilterClauseIsNotNullOrEmpty:
                    return "ليست فارغة أو خالية";

                case StringId.FilterClauseIsNullOrEmpty:
                    return "فارغة أو خالية";

                case StringId.FilterClauseLess:
                    return "أقل من";

                case StringId.FilterClauseLessOrEqual:
                    return "أقل من أو يساوي";

                case StringId.FilterClauseLike:
                    return "مطابق";

                case StringId.FilterClauseNoneOf:
                    return "ليس من";

                case StringId.FilterClauseNotBetween:
                    return "ليس بين";

                case StringId.FilterClauseDoesNotContain:
                    return "لا يحتوي على";

                case StringId.FilterClauseDoesNotEqual:
                    return "لا يساوي";

                case StringId.FilterClauseNotLike:
                    return "غير مطابق";

                case StringId.FilterEmptyEnter:
                    return "أدخل قيمة";

                case StringId.FilterEmptyParameter:
                    return "المعامل فارغ";

                case StringId.FilterMenuAddNewParameter:
                    return "إضافة معامل جديد";

                case StringId.FilterEmptyValue:
                    return "(فارغ)";

                case StringId.FilterMenuConditionAdd:
                    return "إضافة شرط";

                case StringId.FilterMenuGroupAdd:
                    return "إضافة مجموعة";

                case StringId.FilterMenuExpressionAdd:
                    return "إضافة تعبير";

                case StringId.FilterMenuClearAll:
                    return "مسح الكل";

                case StringId.FilterMenuRowRemove:
                    return "حذف الصف";


                // =========================================================
                // Filter Tooltips
                // =========================================================

                case StringId.FilterToolTipNodeAdd:
                    return "إضافة";

                case StringId.FilterToolTipNodeRemove:
                    return "حذف";

                case StringId.FilterToolTipNodeAction:
                    return "الإجراء";

                case StringId.FilterToolTipValueType:
                    return "نوع القيمة";

                case StringId.FilterToolTipElementAdd:
                    return "إضافة عنصر";

                case StringId.FilterToolTipKeysAdd:
                    return "إضافة مفاتيح";

                case StringId.FilterToolTipKeysRemove:
                    return "حذف مفاتيح";


                // =========================================================
                // Filter Criteria Operators
                // =========================================================

                case StringId.FilterCriteriaToStringGroupOperatorAnd:
                    return "و";

                case StringId.FilterCriteriaToStringGroupOperatorOr:
                    return "أو";

                case StringId.FilterCriteriaToStringUnaryOperatorBitwiseNot:
                    return "ليس";

                case StringId.FilterCriteriaToStringUnaryOperatorIsNull:
                    return "فارغ";

                case StringId.FilterCriteriaToStringUnaryOperatorMinus:
                    return "سالب";

                case StringId.FilterCriteriaToStringUnaryOperatorNot:
                    return "ليس";

                case StringId.FilterCriteriaToStringUnaryOperatorPlus:
                    return "موجب";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseAnd:
                    return "و";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseOr:
                    return "أو";

                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseXor:
                    return "أو حصري";

                case StringId.FilterCriteriaToStringBinaryOperatorDivide:
                    return "÷";

                case StringId.FilterCriteriaToStringBinaryOperatorEqual:
                    return "=";

                case StringId.FilterCriteriaToStringBinaryOperatorGreater:
                    return ">";

                case StringId.FilterCriteriaToStringBinaryOperatorGreaterOrEqual:
                    return ">=";

                case StringId.FilterCriteriaToStringBinaryOperatorLess:
                    return "<";

                case StringId.FilterCriteriaToStringBinaryOperatorLessOrEqual:
                    return "<=";

                case StringId.FilterCriteriaToStringBinaryOperatorLike:
                    return "مطابق";

                case StringId.FilterCriteriaToStringBinaryOperatorMinus:
                    return "-";

                case StringId.FilterCriteriaToStringBinaryOperatorModulo:
                    return "%";

                case StringId.FilterCriteriaToStringBinaryOperatorMultiply:
                    return "×";

                case StringId.FilterCriteriaToStringBinaryOperatorNotEqual:
                    return "≠";

                case StringId.FilterCriteriaToStringBinaryOperatorPlus:
                    return "+";

                case StringId.FilterCriteriaToStringBetween:
                    return "بين";

                case StringId.FilterCriteriaToStringIn:
                    return "ضمن";

                case StringId.FilterCriteriaToStringIsNotNull:
                    return "ليس فارغًا";

                case StringId.FilterCriteriaToStringNotLike:
                    return "ليس مطابقًا";


                // =========================================================
                // Filter Functions
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionIif:
                    return "إذا";

                case StringId.FilterCriteriaToStringFunctionIsNull:
                    return "إذا كان فارغًا";

                case StringId.FilterCriteriaToStringFunctionLen:
                    return "الطول";

                case StringId.FilterCriteriaToStringFunctionLower:
                    return "أحرف صغيرة";

                case StringId.FilterCriteriaToStringFunctionNone:
                    return "بدون";

                case StringId.FilterCriteriaToStringFunctionSubstring:
                    return "جزء من النص";

                case StringId.FilterCriteriaToStringFunctionTrim:
                    return "إزالة المسافات";

                case StringId.FilterCriteriaToStringFunctionUpper:
                    return "أحرف كبيرة";

                case StringId.FilterCriteriaToStringFunctionIsNullOrEmpty:
                    return "فارغ أو خالي";

                case StringId.FilterCriteriaToStringFunctionConcat:
                    return "دمج";

                case StringId.FilterCriteriaToStringFunctionAscii:
                    return "ASCII";

                case StringId.FilterCriteriaToStringFunctionChar:
                    return "حرف";

                case StringId.FilterCriteriaToStringFunctionToInt:
                    return "عدد صحيح";

                case StringId.FilterCriteriaToStringFunctionToLong:
                    return "عدد صحيح طويل";

                case StringId.FilterCriteriaToStringFunctionToFloat:
                    return "رقم عشري";

                case StringId.FilterCriteriaToStringFunctionToDouble:
                    return "رقم مزدوج";

                case StringId.FilterCriteriaToStringFunctionToDecimal:
                    return "رقم عشري دقيق";

                case StringId.FilterCriteriaToStringFunctionToStr:
                    return "نص";

                case StringId.FilterCriteriaToStringFunctionReplace:
                    return "استبدال";

                case StringId.FilterCriteriaToStringFunctionReverse:
                    return "عكس";

                case StringId.FilterCriteriaToStringFunctionInsert:
                    return "إدراج";

                case StringId.FilterCriteriaToStringFunctionCharIndex:
                    return "موضع الحرف";

                case StringId.FilterCriteriaToStringFunctionRemove:
                    return "إزالة";

                case StringId.FilterCriteriaToStringFunctionAbs:
                    return "القيمة المطلقة";

                case StringId.FilterCriteriaToStringFunctionSqr:
                    return "مربع";

                case StringId.FilterCriteriaToStringFunctionCos:
                    return "جيب التمام";

                case StringId.FilterCriteriaToStringFunctionSin:
                    return "الجيب";

                case StringId.FilterCriteriaToStringFunctionAtn:
                    return "ظل عكسي";

                case StringId.FilterCriteriaToStringFunctionExp:
                    return "أس";

                case StringId.FilterCriteriaToStringFunctionLog:
                    return "لوغاريتم";

                case StringId.FilterCriteriaToStringFunctionRnd:
                    return "رقم عشوائي";

                case StringId.FilterCriteriaToStringFunctionTan:
                    return "الظل";

                case StringId.FilterCriteriaToStringFunctionPower:
                    return "قوة";

                case StringId.FilterCriteriaToStringFunctionSign:
                    return "إشارة";

                case StringId.FilterCriteriaToStringFunctionRound:
                    return "تقريب";

                case StringId.FilterCriteriaToStringFunctionCeiling:
                    return "تقريب لأعلى";

                case StringId.FilterCriteriaToStringFunctionFloor:
                    return "تقريب لأسفل";

                case StringId.FilterCriteriaToStringFunctionMax:
                    return "أقصى قيمة";

                case StringId.FilterCriteriaToStringFunctionMin:
                    return "أقل قيمة";

                case StringId.FilterCriteriaToStringFunctionAcos:
                    return "جيب تمام عكسي";

                case StringId.FilterCriteriaToStringFunctionAsin:
                    return "جيب عكسي";

                case StringId.FilterCriteriaToStringFunctionAtn2:
                    return "ظل عكسي 2";

                case StringId.FilterCriteriaToStringFunctionBigMul:
                    return "ضرب كبير";

                case StringId.FilterCriteriaToStringFunctionCosh:
                    return "جيب تمام زائدي";

                case StringId.FilterCriteriaToStringFunctionLog10:
                    return "لوغاريتم عشري";

                case StringId.FilterCriteriaToStringFunctionSinh:
                    return "جيب زائدي";

                case StringId.FilterCriteriaToStringFunctionTanh:
                    return "ظل زائدي";

                case StringId.FilterCriteriaToStringFunctionPadLeft:
                    return "إضافة مسافات من اليسار";

                case StringId.FilterCriteriaToStringFunctionPadRight:
                    return "إضافة مسافات من اليمين";


                // =========================================================
                // Date Functions
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionDateDiffTick:
                    return "فرق العلامات الزمنية";

                case StringId.FilterCriteriaToStringFunctionDateDiffSecond:
                    return "فرق الثواني";

                case StringId.FilterCriteriaToStringFunctionDateDiffMilliSecond:
                    return "فرق المللي ثانية";

                case StringId.FilterCriteriaToStringFunctionDateDiffMinute:
                    return "فرق الدقائق";

                case StringId.FilterCriteriaToStringFunctionDateDiffHour:
                    return "فرق الساعات";

                case StringId.FilterCriteriaToStringFunctionDateDiffDay:
                    return "فرق الأيام";

                case StringId.FilterCriteriaToStringFunctionDateDiffMonth:
                    return "فرق الشهور";

                case StringId.FilterCriteriaToStringFunctionDateDiffYear:
                    return "فرق السنوات";

                case StringId.FilterCriteriaToStringFunctionGetDate:
                    return "التاريخ";

                case StringId.FilterCriteriaToStringFunctionGetMilliSecond:
                    return "المللي ثانية";

                case StringId.FilterCriteriaToStringFunctionGetSecond:
                    return "الثانية";

                case StringId.FilterCriteriaToStringFunctionGetMinute:
                    return "الدقيقة";

                case StringId.FilterCriteriaToStringFunctionGetHour:
                    return "الساعة";

                case StringId.FilterCriteriaToStringFunctionGetDay:
                    return "اليوم";

                case StringId.FilterCriteriaToStringFunctionGetMonth:
                    return "الشهر";

                case StringId.FilterCriteriaToStringFunctionGetYear:
                    return "السنة";

                case StringId.FilterCriteriaToStringFunctionGetDayOfWeek:
                    return "يوم الأسبوع";

                case StringId.FilterCriteriaToStringFunctionGetDayOfYear:
                    return "يوم السنة";

                case StringId.FilterCriteriaToStringFunctionGetTimeOfDay:
                    return "وقت اليوم";

                case StringId.FilterCriteriaToStringFunctionNow:
                    return "الآن";

                case StringId.FilterCriteriaToStringFunctionUtcNow:
                    return "الوقت العالمي الآن";

                case StringId.FilterCriteriaToStringFunctionToday:
                    return "اليوم";

                case StringId.FilterCriteriaToStringFunctionAddTimeSpan:
                    return "إضافة مدة";

                case StringId.FilterCriteriaToStringFunctionAddTicks:
                    return "إضافة علامات زمنية";

                case StringId.FilterCriteriaToStringFunctionAddMilliSeconds:
                    return "إضافة مللي ثانية";

                case StringId.FilterCriteriaToStringFunctionAddSeconds:
                    return "إضافة ثواني";

                case StringId.FilterCriteriaToStringFunctionAddMinutes:
                    return "إضافة دقائق";

                case StringId.FilterCriteriaToStringFunctionAddHours:
                    return "إضافة ساعات";

                case StringId.FilterCriteriaToStringFunctionAddDays:
                    return "إضافة أيام";

                case StringId.FilterCriteriaToStringFunctionAddMonths:
                    return "إضافة شهور";

                case StringId.FilterCriteriaToStringFunctionAddYears:
                    return "إضافة سنوات";


                // =========================================================
                // Date Relative Functions
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionIsThisYear:
                    return "هذه السنة";

                case StringId.FilterCriteriaToStringFunctionIsThisMonth:
                    return "هذا الشهر";

                case StringId.FilterCriteriaToStringFunctionIsThisWeek:
                    return "هذا الأسبوع";

                case StringId.FilterCriteriaToStringFunctionIsNextMonth:
                    return "الشهر القادم";

                case StringId.FilterCriteriaToStringFunctionIsNextYear:
                    return "السنة القادمة";

                case StringId.FilterCriteriaToStringFunctionIsLastMonth:
                    return "الشهر الماضي";

                case StringId.FilterCriteriaToStringFunctionIsLastYear:
                    return "السنة الماضية";

                case StringId.FilterCriteriaToStringFunctionIsYearToDate:
                    return "من بداية السنة حتى الآن";

                case StringId.FilterCriteriaToStringFunctionIsSameDay:
                    return "نفس اليوم";

                case StringId.FilterCriteriaToStringFunctionInRange:
                    return "داخل النطاق";

                case StringId.FilterCriteriaToStringFunctionInDateRange:
                    return "داخل النطاق الزمني";

                case StringId.FilterCriteriaToStringFunctionNotInRange:
                    return "خارج النطاق";

                case StringId.FilterCriteriaToStringFunctionNotInDateRange:
                    return "خارج النطاق الزمني";


                // =========================================================
                // Local DateTime
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisYear:
                    return "هذه السنة";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisMonth:
                    return "هذا الشهر";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeLastWeek:
                    return "الأسبوع الماضي";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeThisWeek:
                    return "هذا الأسبوع";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeYesterday:
                    return "أمس";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeToday:
                    return "اليوم";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNow:
                    return "الآن";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTomorrow:
                    return "غدًا";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeDayAfterTomorrow:
                    return "بعد غد";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextWeek:
                    return "الأسبوع القادم";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTwoWeeksAway:
                    return "بعد أسبوعين";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextMonth:
                    return "الشهر القادم";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeNextYear:
                    return "السنة القادمة";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTwoMonthsAway:
                    return "بعد شهرين";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeTwoYearsAway:
                    return "بعد سنتين";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeLastMonth:
                    return "الشهر الماضي";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeLastYear:
                    return "السنة الماضية";

                case StringId.FilterCriteriaToStringFunctionLocalDateTimeYearBeforeToday:
                    return "السنة السابقة";


                // =========================================================
                // Outlook Date
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalBeyondThisYear:
                    return "بعد هذه السنة";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisYear:
                    return "لاحقًا هذا العام";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisMonth:
                    return "لاحقًا هذا الشهر";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalNextWeek:
                    return "الأسبوع القادم";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisWeek:
                    return "لاحقًا هذا الأسبوع";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalTomorrow:
                    return "غدًا";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalToday:
                    return "اليوم";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalYesterday:
                    return "أمس";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisWeek:
                    return "في وقت سابق هذا الأسبوع";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLastWeek:
                    return "الأسبوع الماضي";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisMonth:
                    return "في وقت سابق هذا الشهر";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisYear:
                    return "في وقت سابق هذا العام";

                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalPriorThisYear:
                    return "قبل هذه السنة";


                // =========================================================
                // Months
                // =========================================================

                case StringId.FilterCriteriaToStringFunctionIsJanuary:
                    return "يناير";

                case StringId.FilterCriteriaToStringFunctionIsFebruary:
                    return "فبراير";

                case StringId.FilterCriteriaToStringFunctionIsMarch:
                    return "مارس";

                case StringId.FilterCriteriaToStringFunctionIsApril:
                    return "أبريل";

                case StringId.FilterCriteriaToStringFunctionIsMay:
                    return "مايو";

                case StringId.FilterCriteriaToStringFunctionIsJune:
                    return "يونيو";

                case StringId.FilterCriteriaToStringFunctionIsJuly:
                    return "يوليو";

                case StringId.FilterCriteriaToStringFunctionIsAugust:
                    return "أغسطس";

                case StringId.FilterCriteriaToStringFunctionIsSeptember:
                    return "سبتمبر";

                case StringId.FilterCriteriaToStringFunctionIsOctober:
                    return "أكتوبر";

                case StringId.FilterCriteriaToStringFunctionIsNovember:
                    return "نوفمبر";

                case StringId.FilterCriteriaToStringFunctionIsDecember:
                    return "ديسمبر";


                // =========================================================
                // Filter
                // =========================================================

                case StringId.FilterClauseInDateRange:
                    return "داخل الفترة الزمنية";

                case StringId.FilterClauseNotInDateRange:
                    return "خارج الفترة الزمنية";

                case StringId.FilterCriteriaToStringFunctionCustom:
                    return "مخصص";

                case StringId.FilterCriteriaToStringFunctionCustomNonDeterministic:
                    return "مخصص";

                case StringId.FilterCriteriaInvalidExpression:
                    return "التعبير غير صحيح";

                case StringId.FilterCriteriaInvalidExpressionEx:
                    return "التعبير غير صحيح";

                case StringId.Apply:
                    return "تطبيق";

                case StringId.PreviewPanelText:
                    return "معاينة";

                case StringId.TransparentBackColorNotSupported:
                    return "اللون الشفاف غير مدعوم";

                case StringId.FilterOutlookDateText:
                    return "التاريخ";

                case StringId.FilterDateTimeConstantMenuCaption:
                    return "قيم التاريخ والوقت";

                case StringId.FilterDateTimeOperatorMenuCaption:
                    return "عمليات التاريخ والوقت";

                case StringId.FilterAdvancedDateTimeOperatorMenuCaption:
                    return "عمليات التاريخ والوقت المتقدمة";

                case StringId.FilterCustomFunctionsMenuCaption:
                    return "الدوال المخصصة";

                case StringId.FilterDateTextAlt:
                    return "التاريخ";

                case StringId.FilterFunctionsMenuCaption:
                    return "الدوال";


                // =========================================================
                // Boolean
                // =========================================================

                case StringId.DefaultBooleanTrue:
                    return "نعم";

                case StringId.DefaultBooleanFalse:
                    return "لا";

                case StringId.DefaultBooleanDefault:
                    return "افتراضي";


                // =========================================================
                // Progress
                // =========================================================

                case StringId.ProgressExport:
                    return "جاري التصدير...";

                case StringId.ProgressPrinting:
                    return "جاري الطباعة...";

                case StringId.ProgressCreateDocument:
                    return "جاري إنشاء المستند...";

                case StringId.ProgressCancel:
                    return "إلغاء";

                case StringId.ProgressCancelPending:
                    return "جاري الإلغاء...";

                case StringId.ProgressLoadingData:
                    return "جاري تحميل البيانات...";

                case StringId.ProgressPastingData:
                    return "جاري لصق البيانات...";

                case StringId.ProgressCopyingData:
                    return "جاري نسخ البيانات...";


                // =========================================================
                // Aggregate
                // =========================================================

                case StringId.FilterAggregateAvg:
                    return "المتوسط";

                case StringId.FilterAggregateCount:
                    return "العدد";

                case StringId.FilterAggregateExists:
                    return "موجود";

                case StringId.FilterAggregateMax:
                    return "أقصى قيمة";

                case StringId.FilterAggregateMin:
                    return "أقل قيمة";

                case StringId.FilterAggregateSum:
                    return "المجموع";

                case StringId.FieldListName:
                    return "قائمة الحقول";


                // =========================================================
                // Layout
                // =========================================================

                case StringId.RestoreLayoutDialogFileFilter:
                    return "ملفات التخطيط|*.xml";

                case StringId.SaveLayoutDialogFileFilter:
                    return "ملفات التخطيط|*.xml";

                case StringId.RestoreLayoutDialogTitle:
                    return "استعادة التخطيط";

                case StringId.SaveLayoutDialogTitle:
                    return "حفظ التخطيط";


                // =========================================================
                // Picture Zoom
                // =========================================================

                case StringId.PictureEditMenuZoom:
                    return "تكبير";

                case StringId.PictureEditMenuFullSize:
                    return "الحجم الكامل";

                case StringId.PictureEditMenuFitImage:
                    return "ملاءمة الصورة";

                case StringId.PictureEditMenuZoomIn:
                    return "تكبير";

                case StringId.PictureEditMenuZoomOut:
                    return "تصغير";

                case StringId.PictureEditMenuZoomTo:
                    return "تكبير إلى";

                case StringId.PictureEditMenuZoomToolTip:
                    return "تكبير الصورة";


                // =========================================================
                // Filter Popup Toolbar
                // =========================================================

                case StringId.FilterPopupToolbarShowOnlyAvailableItems:
                    return "إظهار العناصر المتاحة فقط";

                case StringId.FilterPopupToolbarShowNewValues:
                    return "إظهار القيم الجديدة";

                case StringId.FilterPopupToolbarIncrementalSearch:
                    return "البحث التدريجي";

                case StringId.FilterPopupToolbarMultiSelection:
                    return "تحديد متعدد";

                case StringId.FilterPopupToolbarRadioMode:
                    return "وضع الاختيار الواحد";

                case StringId.FilterPopupToolbarInvertFilter:
                    return "عكس التصفية";


                // =========================================================
                // TimeSpan
                // =========================================================

                case StringId.Days:
                    return "أيام";

                case StringId.Hours:
                    return "ساعات";

                case StringId.Mins:
                    return "دقائق";

                case StringId.Secs:
                    return "ثواني";

                case StringId.Millisecs:
                    return "مللي ثانية";

                case StringId.TimeSpanDays:
                    return "يوم";

                case StringId.TimeSpanDaysPlural:
                    return "أيام";

                case StringId.TimeSpanDaysShort:
                    return "ي";

                case StringId.TimeSpanHours:
                    return "ساعة";

                case StringId.TimeSpanHoursPlural:
                    return "ساعات";

                case StringId.TimeSpanHoursShort:
                    return "س";

                case StringId.TimeSpanMinutes:
                    return "دقيقة";

                case StringId.TimeSpanMinutesPlural:
                    return "دقائق";

                case StringId.TimeSpanMinutesShort:
                    return "د";

                case StringId.TimeSpanSeconds:
                    return "ثانية";

                case StringId.TimeSpanSecondsPlural:
                    return "ثواني";

                case StringId.TimeSpanSecondsShort:
                    return "ث";

                case StringId.TimeSpanFractionalSeconds:
                    return "جزء من الثانية";

                case StringId.TimeSpanFractionalSecondsPlural:
                    return "أجزاء من الثانية";

                case StringId.TimeSpanFractionalSecondsShort:
                    return "جزء";

                case StringId.TimeSpanMilliseconds:
                    return "مللي ثانية";

                case StringId.TimeSpanMillisecondsPlural:
                    return "مللي ثانية";

                case StringId.TimeSpanMillisecondsShort:
                    return "مللي";


                // =========================================================
                // Preview / Printer
                // =========================================================

                case StringId.PreviewPaused:
                    return "متوقف مؤقتًا";

                case StringId.PreviewError:
                    return "خطأ";

                case StringId.PreviewPendingDeletion:
                    return "في انتظار الحذف";

                case StringId.PreviewPaperJam:
                    return "انحشار الورق";

                case StringId.PreviewPaperOut:
                    return "نفاد الورق";

                case StringId.PreviewManualFeed:
                    return "تغذية يدوية";

                case StringId.PreviewPaperProblem:
                    return "مشكلة في الورق";

                case StringId.PreviewOffline:
                    return "غير متصل";

                case StringId.PreviewIOActive:
                    return "نشاط الإدخال والإخراج";

                case StringId.PreviewBusy:
                    return "مشغول";

                case StringId.PreviewPrinting:
                    return "جاري الطباعة";

                case StringId.PreviewOutputBinFull:
                    return "درج الإخراج ممتلئ";

                case StringId.PreviewNotAvaible:
                    return "غير متاح";

                case StringId.PreviewWaiting:
                    return "في الانتظار";

                case StringId.PreviewProcessing:
                    return "جاري المعالجة";

                case StringId.PreviewInitializing:
                    return "جاري التهيئة";

                case StringId.PreviewWarmingUp:
                    return "جاري التجهيز";

                case StringId.PreviewTonerLow:
                    return "الحبر منخفض";

                case StringId.PreviewNoToner:
                    return "لا يوجد حبر";

                case StringId.PreviewPagePunt:
                    return "مشكلة في الصفحة";

                case StringId.PreviewUserIntervention:
                    return "تدخل المستخدم مطلوب";

                case StringId.PreviewOutOfMemory:
                    return "الذاكرة غير كافية";

                case StringId.PreviewDoorOpen:
                    return "الباب مفتوح";

                case StringId.PreviewServerUnknown:
                    return "الخادم غير معروف";

                case StringId.PreviewPowerSave:
                    return "توفير الطاقة";

                case StringId.PreviewReady:
                    return "جاهز";

                case StringId.PreviewServerOffline:
                    return "الخادم غير متصل";

                case StringId.PreviewDriverUpdateNeeded:
                    return "يجب تحديث برنامج التشغيل";


                // =========================================================
                // Formatting Rules
                // =========================================================

                case StringId.FormatRuleMenuItemDataUpdateRules:
                    return "قواعد تحديث البيانات";

                case StringId.FormatRuleMenuItemClearColumnRules:
                    return "مسح قواعد العمود";

                case StringId.FormatRuleMenuItemClearAllRules:
                    return "مسح كل القواعد";

                case StringId.FormatRuleMenuItemHighlightCellRules:
                    return "قواعد تمييز الخلايا";

                case StringId.FormatRuleMenuItemTopBottomRules:
                    return "قواعد أعلى وأسفل";

                case StringId.FormatRuleMenuItemDataBars:
                    return "أشرطة البيانات";

                case StringId.FormatRuleMenuItemColorScales:
                    return "مقاييس الألوان";

                case StringId.FormatRuleMenuItemIconSets:
                    return "مجموعات الأيقونات";

                case StringId.FormatRuleMenuItemClearRules:
                    return "مسح القواعد";

                case StringId.FormatRuleMenuItemManageRules:
                    return "إدارة القواعد";

                case StringId.FormatRuleMenuItemUniqueDuplicateRules:
                    return "القيم الفريدة والمكررة";

                case StringId.FormatRuleMenuItemGradientFill:
                    return "تعبئة متدرجة";

                case StringId.FormatRuleMenuItemSolidFill:
                    return "تعبئة بلون ثابت";

                case StringId.FormatRuleMenuItemDataBarDescription:
                    return "تنسيق القيم باستخدام أشرطة البيانات";

                case StringId.FormatRuleMenuItemIconSetDescription:
                    return "تنسيق القيم باستخدام الأيقونات";

                case StringId.FormatRuleMenuItemColorScaleDescription:
                    return "تنسيق القيم باستخدام تدرج الألوان";

                case StringId.FormatRuleMenuItemUnique:
                    return "فريد";

                case StringId.FormatRuleUniqueText:
                    return "القيم الفريدة";

                case StringId.FormatRuleMenuItemDuplicate:
                    return "مكرر";

                case StringId.FormatRuleDuplicateText:
                    return "القيم المكررة";

                case StringId.FormatRuleApplyFormatProperty:
                    return "تطبيق التنسيق";

                case StringId.FormatRuleWith:
                    return "باستخدام";

                case StringId.FormatRuleForThisColumnWith:
                    return "لهذا العمود باستخدام";


                // =========================================================
                // Icon Sets
                // =========================================================

                case StringId.IconSetCategoryRatings:
                    return "التقييمات";

                case StringId.IconSetCategoryIndicators:
                    return "المؤشرات";

                case StringId.IconSetCategorySymbols:
                    return "الرموز";

                case StringId.IconSetCategoryShapes:
                    return "الأشكال";

                case StringId.IconSetCategoryDirectional:
                    return "الاتجاهات";

                case StringId.IconSetCategoryPositiveNegative:
                    return "موجب / سالب";

                case StringId.IconSetTitleStars3:
                    return "3 نجوم";

                case StringId.IconSetTitleRatings4:
                    return "4 تقييمات";

                case StringId.IconSetTitleRatings5:
                    return "5 تقييمات";

                case StringId.IconSetTitleQuarters5:
                    return "5 أرباع";

                case StringId.IconSetTitleBoxes5:
                    return "5 مربعات";

                case StringId.IconSetTitleArrows3Colored:
                    return "3 أسهم ملونة";

                case StringId.IconSetTitleArrows3Gray:
                    return "3 أسهم رمادية";

                case StringId.IconSetTitleTriangles3:
                    return "3 مثلثات";

                case StringId.IconSetTitleArrows4Colored:
                    return "4 أسهم ملونة";

                case StringId.IconSetTitleArrows4Gray:
                    return "4 أسهم رمادية";

                case StringId.IconSetTitleArrows5Colored:
                    return "5 أسهم ملونة";

                case StringId.IconSetTitleArrows5Gray:
                    return "5 أسهم رمادية";

                case StringId.IconSetTitleTrafficLights3Unrimmed:
                    return "3 إشارات مرور بدون إطار";

                case StringId.IconSetTitleTrafficLights3Rimmed:
                    return "3 إشارات مرور بإطار";

                case StringId.IconSetTitleSigns3:
                    return "3 علامات";

                case StringId.IconSetTitleTrafficLights4:
                    return "4 إشارات مرور";

                case StringId.IconSetTitleRedToBlack:
                    return "من الأحمر إلى الأسود";

                case StringId.IconSetTitleSymbols3Circled:
                    return "3 رموز داخل دوائر";

                case StringId.IconSetTitleSymbols3Uncircled:
                    return "3 رموز";

                case StringId.IconSetTitleFlags3:
                    return "3 أعلام";

                case StringId.IconSetTitlePositiveNegativeArrows:
                    return "أسهم موجب وسالب";

                case StringId.IconSetTitlePositiveNegativeArrowsGray:
                    return "أسهم موجب وسالب رمادية";

                case StringId.IconSetTitlePositiveNegativeTriangles:
                    return "مثلثات موجب وسالب";


                // =========================================================
                // Top / Bottom Rules
                // =========================================================

                case StringId.FormatRuleMenuItemTop10Items:
                    return "أعلى 10 عناصر";

                case StringId.FormatRuleMenuItemTop10Percent:
                    return "أعلى 10%";

                case StringId.FormatRuleMenuItemBottom10Items:
                    return "أقل 10 عناصر";

                case StringId.FormatRuleMenuItemBottom10Percent:
                    return "أقل 10%";

                case StringId.FormatRuleMenuItemAboveAverage:
                    return "أعلى من المتوسط";

                case StringId.FormatRuleMenuItemBelowAverage:
                    return "أقل من المتوسط";

                case StringId.FormatRuleTopText:
                    return "أعلى";

                case StringId.FormatRuleBottomText:
                    return "أقل";

                case StringId.FormatRuleAboveAverageText:
                    return "أعلى من المتوسط";

                case StringId.FormatRuleBelowAverageText:
                    return "أقل من المتوسط";

                case StringId.FormatRuleMenuItemGreaterThan:
                    return "أكبر من";

                case StringId.FormatRuleMenuItemLessThan:
                    return "أقل من";

                case StringId.FormatRuleMenuItemBetween:
                    return "بين";

                case StringId.FormatRuleMenuItemEqualTo:
                    return "يساوي";

                case StringId.FormatRuleMenuItemTextThatContains:
                    return "نص يحتوي على";

                case StringId.FormatRuleMenuItemCustomCondition:
                    return "شرط مخصص";

                case StringId.FormatRuleGreaterThanText:
                    return "أكبر من";

                case StringId.FormatRuleLessThanText:
                    return "أقل من";

                case StringId.FormatRuleBetweenText:
                    return "بين";

                case StringId.FormatRuleEqualToText:
                    return "يساوي";

                case StringId.FormatRuleTextThatContainsText:
                    return "النص الذي يحتوي على";

                case StringId.FormatRuleCustomConditionText:
                    return "شرط مخصص";

                case StringId.FormatRuleDataUpdateText:
                    return "تحديث البيانات";

                case StringId.FormatRuleExpressionEmptyEnter:
                    return "أدخل التعبير";


                // =========================================================
                // Predefined Appearance
                // =========================================================

                case StringId.FormatPredefinedAppearanceRedFillRedText:
                    return "تعبئة حمراء ونص أحمر";

                case StringId.FormatPredefinedAppearanceYellowFillYellowText:
                    return "تعبئة صفراء ونص أصفر";

                case StringId.FormatPredefinedAppearanceGreenFillGreenText:
                    return "تعبئة خضراء ونص أخضر";

                case StringId.FormatPredefinedAppearanceRedFill:
                    return "تعبئة حمراء";

                case StringId.FormatPredefinedAppearanceRedText:
                    return "نص أحمر";

                case StringId.FormatPredefinedAppearanceGreenFill:
                    return "تعبئة خضراء";

                case StringId.FormatPredefinedAppearanceGreenText:
                    return "نص أخضر";

                case StringId.FormatPredefinedAppearanceBoldText:
                    return "نص عريض";

                case StringId.FormatPredefinedAppearanceItalicText:
                    return "نص مائل";

                case StringId.FormatPredefinedAppearanceStrikeoutText:
                    return "نص يتوسطه خط";

                case StringId.FormatPredefinedAppearanceRedBoldText:
                    return "نص أحمر عريض";

                case StringId.FormatPredefinedAppearanceGreenBoldText:
                    return "نص أخضر عريض";


                // =========================================================
                // Search
                // =========================================================

                case StringId.SearchForColumn:
                    return "البحث في العمود";

                case StringId.SearchForField:
                    return "البحث في الحقل";


                // =========================================================
                // Formatting Rules Manager
                // =========================================================

                case StringId.ManageRuleCaption:
                    return "إدارة قواعد التنسيق";

                case StringId.ManageRuleShowFormattingRules:
                    return "إظهار قواعد التنسيق";

                case StringId.ManageRuleUp:
                    return "لأعلى";

                case StringId.ManageRuleDown:
                    return "لأسفل";

                case StringId.ManageRuleNewRule:
                    return "قاعدة جديدة";

                case StringId.ManageRuleEditRule:
                    return "تعديل القاعدة";

                case StringId.ManageRuleDeleteRule:
                    return "حذف القاعدة";

                case StringId.ManageRuleGridCaptionRule:
                    return "القاعدة";

                case StringId.ManageRuleGridCaptionFormat:
                    return "التنسيق";

                case StringId.ManageRuleGridCaptionApplyToTheRow:
                    return "تطبيق على الصف";

                case StringId.ManageRuleGridCaptionColumn:
                    return "العمود";

                case StringId.ManageRuleGridCaptionStopIfTrue:
                    return "توقف إذا تحقق الشرط";

                case StringId.ManageRuleGridCaptionColumnApplyTo:
                    return "تطبيق على العمود";

                case StringId.ManageRuleAllColumns:
                    return "كل الأعمدة";

                case StringId.NewFormattingRule:
                    return "قاعدة تنسيق جديدة";

                case StringId.EditFormattingRule:
                    return "تعديل قاعدة التنسيق";

                case StringId.NewEditFormattingRuleSelectARuleType:
                    return "اختار نوع القاعدة";

                case StringId.NewEditFormattingRuleEditTheRuleDescription:
                    return "تعديل وصف القاعدة";

                case StringId.NewEditFormattingRuleFormatAllCellsBasedOnTheirValues:
                    return "تنسيق كل الخلايا حسب قيمها";

                case StringId.NewEditFormattingRuleFormatOnlyCellsThatContain:
                    return "تنسيق الخلايا التي تحتوي على";

                case StringId.NewEditFormattingRuleFormatOnlyTopOrBottomRankedValues:
                    return "تنسيق أعلى أو أقل القيم";

                case StringId.NewEditFormattingRuleFormatOnlyValuesThatAreAboveOrBelowAverage:
                    return "تنسيق القيم الأعلى أو الأقل من المتوسط";

                case StringId.NewEditFormattingRuleFormatOnlyUniqueOrDuplicateValues:
                    return "تنسيق القيم الفريدة أو المكررة";

                case StringId.NewEditFormattingRuleFormatOnlyChangingValues:
                    return "تنسيق القيم المتغيرة";

                case StringId.NewEditFormattingRuleUseAFormulaToDetermineWhichCellsToFormat:
                    return "استخدام معادلة لتحديد الخلايا التي سيتم تنسيقها";


                // =========================================================
                // Formatting Rule Common
                // =========================================================

                case StringId.ManageRuleColorScale2:
                    return "مقياس ألوان ثنائي";

                case StringId.ManageRuleColorScale3:
                    return "مقياس ألوان ثلاثي";

                case StringId.ManageRuleDataBar:
                    return "شريط بيانات";

                case StringId.ManageRuleIconSets:
                    return "مجموعات الأيقونات";

                case StringId.ManageRuleCommonMinimum:
                    return "الحد الأدنى";

                case StringId.ManageRuleCommonMaximum:
                    return "الحد الأقصى";

                case StringId.ManageRuleCommonType:
                    return "النوع";

                case StringId.ManageRuleCommonAutomatic:
                    return "تلقائي";

                case StringId.ManageRuleCommonPercent:
                    return "نسبة مئوية";

                case StringId.ManageRuleCommonNumber:
                    return "رقم";

                case StringId.ManageRuleCommonValue:
                    return "القيمة";

                case StringId.ManageRuleCommonColor:
                    return "اللون";

                case StringId.ManageRuleCommonPreview:
                    return "معاينة";

                case StringId.ManageRuleNoFormatSet:
                    return "لم يتم تحديد تنسيق";

                case StringId.ManageRuleColorScaleMidpoint:
                    return "نقطة المنتصف";


                // =========================================================
                // Data Bar
                // =========================================================

                case StringId.ManageRuleDataBarBarAppearance:
                    return "مظهر شريط البيانات";

                case StringId.ManageRuleDataBarFill:
                    return "تعبئة";

                case StringId.ManageRuleDataBarBorder:
                    return "حدود";

                case StringId.ManageRuleDataBarDrawAxis:
                    return "إظهار المحور";

                case StringId.ManageRuleDataBarUseNegativeBar:
                    return "استخدام شريط للقيم السالبة";

                case StringId.ManageRuleDataBarAxisColor:
                    return "لون المحور";

                case StringId.ManageRuleDataBarBarDirection:
                    return "اتجاه الشريط";

                case StringId.ManageRuleDataBarSolidFill:
                    return "تعبئة ثابتة";

                case StringId.ManageRuleDataBarGradientFill:
                    return "تعبئة متدرجة";

                case StringId.ManageRuleDataBarNoBorder:
                    return "بدون حدود";

                case StringId.ManageRuleDataBarSolidBorder:
                    return "حدود ثابتة";

                case StringId.ManageRuleDataBarContext:
                    return "حسب اتجاه النص";

                case StringId.ManageRuleDataBarLTR:
                    return "من اليسار لليمين";

                case StringId.ManageRuleDataBarRTL:
                    return "من اليمين لليسار";


                // =========================================================
                // Icon Set Rules
                // =========================================================

                case StringId.ManageRuleIconSetsDisplayEachIconAccordingToTheseRules:
                    return "عرض كل أيقونة حسب هذه القواعد";

                case StringId.ManageRuleIconSetsReverseIconOrder:
                    return "عكس ترتيب الأيقونات";

                case StringId.ManageRuleIconSetsWhen:
                    return "عندما";

                case StringId.ManageRuleIconSetsValueIs:
                    return "القيمة هي";


                // =========================================================
                // Simple Rules
                // =========================================================

                case StringId.ManageRuleSimpleRuleBaseFormat:
                    return "التنسيق الأساسي";

                case StringId.ManageRuleAverageFormatValuesThatAre:
                    return "تنسيق القيم التي";

                case StringId.ManageRuleAverageTheAverageForTheSelectedRange:
                    return "متوسط النطاق المحدد";

                case StringId.ManageRuleAverageAbove:
                    return "أعلى";

                case StringId.ManageRuleAverageBelow:
                    return "أقل";

                case StringId.ManageRuleAverageEqualOrAbove:
                    return "مساوية أو أعلى";

                case StringId.ManageRuleAverageEqualOrBelow:
                    return "مساوية أو أقل";


                // =========================================================
                // Formula / Ranked
                // =========================================================

                case StringId.ManageRuleFormulaFormatValuesWhereThisFormulaIsTrue:
                    return "تنسيق القيم التي تكون فيها هذه المعادلة صحيحة";

                case StringId.ManageRuleRankedValuesFormatValuesThatRankInThe:
                    return "تنسيق القيم التي ترتيبها ضمن";

                case StringId.ManageRuleRankedValuesOfTheColumnsCellValues:
                    return "من قيم خلايا العمود";

                case StringId.ManageRuleRankedValuesTop:
                    return "الأعلى";

                case StringId.ManageRuleRankedValuesBottom:
                    return "الأدنى";


                // =========================================================
                // Contains
                // =========================================================

                case StringId.ManageRuleThatContainFormatOnlyCellsWith:
                    return "تنسيق الخلايا التي تحتوي على";

                case StringId.ManageRuleThatContainCellValue:
                    return "قيمة الخلية";

                case StringId.ManageRuleThatContainDatesOccurring:
                    return "تاريخ يحدث";

                case StringId.ManageRuleThatContainSpecificText:
                    return "نص محدد";

                case StringId.ManageRuleThatContainBlanks:
                    return "خلايا فارغة";

                case StringId.ManageRuleThatContainNoBlanks:
                    return "خلايا غير فارغة";

                case StringId.ManageRuleThatContainErrors:
                    return "أخطاء";

                case StringId.ManageRuleThatContainNoErrors:
                    return "بدون أخطاء";


                // =========================================================
                // Cell Values
                // =========================================================

                case StringId.ManageRuleCellValueBetween:
                    return "بين";

                case StringId.ManageRuleCellValueNotBetween:
                    return "ليس بين";

                case StringId.ManageRuleCellValueEqualTo:
                    return "يساوي";

                case StringId.ManageRuleCellValueNotEqualTo:
                    return "لا يساوي";

                case StringId.ManageRuleCellValueGreaterThan:
                    return "أكبر من";

                case StringId.ManageRuleCellValueLessThan:
                    return "أقل من";

                case StringId.ManageRuleCellValueGreaterThanOrEqualTo:
                    return "أكبر من أو يساوي";

                case StringId.ManageRuleCellValueLessThanOrEqualTo:
                    return "أقل من أو يساوي";


                // =========================================================
                // Dates Occurring
                // =========================================================

                case StringId.ManageRuleDatesOccurringBeyond:
                    return "بعد";

                case StringId.ManageRuleDatesOccurringBeyondThisYear:
                    return "بعد هذه السنة";

                case StringId.ManageRuleDatesOccurringEarlier:
                    return "قبل";

                case StringId.ManageRuleDatesOccurringEarlierThisMonth:
                    return "قبل هذا الشهر";

                case StringId.ManageRuleDatesOccurringEarlierThisWeek:
                    return "قبل هذا الأسبوع";

                case StringId.ManageRuleDatesOccurringEarlierThisYear:
                    return "قبل هذه السنة";

                case StringId.ManageRuleDatesOccurringLastWeek:
                    return "الأسبوع الماضي";

                case StringId.ManageRuleDatesOccurringLaterThisMonth:
                    return "لاحقًا هذا الشهر";

                case StringId.ManageRuleDatesOccurringLaterThisWeek:
                    return "لاحقًا هذا الأسبوع";

                case StringId.ManageRuleDatesOccurringLaterThisYear:
                    return "لاحقًا هذا العام";

                case StringId.ManageRuleDatesOccurringMonthAfter1:
                    return "بعد شهر";

                case StringId.ManageRuleDatesOccurringMonthAfter2:
                    return "بعد شهرين";

                case StringId.ManageRuleDatesOccurringMonthAgo1:
                    return "منذ شهر";

                case StringId.ManageRuleDatesOccurringMonthAgo2:
                    return "منذ شهرين";

                case StringId.ManageRuleDatesOccurringMonthAgo3:
                    return "منذ 3 شهور";

                case StringId.ManageRuleDatesOccurringMonthAgo4:
                    return "منذ 4 شهور";

                case StringId.ManageRuleDatesOccurringMonthAgo5:
                    return "منذ 5 شهور";

                case StringId.ManageRuleDatesOccurringMonthAgo6:
                    return "منذ 6 شهور";

                case StringId.ManageRuleDatesOccurringNextWeek:
                    return "الأسبوع القادم";

                case StringId.ManageRuleDatesOccurringPriorThisYear:
                    return "قبل هذه السنة";

                case StringId.ManageRuleDatesOccurringThisMonth:
                    return "هذا الشهر";

                case StringId.ManageRuleDatesOccurringThisWeek:
                    return "هذا الأسبوع";

                case StringId.ManageRuleDatesOccurringTomorrow:
                    return "غدًا";

                case StringId.ManageRuleDatesOccurringToday:
                    return "اليوم";

                case StringId.ManageRuleDatesOccurringYesterday:
                    return "أمس";


                // =========================================================
                // Specific Text
                // =========================================================

                case StringId.ManageRuleSpecificTextContaining:
                    return "يحتوي على";

                case StringId.ManageRuleSpecificTextNotContaining:
                    return "لا يحتوي على";

                case StringId.ManageRuleSpecificTextBeginningWith:
                    return "يبدأ بـ";

                case StringId.ManageRuleSpecificTextEndingWith:
                    return "ينتهي بـ";


                // =========================================================
                // Unique / Duplicate
                // =========================================================

                case StringId.ManageRuleUniqueOrDuplicateFormatAll:
                    return "تنسيق الكل";

                case StringId.ManageRuleUniqueOrDuplicateValuesInTheSelectedRange:
                    return "القيم في النطاق المحدد";

                case StringId.ManageRuleUniqueOrDuplicateUnique:
                    return "فريدة";

                case StringId.ManageRuleUniqueOrDuplicateDuplicate:
                    return "مكررة";


                // =========================================================
                // Rule Types
                // =========================================================

                case StringId.ManageRuleDataUpdate:
                    return "تحديث البيانات";

                case StringId.ManageRuleColorScale:
                    return "مقياس الألوان";

                case StringId.ManageRuleIconSet:
                    return "مجموعة الأيقونات";

                case StringId.ManageRuleFormula:
                    return "معادلة";

                case StringId.ManageRuleAboveAverage:
                    return "أعلى من المتوسط";

                case StringId.ManageRuleBelowAverage:
                    return "أقل من المتوسط";

                case StringId.ManageRuleEqualOrAboveAverage:
                    return "مساوي أو أعلى من المتوسط";

                case StringId.ManageRuleEqualOrBelowAverage:
                    return "مساوي أو أقل من المتوسط";


                // =========================================================
                // Format Cells
                // =========================================================

                case StringId.ManageRuleFormatCellsCaption:
                    return "تنسيق الخلايا";

                case StringId.ManageRuleFormatCellsFont:
                    return "الخط";

                case StringId.ManageRuleFormatCellsFill:
                    return "التعبئة";

                case StringId.ManageRuleFormatCellsPredefinedAppearance:
                    return "مظهر جاهز";

                case StringId.ManageRuleFormatCellsFontStyle:
                    return "نمط الخط";

                case StringId.ManageRuleFormatCellsFontColor:
                    return "لون الخط";

                case StringId.ManageRuleFormatCellsEffects:
                    return "التأثيرات";

                case StringId.ManageRuleFormatCellsUnderline:
                    return "تسطير";

                case StringId.ManageRuleFormatCellsStrikethrough:
                    return "يتوسطه خط";

                case StringId.ManageRuleFormatCellsClear:
                    return "مسح";

                case StringId.ManageRuleFormatCellsBackgroundColor:
                    return "لون الخلفية";

                case StringId.ManageRuleFormatCellsNone:
                    return "بدون";

                case StringId.ManageRuleFormatCellsRegular:
                    return "عادي";

                case StringId.ManageRuleFormatCellsBold:
                    return "عريض";

                case StringId.ManageRuleFormatCellsItalic:
                    return "مائل";

                case StringId.ManageRuleValuesFor:
                    return "القيم لـ";

                case StringId.ManageRuleMillisecond:
                    return "مللي ثانية";


                // =========================================================
                // Camera
                // =========================================================

                case StringId.TakePictureDialogTitle:
                    return "التقاط صورة";

                case StringId.TakePictureMenuItem:
                    return "التقاط صورة";

                case StringId.TakePictureDialogCapture:
                    return "التقاط";

                case StringId.TakePictureDialogTryAgain:
                    return "المحاولة مرة أخرى";

                case StringId.TakePictureDialogSave:
                    return "حفظ";

                case StringId.CameraSettingsActiveDevice:
                    return "الجهاز النشط";

                case StringId.CameraSettingsBrightness:
                    return "السطوع";

                case StringId.CameraSettingsContrast:
                    return "التباين";

                case StringId.CameraSettingsDesaturate:
                    return "تقليل التشبع";

                case StringId.CameraSettingsDefaults:
                    return "الإعدادات الافتراضية";

                case StringId.CameraSettingsCaption:
                    return "إعدادات الكاميرا";

                case StringId.CameraSettingsResolution:
                    return "الدقة";

                case StringId.CameraDeviceNotFound:
                    return "لم يتم العثور على الكاميرا";

                case StringId.CameraDeviceIsBusy:
                    return "الكاميرا مشغولة";

                case StringId.CameraDesignTimeInfo:
                    return "معاينة الكاميرا";


                // =========================================================
                // Misc
                // =========================================================

                case StringId.OfficeNavigationOptions:
                    return "خيارات التنقل";

                case StringId.NoneItemText:
                    return "بدون";

                case StringId.ProgressPanelDefaultCaption:
                    return "جاري التنفيذ...";

                case StringId.ProgressPanelDefaultDescription:
                    return "يرجى الانتظار...";

                case StringId.FormatRuleNoCellIcon:
                    return "بدون أيقونة";

                case StringId.PictureEditMenuEdit:
                    return "تحرير";

                case StringId.ImageEditorDialogCaption:
                    return "محرر الصور";

                case StringId.DataUpdateTriggerChanged:
                    return "عند التغيير";

                case StringId.DataUpdateTriggerIncreased:
                    return "عند الزيادة";

                case StringId.DataUpdateTriggerDecreased:
                    return "عند الانخفاض";

                case StringId.FilterNewEmptyEnter:
                    return "أدخل قيمة";

                case StringId.FilterNewEmptyParameter:
                    return "المعامل فارغ";

                case StringId.FilterEmptyField:
                    return "الحقل فارغ";

                case StringId.FilterExpressionEmptyText:
                    return "التعبير فارغ";

                case StringId.ChartRangeControlClientRangeValidationMsg:
                    return "النطاق غير صحيح";

                case StringId.AllRightsReserved:
                    return "جميع الحقوق محفوظة";

                case StringId.Version:
                    return "الإصدار";


                // =========================================================
                // Collection Editor
                // =========================================================

                case StringId.DXCollectionEditorOKButtonText:
                    return "موافق";

                case StringId.DXCollectionEditorCancelButtonText:
                    return "إلغاء";

                case StringId.DXCollectionEditorAddItemButtonText:
                    return "إضافة";

                case StringId.DXCollectionEditorRemoveItemButtonText:
                    return "حذف";

                case StringId.DXCollectionEditorItemsListGroupCaptionStringFormat:
                    return "العناصر ({0})";

                case StringId.DXCollectionEditorPreviewGroupCaption:
                    return "معاينة";

                case StringId.DXCollectionEditorItemPropertiesGroupCaption:
                    return "خصائص العنصر";

                case StringId.DXCollectionEditorSomeItemsTypeAddItemButtonStringFormat:
                    return "إضافة {0}";


                // =========================================================
                // Syntax Edit Find / Replace
                // =========================================================

                case StringId.SyntaxEditFindPanelFindCaption:
                    return "بحث";

                case StringId.SyntaxEditFindPanelReplaceCaption:
                    return "استبدال";

                case StringId.SyntaxEditClearButtonCaption:
                    return "مسح";

                case StringId.SyntaxEditShowDropdownButtonCaption:
                    return "إظهار الخيارات";

                case StringId.SyntaxEditReplaceButtonTooltip:
                    return "استبدال";

                case StringId.SyntaxEditReplaceAllButtonTooltip:
                    return "استبدال الكل";

                case StringId.SyntaxEditFindPanelFindNextButtonTooltip:
                    return "بحث عن التالي";

                case StringId.SyntaxEditFindPanelFindPreviousButtonTooltip:
                    return "بحث عن السابق";

                case StringId.SyntaxEditFindPanelCloseButtonTooltip:
                    return "إغلاق";

                case StringId.SyntaxEditFindPanelExpandButtonTooltip:
                    return "توسيع";


                // =========================================================
                // Default
                // =========================================================

                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
