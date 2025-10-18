using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Services;

namespace Midterm_EquipmentRental.Controllers
{
    public class ViewController : Controller
    {
        private readonly IAuthService _authService;

        public ViewController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var loginRequest = new LoginRequest
            {
                Username = model.Username,
                Password = model.Password
            };

            var user = _authService.ValidateCredentials(loginRequest);
            
            if (user == null)
            {
                model.ErrorMessage = "Invalid username or password";
                return View(model);
            }

            var token = _authService.GenerateToken(user);
            HttpContext.Session.SetString("JwtToken", token);

            return user.Role switch
            {
                UserRole.Admin => RedirectToAction("AdminDashboard"),
                UserRole.User => RedirectToAction("UserDashboard"),
                _ => RedirectToAction("Login")
            };
        }


        [HttpGet]
        public IActionResult AdminDashboard()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            if (roleClaim != UserRole.Admin)
                return RedirectToAction("Login");

            return View();
        }

        [HttpGet]
        public IActionResult UserDashboard()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            if (roleClaim != UserRole.User)
                return RedirectToAction("Login");

            return View();
        }
    }
}
