using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Shop_ERP.DAL
{
    public partial class ClothesShopDBContext
    {
        private static readonly string[] AuditedTables =
        {
               "Products", "ProductVariants", "Users", "SalesInvoices", "PurchaseInvoices", "Branches"
        };
        public override int SaveChanges()
        {

            var trackedEntries = ChangeTracker.Entries()
    .Where(e => AuditedTables.Contains(e.Entity.GetType().Name)
             && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
    .Select(e => new { Entry = e, e.State, TypeName = e.Entity.GetType().Name })
    .ToList();

            int result = base.SaveChanges();   // this also fills in the Id for new (Added) rows

            foreach (var item in trackedEntries)
            {
                string action = item.State == EntityState.Added ? "Insert"
                              : item.State == EntityState.Modified ? "Update"
                              : "Delete";

                var idProp = item.Entry.Entity.GetType().GetProperty("Id");
                string recordId = idProp != null ? idProp.GetValue(item.Entry.Entity)?.ToString() : null;

                this.AuditLogs.Add(new AuditLogs
                {
                    TableName = item.TypeName,
                    RecordId = recordId,
                    Action = action,
                    ChangedByUserId = Clothes_Shop_ERP.FrmLogin.CurrentUserId == 0
                        ? (int?)null
                        : Clothes_Shop_ERP.FrmLogin.CurrentUserId,
                    ChangedAt = DateTime.Now
                });
            }

            if (trackedEntries.Count > 0)
                base.SaveChanges();  

            return result;
        }
    }
}