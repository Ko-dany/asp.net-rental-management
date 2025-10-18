using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Midterm_EquipmentRental.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _context;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Customer? ValidateCredentials(LoginRequest loginRequest)
        {
            return _context.Customers.GetCustomerDB().FirstOrDefault(c => c.UserName == loginRequest.Username && c.Password == loginRequest.Password);
        }

        public string GenerateToken(Customer customer)
        {
            var secret = _configuration["JwtSettings:Secret"];
            var expiresMinutes = int.Parse(_configuration["JwtSettings:ExpiresMinutes"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customer.UserName),
                new Claim(ClaimTypes.Role, customer.Role.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expiresMinutes),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
