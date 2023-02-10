using System;
using System.Collections.Generic;

#nullable disable

namespace WebLazadaApi.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountLogins = new HashSet<AccountLogin>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AccountLogin> AccountLogins { get; set; }
    }
}
