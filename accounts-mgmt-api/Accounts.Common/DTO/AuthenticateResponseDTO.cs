using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Common.DTO
{
    public class AuthenticateResponseDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }
        
    }
}
