using Accounts.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Services.Aggregates
{
    public interface IUserService
    {
        Task<int> Signup(UserDTO request);
        Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO request);
        Task<decimal> GetBalanceAsync(string userId);
    }

}
