using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Device { get; set; }
        public string IpAddress { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsFirstLogin { get; set; } = true;
        public decimal Balance { get; set; } = 0;
    }
}
