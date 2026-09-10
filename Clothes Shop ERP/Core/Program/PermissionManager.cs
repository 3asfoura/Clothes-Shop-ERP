using Clothes_Shop_ERP.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Clothes_Shop_ERP
{
    // Knows which screens the logged-in role can see/use. Loaded once at login.
    public static class PermissionManager
    {
        public const string LevelNone = "None";
        public const string LevelRead = "Read";
        public const string LevelWrite = "Write";

        // Every permission-gated screen and its sidebar localization key, in sidebar order.
        public static readonly Dictionary<string, string> AllScreens = new Dictionary<string, string>
        {
            { "Products", "Main_Products" },
            { "ProductVariants", "Main_ProductVariants" },
            { "Categories", "Main_Categories" },
            { "Brands", "Main_Brands" },
            { "ColorsSizes", "Main_ColorsSizes" },
            { "StockCount", "Main_StockCount" },
            { "StockMovements", "Main_StockMovements" },
            { "BranchTransfer", "Main_BranchTransfer" },
            { "PointOfSale", "Main_PointOfSale" },
            { "SalesInvoices", "Main_SalesInvoices" },
            { "Returns", "Main_Returns" },
            { "CashierShifts", "Main_CashierShifts" },
            { "Customers", "Main_Customers" },
            { "PurchaseInvoices", "Main_PurchaseInvoices" },
            { "PurchaseReturns", "Main_PurchaseReturns" },
            { "Suppliers", "Main_Suppliers" },
            { "Treasury", "Main_TreasuryTransactions" },
            { "TreasuryBalance", "Main_TreasuryBalance" },
            { "SalesReport", "Main_SalesReport" },
            { "StockReport", "Main_StockReport" },
            { "ProfitReport", "Main_ProfitReport" },
            { "AccountStatement", "Main_AccountStatement" },
            { "DayClosingReport", "Main_DayClosingReport" },
            { "Branches", "Main_Branches" },
            { "UsersRoles", "Main_UsersRoles" },
            { "PaymentMethods", "Main_PaymentMethods" },
            { "AuditLogs", "Main_AuditLogs" },
            { "BackupSettings", "Main_BackupSettings" },
            { "About", "Main_About" },
        };

        private static Dictionary<string, string> _levelsByScreen = new Dictionary<string, string>();
        private static bool _fullAccess;

        public static void Load(int roleId)
        {
            _levelsByScreen = new Dictionary<string, string>();

            using (var db = new ClothesShopDBContext())
            {
                // Safety net: the first role ever created always gets full access everywhere.
                int firstRoleId = db.Roles.OrderBy(r => r.Id).Select(r => r.Id).FirstOrDefault();
                _fullAccess = roleId == firstRoleId;

                if (!_fullAccess)
                {
                    _levelsByScreen = db.RolePermissions
                        .Where(x => x.RoleId == roleId)
                        .ToDictionary(x => x.ScreenName, x => x.PermissionLevel);
                }
            }
        }

        // Screens not listed for a role default to None (deny by default).
        public static string GetLevel(string screenName)
        {
            if (_fullAccess) return LevelWrite;
            return _levelsByScreen.TryGetValue(screenName, out var level) ? level : LevelNone;
        }

        public static bool CanView(string screenName) => GetLevel(screenName) != LevelNone;

        public static bool CanEdit(string screenName) => GetLevel(screenName) == LevelWrite;

        // Non-full-access roles only ever see data for their own logged-in branch.
        public static bool BranchRestricted => !_fullAccess;
    }
}
