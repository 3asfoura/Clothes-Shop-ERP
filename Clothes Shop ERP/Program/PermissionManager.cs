using Clothes_Shop_ERP.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Clothes_Shop_ERP
{
    // Central place that knows which screens the currently logged-in role can
    // see/use. Loaded once at login (see FrmLogin), then read from everywhere
    // else (FrmMain sidebar, and later the individual screens) without hitting
    // the database again.
    public static class PermissionManager
    {
        public const string LevelNone = "None";
        public const string LevelRead = "Read";
        public const string LevelWrite = "Write";

        // Every screen that can be permission-gated, and the localization key
        // for its display name (same keys FrmMain.ApplyLanguage uses for the
        // sidebar), in sidebar order. Used both to hide sidebar entries and to
        // build the per-role permission list in FrmRoleEdit.
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
        };

        private static Dictionary<string, string> _levelsByScreen = new Dictionary<string, string>();
        private static bool _fullAccess;

        public static void Load(int roleId)
        {
            _levelsByScreen = new Dictionary<string, string>();

            using (var db = new ClothesShopDBContext())
            {
                // Safety net: the very first role ever created (lowest Id) always
                // gets full access everywhere, so a misconfigured RolePermissions
                // table can never lock every admin out of every screen at once.
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

        // Screens not listed for a role default to None (deny by default),
        // matching the RolePermissions table's own DEFAULT 'None'.
        public static string GetLevel(string screenName)
        {
            if (_fullAccess) return LevelWrite;
            return _levelsByScreen.TryGetValue(screenName, out var level) ? level : LevelNone;
        }

        public static bool CanView(string screenName) => GetLevel(screenName) != LevelNone;

        public static bool CanEdit(string screenName) => GetLevel(screenName) == LevelWrite;
    }
}
