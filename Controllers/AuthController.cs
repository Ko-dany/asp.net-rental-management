using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Services;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _context;

        public AuthController(IAuthService context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.ValidateCredentials(loginRequest);
            if (user == null) { return Unauthorized("Invalid username or password"); }
            var token = _context.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
