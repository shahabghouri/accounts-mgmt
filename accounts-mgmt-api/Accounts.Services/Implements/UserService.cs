using Accounts.Common.DTO;
using Accounts.Common.Exceptions;
using Accounts.Domain.Entities;
using Accounts.Services.Aggregates;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Accounts.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<int> Signup(UserDTO request)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Device = request.Device,
                IpAddress = request.IpAddress,
                IsFirstLogin = true,
                Balance = 0
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new UserTypeException(string.Join("\n", result.Errors.Select(e => e.Description)));
            }

            return user.Id.GetHashCode();
        }

        public async Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
                return null;

            user.LastLogin = DateTime.UtcNow;
            user.IpAddress = request.IpAddress;
            user.Device = request.Device;

            if (user.IsFirstLogin)
            {
                user.Balance += 5; // Add 5 GBP on first login
                user.IsFirstLogin = false;
            }

            await _userManager.UpdateAsync(user);

            var token = GenerateJwtToken(user);

            return new AuthenticateResponseDTO
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Token = token
            };
        }
        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserTypeException("User not found");
            }

            return user.Balance;
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
