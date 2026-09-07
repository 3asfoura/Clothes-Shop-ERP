using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class AuditLogs
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string Action { get; set; }
        public int? ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
