using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.Localization
{
    public enum AppLanguage { English, Egyptian }

    public static class LocalizationManager
    {
      
        public static AppLanguage CurrentLanguage = AppLanguage.Egyptian;
        private static readonly string SettingsFilePath =
            Path.Combine(Clothes_Shop_ERP.Sett.AppDataFolder, "lang.settings");
        public static void LoadLanguagePreference()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string saved = File.ReadAllText(SettingsFilePath).Trim();
                    if (Enum.TryParse(saved, out AppLanguage lang))
                    {
                        CurrentLanguage = lang;
                    }
                }
            }
            catch
            {
               
            }
        }
        public static void SaveLanguagePreference()
        {
            try
            {
                File.WriteAllText(SettingsFilePath, CurrentLanguage.ToString());
            }
            catch
            {
               
            }
        }
        public static string T(string key)
        {
            Dictionary<string, string> dict = CurrentLanguage == AppLanguage.English
                ? Lang_English.Strings
                : Lang_Arabic.Strings;

            if (dict.TryGetValue(key, out string value))
                return value;

            return key;
        }

        // DB-stored codes stay in English (compared against elsewhere); this only translates for display.
        public static string TranslateStatusCode(string code)
        {
            switch (code)
            {
                case "Completed": return T("Status_Completed");
                case "Pending": return T("Status_Pending");
                case "Cancelled": return T("Status_Cancelled");
                case "Open": return T("Status_Open");
                case "Closed": return T("Status_Closed");
                case "In": return T("Status_In");
                case "Out": return T("Status_Out");
                case "Insert": return T("Status_Insert");
                case "Update": return T("Status_Update");
                case "Delete": return T("Status_Delete");
                case "Sale": return T("Status_Sale");
                case "Purchase": return T("Status_Purchase");
                case "Return": return T("Status_Return");
                case "PurchaseReturn": return T("Status_PurchaseReturn");
                case "TransferIn": return T("Status_TransferIn");
                case "TransferOut": return T("Status_TransferOut");
                case "SalesInvoice": return T("Status_SalesInvoice");
                case "PurchaseInvoice": return T("Status_PurchaseInvoice");
                case "SalesReturn": return T("Status_SalesReturn");
                case "StockTransfer": return T("Status_StockTransfer");
                case "Manual": return T("Status_Manual");
                default: return code;
            }
        }

        // Raw entity/table names from the audit trail, translated for display only.
        public static string TranslateTableName(string tableName)
        {
            switch (tableName)
            {
                case "Products": return T("TableName_Products");
                case "ProductVariants": return T("TableName_ProductVariants");
                case "Users": return T("TableName_Users");
                case "SalesInvoices": return T("TableName_SalesInvoices");
                case "PurchaseInvoices": return T("TableName_PurchaseInvoices");
                case "Branches": return T("TableName_Branches");
                case "BranchStock": return T("TableName_BranchStock");
                case "SalesInvoiceDetails": return T("TableName_SalesInvoiceDetails");
                case "SalesReturns": return T("TableName_SalesReturns");
                case "SalesReturnDetails": return T("TableName_SalesReturnDetails");
                case "StockMovements": return T("TableName_StockMovements");
                case "TreasuryTransactions": return T("TableName_TreasuryTransactions");
                default: return tableName;
            }
        }

        // Swaps only the leading English label; the embedded reference number is never translated.
        public static string TranslateTreasuryDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) return description;

            if (description == "Purchase return") return T("TreasuryDesc_PurchaseReturn");
            if (description == "Sales return") return T("TreasuryDesc_SalesReturn");

            const string salePrefix = "Sale - ";
            if (description.StartsWith(salePrefix))
                return T("TreasuryDesc_SalePrefix") + description.Substring(salePrefix.Length);

            const string paySupplierPrefix = "Payment to supplier - Invoice #";
            if (description.StartsWith(paySupplierPrefix))
                return T("TreasuryDesc_PaymentToSupplierPrefix") + description.Substring(paySupplierPrefix.Length);

            return description;
        }
    }
}