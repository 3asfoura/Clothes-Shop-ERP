using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes_Shop_ERP
{
    internal class FileName
    {
        public enum StringId
        {
            
            None,
            
            CaptionError,
            
            InvalidValueText,
            
            CheckChecked,
            
            CheckUnchecked,
            
            CheckIndeterminate,
            
            SearchControlNullValuePrompt,
            
            SearchControlSearchByMemberAny,
            
            DateEditToday,
            
            DateEditClear,
            
            OK,
            
            Cancel,
            
            NavigatorFirstButtonHint,
            
            NavigatorPreviousButtonHint,
            
            NavigatorPreviousPageButtonHint,
            
            NavigatorNextButtonHint,
            
            NavigatorNextPageButtonHint,
            
            NavigatorLastButtonHint,
            
            NavigatorAppendButtonHint,
            
            NavigatorRemoveButtonHint,
            
            NavigatorEditButtonHint,
            
            NavigatorEndEditButtonHint,
            
            NavigatorCancelEditButtonHint,
            
            NavigatorTextStringFormat,
            
            PictureEditMenuCut,
            
            PictureEditMenuCopy,
            
            PictureEditMenuPaste,
            
            PictureEditMenuDelete,
            
            PictureEditMenuLoad,
            
            PictureEditMenuSave,
            
            PictureEditOpenFileFilter,
            
            PictureEditSaveFileFilter,
            
            PictureEditOpenFileTitle,
            
            PictureEditSaveFileTitle,
            
            PictureEditOpenFileError,
            
            PictureEditOpenFileErrorCaption,
            
            PictureEditCopyImageError,
            
            LookUpEditValueIsNull,
            
            LookUpInvalidEditValueType,
            
            LookUpColumnDefaultName,
            
            MaskBoxValidateError,
            
            UnknownPictureFormat,
            
            DataEmpty,
            
            NotValidArrayLength,
            
            ImagePopupEmpty,
            
            ImagePopupPicture,
            
            ColorTabCustom,
            
            ColorTabWeb,
            
            ColorTabSystem,
            
            CalcButtonMC,
            
            CalcButtonMR,
            
            CalcButtonMS,
            
            CalcButtonMx,
            
            CalcButtonSqrt,
            
            CalcButtonBack,
            
            CalcButtonCE,
            
            CalcButtonC,
            
            CalcError,
            
            TabHeaderButtonPrev,
            
            TabHeaderButtonNext,
            
            TabHeaderButtonUp,
            
            TabHeaderButtonDown,
            
            TabHeaderButtonClose,
            
            TabHeaderButtonPin,
            
            TabHeaderButtonUnpin,
            
            TabHeaderSelectorButton,
            
            XtraMessageBoxOkButtonText,
            
            XtraMessageBoxCancelButtonText,
            
            XtraMessageBoxYesButtonText,
            
            XtraMessageBoxNoButtonText,
            
            XtraMessageBoxAbortButtonText,
            
            XtraMessageBoxRetryButtonText,
            
            XtraMessageBoxIgnoreButtonText,
            
            XtraMessageBoxDoNotShowThisMessageAgain,
            
            TextEditMenuUndo,
            
            TextEditMenuCut,
            
            TextEditMenuCopy,
            
            TextEditMenuPaste,
            
            TextEditMenuDelete,
            
            TextEditMenuSelectAll,
            
            FilterEditorTabText,
            
            FilterEditorTabVisual,
            
            FilterShowAll,
            
            FilterGroupAnd,
            
            FilterGroupNotAnd,
            
            FilterGroupNotOr,
            
            FilterGroupOr,
            
            FilterClauseAnyOf,
            
            FilterClauseBeginsWith,
            
            FilterClauseBetween,
            
            FilterClauseBetweenAnd,
            
            FilterClauseContains,
            
            FilterClauseEndsWith,
            
            FilterClauseEquals,
            
            FilterClauseGreater,
            
            FilterClauseGreaterOrEqual,
            
            FilterClauseInRange,
            
            FilterClauseNotInRange,
            
            FilterClauseInRangeFrom,
            
            FilterClauseInRangeTo,
            
            FilterClauseIsNotNull,
            
            FilterClauseIsNull,
            
            FilterClauseIsNotNullOrEmpty,
            
            FilterClauseIsNullOrEmpty,
            
            FilterClauseLess,
            
            FilterClauseLessOrEqual,
            
            FilterClauseLike,
            
            FilterClauseNoneOf,
            
            FilterClauseNotBetween,
            
            FilterClauseDoesNotContain,
            
            FilterClauseDoesNotEqual,
            
            FilterClauseNotLike,
            
            FilterEmptyEnter,
            
            FilterEmptyParameter,
            
            FilterMenuAddNewParameter,
            
            FilterEmptyValue,
            
            FilterMenuConditionAdd,
            
            FilterMenuGroupAdd,
            
            FilterMenuExpressionAdd,
            
            FilterMenuClearAll,
            
            FilterMenuRowRemove,
            
            FilterToolTipNodeAdd,
            
            FilterToolTipNodeRemove,
            
            FilterToolTipNodeAction,
            
            FilterToolTipValueType,
            
            FilterToolTipElementAdd,
            
            FilterToolTipKeysAdd,
            
            FilterToolTipKeysRemove,
            
            ContainerAccessibleEditName,
            
            FilterCriteriaToStringGroupOperatorAnd,
            
            FilterCriteriaToStringGroupOperatorOr,
            
            FilterCriteriaToStringUnaryOperatorBitwiseNot,
            
            FilterCriteriaToStringUnaryOperatorIsNull,
            
            FilterCriteriaToStringUnaryOperatorMinus,
            
            FilterCriteriaToStringUnaryOperatorNot,
            
            FilterCriteriaToStringUnaryOperatorPlus,
            
            FilterCriteriaToStringBinaryOperatorBitwiseAnd,
            
            FilterCriteriaToStringBinaryOperatorBitwiseOr,
            
            FilterCriteriaToStringBinaryOperatorBitwiseXor,
            
            FilterCriteriaToStringBinaryOperatorDivide,
            
            FilterCriteriaToStringBinaryOperatorEqual,
            
            FilterCriteriaToStringBinaryOperatorGreater,
            
            FilterCriteriaToStringBinaryOperatorGreaterOrEqual,
            
            FilterCriteriaToStringBinaryOperatorLess,
            
            FilterCriteriaToStringBinaryOperatorLessOrEqual,
            
            FilterCriteriaToStringBinaryOperatorLike,
            
            FilterCriteriaToStringBinaryOperatorMinus,
            
            FilterCriteriaToStringBinaryOperatorModulo,
            
            FilterCriteriaToStringBinaryOperatorMultiply,
            
            FilterCriteriaToStringBinaryOperatorNotEqual,
            
            FilterCriteriaToStringBinaryOperatorPlus,
            
            FilterCriteriaToStringBetween,
            
            FilterCriteriaToStringIn,
            
            FilterCriteriaToStringIsNotNull,
            
            FilterCriteriaToStringNotLike,
            
            FilterCriteriaToStringFunctionIif,
            
            FilterCriteriaToStringFunctionIsNull,
            
            FilterCriteriaToStringFunctionLen,
            
            FilterCriteriaToStringFunctionLower,
            
            FilterCriteriaToStringFunctionNone,
            
            FilterCriteriaToStringFunctionSubstring,
            
            FilterCriteriaToStringFunctionTrim,
            
            FilterCriteriaToStringFunctionUpper,
            
            FilterCriteriaToStringFunctionIsThisYear,
            
            FilterCriteriaToStringFunctionIsThisMonth,
            
            FilterCriteriaToStringFunctionIsThisWeek,
            
            FilterCriteriaToStringFunctionLocalDateTimeThisYear,
            
            FilterCriteriaToStringFunctionLocalDateTimeThisMonth,
            
            FilterCriteriaToStringFunctionLocalDateTimeLastWeek,
            
            FilterCriteriaToStringFunctionLocalDateTimeThisWeek,
            
            FilterCriteriaToStringFunctionLocalDateTimeYesterday,
            
            FilterCriteriaToStringFunctionLocalDateTimeToday,
            
            FilterCriteriaToStringFunctionLocalDateTimeNow,
            
            FilterCriteriaToStringFunctionLocalDateTimeTomorrow,
            
            FilterCriteriaToStringFunctionLocalDateTimeDayAfterTomorrow,
            
            FilterCriteriaToStringFunctionLocalDateTimeNextWeek,
            
            FilterCriteriaToStringFunctionLocalDateTimeTwoWeeksAway,
            
            FilterCriteriaToStringFunctionLocalDateTimeNextMonth,
            
            FilterCriteriaToStringFunctionLocalDateTimeNextYear,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalBeyondThisYear,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisYear,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisMonth,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalNextWeek,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisWeek,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalTomorrow,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalToday,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalYesterday,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisWeek,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalLastWeek,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisMonth,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisYear,
            
            FilterCriteriaToStringFunctionIsOutlookIntervalPriorThisYear,
            
            FilterClauseInDateRange,
            
            FilterClauseNotInDateRange,
            
            FilterCriteriaToStringFunctionCustom,
            
            FilterCriteriaToStringFunctionCustomNonDeterministic,
            
            FilterCriteriaToStringFunctionIsNullOrEmpty,
            
            FilterCriteriaToStringFunctionConcat,
            
            FilterCriteriaToStringFunctionAscii,
            
            FilterCriteriaToStringFunctionChar,
            
            FilterCriteriaToStringFunctionToInt,
            
            FilterCriteriaToStringFunctionToLong,
            
            FilterCriteriaToStringFunctionToFloat,
            
            FilterCriteriaToStringFunctionToDouble,
            
            FilterCriteriaToStringFunctionToDecimal,
            
            FilterCriteriaToStringFunctionToStr,
            
            FilterCriteriaToStringFunctionReplace,
            
            FilterCriteriaToStringFunctionReverse,
            
            FilterCriteriaToStringFunctionInsert,
            
            FilterCriteriaToStringFunctionCharIndex,
            
            FilterCriteriaToStringFunctionRemove,
            
            FilterCriteriaToStringFunctionAbs,
            
            FilterCriteriaToStringFunctionSqr,
            
            FilterCriteriaToStringFunctionCos,
            
            FilterCriteriaToStringFunctionSin,
            
            FilterCriteriaToStringFunctionAtn,
            
            FilterCriteriaToStringFunctionExp,
            
            FilterCriteriaToStringFunctionLog,
            
            FilterCriteriaToStringFunctionRnd,
            
            FilterCriteriaToStringFunctionTan,
            
            FilterCriteriaToStringFunctionPower,
            
            FilterCriteriaToStringFunctionSign,
            
            FilterCriteriaToStringFunctionRound,
            
            FilterCriteriaToStringFunctionCeiling,
            
            FilterCriteriaToStringFunctionFloor,
            
            FilterCriteriaToStringFunctionMax,
            
            FilterCriteriaToStringFunctionMin,
            
            FilterCriteriaToStringFunctionAcos,
            
            FilterCriteriaToStringFunctionAsin,
            
            FilterCriteriaToStringFunctionAtn2,
            
            FilterCriteriaToStringFunctionBigMul,
            
            FilterCriteriaToStringFunctionCosh,
            
            FilterCriteriaToStringFunctionLog10,
            
            FilterCriteriaToStringFunctionSinh,
            
            FilterCriteriaToStringFunctionTanh,
            
            FilterCriteriaToStringFunctionPadLeft,
            
            FilterCriteriaToStringFunctionPadRight,
            
            FilterCriteriaToStringFunctionDateDiffTick,
            
            FilterCriteriaToStringFunctionDateDiffSecond,
            
            FilterCriteriaToStringFunctionDateDiffMilliSecond,
            
            FilterCriteriaToStringFunctionDateDiffMinute,
            
            FilterCriteriaToStringFunctionDateDiffHour,
            
            FilterCriteriaToStringFunctionDateDiffDay,
            
            FilterCriteriaToStringFunctionDateDiffMonth,
            
            FilterCriteriaToStringFunctionDateDiffYear,
            
            FilterCriteriaToStringFunctionGetDate,
            
            FilterCriteriaToStringFunctionGetMilliSecond,
            
            FilterCriteriaToStringFunctionGetSecond,
            
            FilterCriteriaToStringFunctionGetMinute,
            
            FilterCriteriaToStringFunctionGetHour,
            
            FilterCriteriaToStringFunctionGetDay,
            
            FilterCriteriaToStringFunctionGetMonth,
            
            FilterCriteriaToStringFunctionGetYear,
            
            FilterCriteriaToStringFunctionGetDayOfWeek,
            
            FilterCriteriaToStringFunctionGetDayOfYear,
            
            FilterCriteriaToStringFunctionGetTimeOfDay,
            
            FilterCriteriaToStringFunctionNow,
            
            FilterCriteriaToStringFunctionUtcNow,
            
            FilterCriteriaToStringFunctionToday,
            
            FilterCriteriaToStringFunctionAddTimeSpan,
            
            FilterCriteriaToStringFunctionAddTicks,
            
            FilterCriteriaToStringFunctionAddMilliSeconds,
            
            FilterCriteriaToStringFunctionAddSeconds,
            
            FilterCriteriaToStringFunctionAddMinutes,
            
            FilterCriteriaToStringFunctionAddHours,
            
            FilterCriteriaToStringFunctionAddDays,
            
            FilterCriteriaToStringFunctionAddMonths,
            
            FilterCriteriaToStringFunctionAddYears,
            
            FilterCriteriaToStringFunctionStartsWith,
            
            FilterCriteriaToStringFunctionEndsWith,
            
            FilterCriteriaToStringFunctionContains,
            
            FilterCriteriaInvalidExpression,
            
            FilterCriteriaInvalidExpressionEx,
            
            Apply,
            
            PreviewPanelText,
            
            TransparentBackColorNotSupported,
            
            FilterOutlookDateText,
            
            FilterDateTimeConstantMenuCaption,
            
            FilterDateTimeOperatorMenuCaption,
            
            FilterAdvancedDateTimeOperatorMenuCaption,
            
            FilterCustomFunctionsMenuCaption,
            
            FilterDateTextAlt,
            
            FilterFunctionsMenuCaption,
            
            DefaultBooleanTrue,
            
            DefaultBooleanFalse,
            
            DefaultBooleanDefault,
            
            ProgressExport,
            
            ProgressPrinting,
            
            ProgressCreateDocument,
            
            ProgressCancel,
            
            ProgressCancelPending,
            
            ProgressLoadingData,
            
            ProgressPastingData,
            
            ProgressCopyingData,
            
            FilterAggregateAvg,
            
            FilterAggregateCount,
            
            FilterAggregateExists,
            
            FilterAggregateMax,
            
            FilterAggregateMin,
            
            FilterAggregateSum,
            
            FieldListName,
            
            RestoreLayoutDialogFileFilter,
            
            SaveLayoutDialogFileFilter,
            
            RestoreLayoutDialogTitle,
            
            SaveLayoutDialogTitle,
            
            PictureEditMenuZoom,
            
            PictureEditMenuFullSize,
            
            PictureEditMenuFitImage,
            
            PictureEditMenuZoomIn,
            
            PictureEditMenuZoomOut,
            
            PictureEditMenuZoomTo,
            
            PictureEditMenuZoomToolTip,
            
            FilterPopupToolbarShowOnlyAvailableItems,
            
            FilterPopupToolbarShowNewValues,
            
            FilterPopupToolbarIncrementalSearch,
            
            FilterPopupToolbarMultiSelection,
            
            FilterPopupToolbarRadioMode,
            
            FilterPopupToolbarInvertFilter,
            
            ColorPickPopupAutomaticItemCaption,
            
            ColorPickPopupThemeColorsGroupCaption,
            
            ColorPickPopupStandardColorsGroupCaption,
            
            ColorPickPopupRecentColorsGroupCaption,
            
            ColorPickPopupMoreColorsItemCaption,
            
            ColorPickHueAxisName,
            
            ColorPickSaturationAxisName,
            
            ColorPickLuminanceAxisName,
            
            ColorPickBrightnessAxisName,
            
            ColorPickOpacityAxisName,
            
            ColorPickRedValidationMsg,
            
            ColorPickGreenValidationMsg,
            
            ColorPickBlueValidationMsg,
            
            ColorPickOpacityValidationMsg,
            
            ColorPickColorHexValidationMsg,
            
            ColorPickHueValidationMsg,
            
            ColorPickSaturationValidationMsg,
            
            ColorPickBrightValidationMsg,
            
            ColorTabWebSafeColors,
            
            Days,
            
            Hours,
            
            Mins,
            
            Secs,
            
            Millisecs,
            
            TimeSpanDays,
            
            TimeSpanDaysPlural,
            
            TimeSpanDaysShort,
            
            TimeSpanHours,
            
            TimeSpanHoursPlural,
            
            TimeSpanHoursShort,
            
            TimeSpanMinutes,
            
            TimeSpanMinutesPlural,
            
            TimeSpanMinutesShort,
            
            TimeSpanSeconds,
            
            TimeSpanSecondsPlural,
            
            TimeSpanSecondsShort,
            
            TimeSpanFractionalSeconds,
            
            TimeSpanFractionalSecondsPlural,
            
            TimeSpanFractionalSecondsShort,
            
            TimeSpanMilliseconds,
            
            TimeSpanMillisecondsPlural,
            
            TimeSpanMillisecondsShort,
            
            PreviewPaused,
            
            PreviewError,
            
            PreviewPendingDeletion,
            
            PreviewPaperJam,
            
            PreviewPaperOut,
            
            PreviewManualFeed,
            
            PreviewPaperProblem,
            
            PreviewOffline,
            
            PreviewIOActive,
            
            PreviewBusy,
            
            PreviewPrinting,
            
            PreviewOutputBinFull,
            
            PreviewNotAvaible,
            
            PreviewWaiting,
            
            PreviewProcessing,
            
            PreviewInitializing,
            
            PreviewWarmingUp,
            
            PreviewTonerLow,
            
            PreviewNoToner,
            
            PreviewPagePunt,
            
            PreviewUserIntervention,
            
            PreviewOutOfMemory,
            
            PreviewDoorOpen,
            
            PreviewServerUnknown,
            
            PreviewPowerSave,
            
            PreviewReady,
            
            PreviewServerOffline,
            
            PreviewDriverUpdateNeeded,
            
            IncorrectNumberCopies,
            
            ChartRangeControlClientInvalidGrid,
            
            ChartRangeControlClientNoData,
            
            DataBarBlue,
            
            DataBarLightBlue,
            
            DataBarGreen,
            
            DataBarYellow,
            
            DataBarOrange,
            
            DataBarMint,
            
            DataBarViolet,
            
            DataBarRaspberry,
            
            DataBarCoral,
            
            DataBarBlueGradient,
            
            DataBarLightBlueGradient,
            
            DataBarGreenGradient,
            
            DataBarYellowGradient,
            
            DataBarOrangeGradient,
            
            DataBarMintGradient,
            
            DataBarVioletGradient,
            
            DataBarRaspberryGradient,
            
            DataBarCoralGradient,
            
            FormatRuleMenuItemDataUpdateRules,
            
            FormatRuleMenuItemClearColumnRules,
            
            FormatRuleMenuItemClearAllRules,
            
            FormatRuleMenuItemHighlightCellRules,
            
            FormatRuleMenuItemTopBottomRules,
            
            FormatRuleMenuItemDataBars,
            
            FormatRuleMenuItemColorScales,
            
            FormatRuleMenuItemIconSets,
            
            FormatRuleMenuItemClearRules,
            
            FormatRuleMenuItemManageRules,
            
            FormatRuleMenuItemUniqueDuplicateRules,
            
            FormatRuleMenuItemGradientFill,
            
            FormatRuleMenuItemSolidFill,
            
            FormatRuleMenuItemDataBarDescription,
            
            IconSetCategoryRatings,
            
            IconSetCategoryIndicators,
            
            IconSetCategorySymbols,
            
            IconSetCategoryShapes,
            
            IconSetCategoryDirectional,
            
            IconSetCategoryPositiveNegative,
            
            FormatRuleMenuItemIconSetDescription,
            
            ColorScaleGreenYellowRed,
            
            ColorScalePurpleWhiteAzure,
            
            ColorScaleYellowOrangeCoral,
            
            ColorScaleGreenWhiteRed,
            
            ColorScaleEmeraldAzureBlue,
            
            ColorScaleWhiteRed,
            
            ColorScaleWhiteGreen,
            
            ColorScaleWhiteAzure,
            
            ColorScaleYellowGreen,
            
            ColorScaleBlueWhiteRed,
            
            FormatRuleMenuItemColorScaleDescription,
            
            FormatRuleMenuItemUnique,
            
            FormatRuleUniqueText,
            
            FormatRuleMenuItemDuplicate,
            
            FormatRuleDuplicateText,
            
            FormatRuleApplyFormatProperty,
            
            FormatRuleWith,
            
            FormatRuleForThisColumnWith,
            
            IconSetTitleStars3,
            
            IconSetTitleRatings4,
            
            IconSetTitleRatings5,
            
            IconSetTitleQuarters5,
            
            IconSetTitleBoxes5,
            
            IconSetTitleArrows3Colored,
            
            IconSetTitleArrows3Gray,
            
            IconSetTitleTriangles3,
            
            IconSetTitleArrows4Colored,
            
            IconSetTitleArrows4Gray,
            
            IconSetTitleArrows5Colored,
            
            IconSetTitleArrows5Gray,
            
            IconSetTitleTrafficLights3Unrimmed,
            
            IconSetTitleTrafficLights3Rimmed,
            
            IconSetTitleSigns3,
            
            IconSetTitleTrafficLights4,
            
            IconSetTitleRedToBlack,
            
            IconSetTitleSymbols3Circled,
            
            IconSetTitleSymbols3Uncircled,
            
            IconSetTitleFlags3,
            
            IconSetTitlePositiveNegativeArrows,
            
            IconSetTitlePositiveNegativeArrowsGray,
            
            IconSetTitlePositiveNegativeTriangles,
            
            FormatRuleMenuItemTop10Items,
            
            FormatRuleMenuItemTop10Percent,
            
            FormatRuleMenuItemBottom10Items,
            
            FormatRuleMenuItemBottom10Percent,
            
            FormatRuleMenuItemAboveAverage,
            
            FormatRuleMenuItemBelowAverage,
            
            FormatRuleTopText,
            
            FormatRuleBottomText,
            
            FormatRuleAboveAverageText,
            
            FormatRuleBelowAverageText,
            
            FormatRuleMenuItemGreaterThan,
            
            FormatRuleMenuItemLessThan,
            
            FormatRuleMenuItemBetween,
            
            FormatRuleMenuItemEqualTo,
            
            FormatRuleMenuItemTextThatContains,
            
            FormatRuleMenuItemCustomCondition,
            
            FormatRuleGreaterThanText,
            
            FormatRuleLessThanText,
            
            FormatRuleBetweenText,
            
            FormatRuleEqualToText,
            
            FormatRuleTextThatContainsText,
            
            FormatRuleCustomConditionText,
            
            FormatRuleDataUpdateText,
            
            FormatRuleExpressionEmptyEnter,
            
            FormatRuleMenuItemDateOccurring,
            
            FormatRuleDateOccurring,
            
            FormatPredefinedAppearanceRedFillRedText,
            
            FormatPredefinedAppearanceYellowFillYellowText,
            
            FormatPredefinedAppearanceGreenFillGreenText,
            
            FormatPredefinedAppearanceRedFill,
            
            FormatPredefinedAppearanceRedText,
            
            FormatPredefinedAppearanceGreenFill,
            
            FormatPredefinedAppearanceGreenText,
            
            FormatPredefinedAppearanceBoldText,
            
            FormatPredefinedAppearanceItalicText,
            
            FormatPredefinedAppearanceStrikeoutText,
            
            FormatPredefinedAppearanceRedBoldText,
            
            FormatPredefinedAppearanceGreenBoldText,
            
            SearchForColumn,
            
            SearchForField,
            
            ManageRuleCaption,
            
            ManageRuleShowFormattingRules,
            
            ManageRuleUp,
            
            ManageRuleDown,
            
            ManageRuleNewRule,
            
            ManageRuleEditRule,
            
            ManageRuleDeleteRule,
            
            ManageRuleGridCaptionRule,
            
            ManageRuleGridCaptionFormat,
            
            ManageRuleGridCaptionApplyToTheRow,
            
            ManageRuleGridCaptionColumn,
            
            ManageRuleGridCaptionStopIfTrue,
            
            ManageRuleGridCaptionColumnApplyTo,
            
            ManageRuleAllColumns,
            
            NewFormattingRule,
            
            EditFormattingRule,
            
            NewEditFormattingRuleSelectARuleType,
            
            NewEditFormattingRuleEditTheRuleDescription,
            
            NewEditFormattingRuleFormatAllCellsBasedOnTheirValues,
            
            NewEditFormattingRuleFormatOnlyCellsThatContain,
            
            NewEditFormattingRuleFormatOnlyTopOrBottomRankedValues,
            
            NewEditFormattingRuleFormatOnlyValuesThatAreAboveOrBelowAverage,
            
            NewEditFormattingRuleFormatOnlyUniqueOrDuplicateValues,
            
            NewEditFormattingRuleFormatOnlyChangingValues,
            
            NewEditFormattingRuleUseAFormulaToDetermineWhichCellsToFormat,
            
            ManageRuleComplexRuleBaseFormatStyle,
            
            ManageRuleColorScale2,
            
            ManageRuleColorScale3,
            
            ManageRuleDataBar,
            
            ManageRuleIconSets,
            
            ManageRuleCommonMinimum,
            
            ManageRuleCommonMaximum,
            
            ManageRuleCommonType,
            
            ManageRuleCommonAutomatic,
            
            ManageRuleCommonPercent,
            
            ManageRuleCommonNumber,
            
            ManageRuleCommonValue,
            
            ManageRuleCommonColor,
            
            ManageRuleCommonPreview,
            
            ManageRuleNoFormatSet,
            
            ManageRuleColorScaleMidpoint,
            
            ManageRuleDataBarBarAppearance,
            
            ManageRuleDataBarFill,
            
            ManageRuleDataBarBorder,
            
            ManageRuleDataBarDrawAxis,
            
            ManageRuleDataBarUseNegativeBar,
            
            ManageRuleDataBarAxisColor,
            
            ManageRuleDataBarBarDirection,
            
            ManageRuleDataBarSolidFill,
            
            ManageRuleDataBarGradientFill,
            
            ManageRuleDataBarNoBorder,
            
            ManageRuleDataBarSolidBorder,
            
            ManageRuleDataBarContext,
            
            ManageRuleDataBarLTR,
            
            ManageRuleDataBarRTL,
            
            ManageRuleIconSetsDisplayEachIconAccordingToTheseRules,
            
            ManageRuleIconSetsReverseIconOrder,
            
            ManageRuleIconSetsWhen,
            
            ManageRuleIconSetsValueIs,
            
            ManageRuleSimpleRuleBaseFormat,
            
            ManageRuleAverageFormatValuesThatAre,
            
            ManageRuleAverageTheAverageForTheSelectedRange,
            
            ManageRuleAverageAbove,
            
            ManageRuleAverageBelow,
            
            ManageRuleAverageEqualOrAbove,
            
            ManageRuleAverageEqualOrBelow,
            
            ManageRuleFormulaFormatValuesWhereThisFormulaIsTrue,
            
            ManageRuleRankedValuesFormatValuesThatRankInThe,
            
            ManageRuleRankedValuesOfTheColumnsCellValues,
            
            ManageRuleRankedValuesTop,
            
            ManageRuleRankedValuesBottom,
            
            ManageRuleThatContainFormatOnlyCellsWith,
            
            ManageRuleThatContainCellValue,
            
            ManageRuleThatContainDatesOccurring,
            
            ManageRuleThatContainSpecificText,
            
            ManageRuleThatContainBlanks,
            
            ManageRuleThatContainNoBlanks,
            
            ManageRuleThatContainErrors,
            
            ManageRuleThatContainNoErrors,
            
            ManageRuleCellValueBetween,
            
            ManageRuleCellValueNotBetween,
            
            ManageRuleCellValueEqualTo,
            
            ManageRuleCellValueNotEqualTo,
            
            ManageRuleCellValueGreaterThan,
            
            ManageRuleCellValueLessThan,
            
            ManageRuleCellValueGreaterThanOrEqualTo,
            
            ManageRuleCellValueLessThanOrEqualTo,
            
            ManageRuleDatesOccurringBeyond,
            
            ManageRuleDatesOccurringBeyondThisYear,
            
            ManageRuleDatesOccurringEarlier,
            
            ManageRuleDatesOccurringEarlierThisMonth,
            
            ManageRuleDatesOccurringEarlierThisWeek,
            
            ManageRuleDatesOccurringEarlierThisYear,
            
            ManageRuleDatesOccurringLastWeek,
            
            ManageRuleDatesOccurringLaterThisMonth,
            
            ManageRuleDatesOccurringLaterThisWeek,
            
            ManageRuleDatesOccurringLaterThisYear,
            
            ManageRuleDatesOccurringMonthAfter1,
            
            ManageRuleDatesOccurringMonthAfter2,
            
            ManageRuleDatesOccurringMonthAgo1,
            
            ManageRuleDatesOccurringMonthAgo2,
            
            ManageRuleDatesOccurringMonthAgo3,
            
            ManageRuleDatesOccurringMonthAgo4,
            
            ManageRuleDatesOccurringMonthAgo5,
            
            ManageRuleDatesOccurringMonthAgo6,
            
            ManageRuleDatesOccurringNextWeek,
            
            ManageRuleDatesOccurringPriorThisYear,
            
            ManageRuleDatesOccurringThisMonth,
            
            ManageRuleDatesOccurringThisWeek,
            
            ManageRuleDatesOccurringTomorrow,
            
            ManageRuleDatesOccurringToday,
            
            ManageRuleDatesOccurringYesterday,
            
            ManageRuleSpecificTextContaining,
            
            ManageRuleSpecificTextNotContaining,
            
            ManageRuleSpecificTextBeginningWith,
            
            ManageRuleSpecificTextEndingWith,
            
            ManageRuleUniqueOrDuplicateFormatAll,
            
            ManageRuleUniqueOrDuplicateValuesInTheSelectedRange,
            
            ManageRuleUniqueOrDuplicateUnique,
            
            ManageRuleUniqueOrDuplicateDuplicate,
            
            ManageRuleDataUpdate,
            
            ManageRuleColorScale,
            
            ManageRuleIconSet,
            
            ManageRuleFormula,
            
            ManageRuleAboveAverage,
            
            ManageRuleBelowAverage,
            
            ManageRuleEqualOrAboveAverage,
            
            ManageRuleEqualOrBelowAverage,
            
            ManageRuleFormatCellsCaption,
            
            ManageRuleFormatCellsFont,
            
            ManageRuleFormatCellsFill,
            
            ManageRuleFormatCellsPredefinedAppearance,
            
            ManageRuleFormatCellsFontStyle,
            
            ManageRuleFormatCellsFontColor,
            
            ManageRuleFormatCellsEffects,
            
            ManageRuleFormatCellsUnderline,
            
            ManageRuleFormatCellsStrikethrough,
            
            ManageRuleFormatCellsClear,
            
            ManageRuleFormatCellsBackgroundColor,
            
            ManageRuleFormatCellsNone,
            
            ManageRuleFormatCellsRegular,
            
            ManageRuleFormatCellsBold,
            
            ManageRuleFormatCellsItalic,
            
            ManageRuleValuesFor,
            
            ManageRuleMillisecond,
            
            TakePictureDialogTitle,
            
            TakePictureMenuItem,
            
            TakePictureDialogCapture,
            
            TakePictureDialogTryAgain,
            
            TakePictureDialogSave,
            
            CameraSettingsActiveDevice,
            
            CameraSettingsBrightness,
            
            CameraSettingsContrast,
            
            CameraSettingsDesaturate,
            
            CameraSettingsDefaults,
            
            CameraSettingsCaption,
            
            CameraSettingsResolution,
            
            CameraDeviceNotFound,
            
            CameraDeviceIsBusy,
            
            CameraDesignTimeInfo,
            
            OfficeNavigationOptions,
            
            FilterCriteriaToStringFunctionIsNextMonth,
            
            FilterCriteriaToStringFunctionIsNextYear,
            
            FilterCriteriaToStringFunctionIsLastMonth,
            
            FilterCriteriaToStringFunctionIsLastYear,
            
            FilterCriteriaToStringFunctionIsYearToDate,
            
            FilterCriteriaToStringFunctionLocalDateTimeTwoMonthsAway,
            
            FilterCriteriaToStringFunctionLocalDateTimeTwoYearsAway,
            
            FilterCriteriaToStringFunctionLocalDateTimeLastMonth,
            
            FilterCriteriaToStringFunctionLocalDateTimeLastYear,
            
            FilterCriteriaToStringFunctionLocalDateTimeYearBeforeToday,
            
            FilterCriteriaToStringFunctionIsJanuary,
            
            FilterCriteriaToStringFunctionIsFebruary,
            
            FilterCriteriaToStringFunctionIsMarch,
            
            FilterCriteriaToStringFunctionIsApril,
            
            FilterCriteriaToStringFunctionIsMay,
            
            FilterCriteriaToStringFunctionIsJune,
            
            FilterCriteriaToStringFunctionIsJuly,
            
            FilterCriteriaToStringFunctionIsAugust,
            
            FilterCriteriaToStringFunctionIsSeptember,
            
            FilterCriteriaToStringFunctionIsOctober,
            
            FilterCriteriaToStringFunctionIsNovember,
            
            FilterCriteriaToStringFunctionIsDecember,
            
            FilterCriteriaToStringFunctionIsSameDay,
            
            FilterCriteriaToStringFunctionInRange,
            
            FilterCriteriaToStringFunctionInDateRange,
            
            FilterCriteriaToStringFunctionNotInRange,
            
            FilterCriteriaToStringFunctionNotInDateRange,
            
            NoneItemText,
            
            ProgressPanelDefaultCaption,
            
            ProgressPanelDefaultDescription,
            
            FormatRuleNoCellIcon,
            
            PictureEditMenuEdit,
            
            ImageEditorDialogCaption,
            
            DataUpdateTriggerChanged,
            
            DataUpdateTriggerIncreased,
            
            DataUpdateTriggerDecreased,
            
            FilterNewEmptyEnter,
            
            FilterNewEmptyParameter,
            
            FilterEmptyField,
            
            FilterExpressionEmptyText,
            
            ChartRangeControlClientRangeValidationMsg,
            
            AllRightsReserved,
            
            Version,
            
            ManageRuleGridCaptionApplyToTheRecord,
            
            ManageRuleGridCaptionRow,
            
            ManageRuleGridCaptionRowApplyTo,
            
            ManageRuleRankedValuesOfTheRowCellValues,
            
            ManageRuleUniqueOrDuplicateValuesInTheSelectedRangeRow,
            
            ManageRuleAverageTheAverageForTheSelectedRangeRow,
            
            FormatRuleApplyFormatPropertyRecord,
            
            FormatRuleForThisRowWith,
            
            ManageRuleFormatCellsFontSizeDelta,
            
            DXCollectionEditorOKButtonText,
            
            DXCollectionEditorCancelButtonText,
            
            DXCollectionEditorAddItemButtonText,
            
            DXCollectionEditorRemoveItemButtonText,
            
            DXCollectionEditorItemsListGroupCaptionStringFormat,
            
            DXCollectionEditorPreviewGroupCaption,
            
            DXCollectionEditorItemPropertiesGroupCaption,
            
            DXCollectionEditorSomeItemsTypeAddItemButtonStringFormat,
            
            SyntaxEditFindPanelFindCaption,
            
            SyntaxEditFindPanelReplaceCaption,
            
            SyntaxEditClearButtonCaption,
            
            SyntaxEditShowDropdownButtonCaption,
            
            SyntaxEditReplaceButtonTooltip,
            
            SyntaxEditReplaceAllButtonTooltip,
            
            SyntaxEditFindPanelFindNextButtonTooltip,
            
            SyntaxEditFindPanelFindPreviousButtonTooltip,
            
            SyntaxEditFindPanelCloseButtonTooltip,
         
            SyntaxEditFindPanelExpandButtonTooltip
        }
    }
}
