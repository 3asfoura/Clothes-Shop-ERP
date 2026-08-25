using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
