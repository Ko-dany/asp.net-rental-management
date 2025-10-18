using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
            var client = _httpClientFactory.CreateClient();
            var loginRequest = new LoginRequest
            {
                Username = model.Username,
                Password = model.Password
            };
            var response = await client.PostAsJsonAsync("https://localhost:7048/api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<JsonElement>(json, options);

                var token = result.GetProperty("token").GetString();
                model.Token = token;
                HttpContext.Session.SetString("JwtToken", model.Token);
                model.ErrorMessage = null;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

                return roleClaim switch
                {
                    UserRole.Admin => RedirectToAction("AdminDashboard"),
                    UserRole.User => RedirectToAction("UserDashboard"),
                    _ => RedirectToAction("Login")
                };
            }
            else
            {
                model.Token = null;
                model.ErrorMessage = "Invalid username or password";
                return View(model);
            }
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
