using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class RolePermissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ScreenName { get; set; }
        public string PermissionLevel { get; set; }

        public Roles Role { get; set; }
    }
}
