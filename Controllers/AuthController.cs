using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Data;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("LoginCallback")]
        public async Task<IActionResult> LoginCallback(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email not provided");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("userId", user.Id.ToString());
            HttpContext.Session.SetString("role", user.Role);

            return RedirectToAction("GoToDashboard", "View");
        }
    }
}
