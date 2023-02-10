using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLazadaMvc.Models
{
    public class MVCAccount
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

    }
}
