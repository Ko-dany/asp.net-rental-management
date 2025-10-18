using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Customers.GetCustomerDB().FirstOrDefault(c => c.UserName == loginRequest.Username && c.Password == loginRequest.Password);
            if(user == null) { return Unauthorized("Invalid username or password"); }
            var token = GenerateToken(user);
            return Ok(new { token });
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

        [HttpGet("session")]
        public IActionResult CheckToken()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Ok("No token found in session");
            }
            return Ok($"Token in session: {token}");
        }
    }
}
