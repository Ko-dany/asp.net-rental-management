using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Models.ViewModels;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Midterm_EquipmentRental.Controllers
{
    public class ViewController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEquipmentService _equipmentService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;
        private readonly IDashboardService _dashboardService;

        public ViewController(IAuthService authService, IEquipmentService equipmentService, ICustomerService customerService, IRentalService rentalService, IDashboardService dashboardService)
        {
            _authService = authService;
            _equipmentService = equipmentService;
            _customerService = customerService;
            _rentalService = rentalService;
            _dashboardService = dashboardService;
        }

        /* Authentication view */
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
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            HttpContext.Session.SetString("role", roleClaim);
            HttpContext.Session.SetString("username", loginRequest.Username);
            HttpContext.Session.SetString("JwtToken", token);

            return RedirectToAction("GoToDashboard");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", new LoginViewModel());
        }

        [HttpGet]
        public IActionResult GoToDashboard()
        {
            var role = HttpContext.Session.GetString("role");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login");

            if (role == UserRole.Admin || role == UserRole.User)
            {
                DashboardViewModel dashboardViewModel = _dashboardService.GetDashboardInfo();
                dashboardViewModel.UserName = HttpContext.Session.GetString("username") ?? "";
                dashboardViewModel.RoleName = role;
                dashboardViewModel.IsAdmin = role == UserRole.Admin;
                dashboardViewModel.SystemStatus = "Online";

                return View("~/Views/Dashboard/Index.cshtml", dashboardViewModel);
            }
            return RedirectToAction("Login");
        }

        /* Equipments view */
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipment()
        {
            var role = HttpContext.Session.GetString("role");

            var equipments = _equipmentService.GetAllEquipment();
            EquipmentListViewModel equipmentsList = new EquipmentListViewModel()
            {
                Items = equipments,
                IsAdmin = role == UserRole.Admin
            };
            return View("~/Views/Equipment/Index.cshtml", equipmentsList);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllAvailableEquipment()
        {
            var role = HttpContext.Session.GetString("role");

            var equipments = _equipmentService.GetAllAvailableEquipment();
            EquipmentListViewModel equipmentsList = new EquipmentListViewModel()
            {
                Items = equipments,
                Filter = "Available",
                IsAdmin = role == UserRole.Admin
            };
            return View("~/Views/Equipment/Index.cshtml", equipmentsList);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllRentedEquipment()
        {
            var role = HttpContext.Session.GetString("role");

            var equipments = _equipmentService.GetAllRentedEquipment();
            EquipmentListViewModel equipmentsList = new EquipmentListViewModel()
            {
                Items = equipments,
                Filter = "Rented",
                IsAdmin = role == UserRole.Admin
            };
            return View("~/Views/Equipment/Index.cshtml", equipmentsList);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult AddEquipment()
        {
            return View("~/Views/Equipment/Create.cshtml", new Equipment());
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult AddEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return View(equipment);
            }
            equipment.CreatedAt = DateTime.Now;
            _equipmentService.AddEquipment(equipment);
            return RedirectToAction("GetAllEquipment");
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public IActionResult UpdateEquipment(int id)
        {
            var equipment = _equipmentService.GetEquipmentById(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View("~/Views/Equipment/Edit.cshtml", equipment);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult UpdateEquipment(int id, Equipment equipment)
        {
            if(id != equipment.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(equipment);
            }
            equipment.CreatedAt = DateTime.Now;
            _equipmentService.UpdateEquipment(equipment);
            return RedirectToAction("GetAllEquipment");
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult DeleteEquipment(int id)
        {
            var existingEquipment = _equipmentService.GetEquipmentById(id);
            if (existingEquipment == null) 
            {
                return NotFound();
            }
            _equipmentService.DeleteEquipment(existingEquipment);
            return RedirectToAction("GetAllEquipment");
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public IActionResult GetEquipmentDetails(int id)
        {
            var equipment = _equipmentService.GetEquipmentById(id);
            return View("~/Views/Equipment/Details.cshtml", equipment);
        }

        /* Rentals view */
        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetAllRentals()
        {
            var rentals = _rentalService.GetAllRentals();
            return View("~/Views/Rentals/Index.cshtml", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetActiveRentals()
        {
            var rentals = _rentalService.GetActiveRentals();
            return View("~/Views/Rentals/Index.cshtml", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetCompletedRentals()
        {
            var rentals = _rentalService.GetCompletedRentals();
            return View("~/Views/Rentals/Index.cshtml", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetOverdueRentals()
        {
            var rentals = _rentalService.GetOverdueRentals();
            return View("~/Views/Rentals/Index.cshtml", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public IActionResult GetRentalDetails(int id)
        {
            var rental = _rentalService.GetRentalById(id);
            if(rental == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerById(rental.CustomerId);
            var equipment = _equipmentService.GetEquipmentById(rental.EquipmentId);

            string customerName = string.IsNullOrWhiteSpace(customer?.Name)
                ? "Customer name is missing"
                : customer.Name;

            string customerUsername = string.IsNullOrWhiteSpace(customer?.UserName)
                ? "Customer username is missing"
                : customer.UserName;

            string equipmentName = string.IsNullOrWhiteSpace(equipment?.Name)
                ? "Equipment name is missing"
                : equipment.Name;
            RentalDetailsViewModel rentalDetails = new RentalDetailsViewModel()
            {
                Rental = rental,
                CustomerName = customerName,
                CustomerUserName = customerUsername,
                EquipmentName = equipmentName
            };
            return View("~/Views/Rentals/Details.cshtml", rentalDetails);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public IActionResult GetExtendRental(int id)
        {
            var rental = _rentalService.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View("~/Views/Rentals/Extend.cshtml", rental);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult ExtendRental(int id, DateTime newDueDate)
        {
            var rental = _rentalService.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }
            if(newDueDate <= DateTime.Now)
            {
                return View(rental);
            }
            _rentalService.ExtendRentalById(id, newDueDate);
            return RedirectToAction("GetAllRentals");
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult CancelRental(int id)
        {
            var rental = _rentalService.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }
            _rentalService.CancelRentalById(id);
            return RedirectToAction("GetAllRentals");
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult ReturnRental(Rental rental)
        {
            if (rental == null)
            {
                return NotFound();
            }
            _rentalService.ReturnEquipment(rental);
            return RedirectToAction("GetAllRentals");
        }

        /* Customers view */
        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return View("~/Views/Customers/Index.cshtml", customers);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View("~/Views/Customers/Create.cshtml", new Customer());
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerService.AddCustomer(customer);
            return RedirectToAction("GetAllCustomers");
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public IActionResult GetCustomerDetails(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            customer.RentalHistory = _rentalService.GetAllRentals().Where(r => r.CustomerId == customer.Id).ToList();
            return View("~/Views/Customers/Details.cshtml", customer);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("~/Views/Customers/Edit.cshtml", customer);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpPost]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerService.UpdateCustomer(customer);
            return RedirectToAction("GetAllCustomers");
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerService.DeleteCustomer(customer);
            return RedirectToAction("GetAllCustomers");
        }
    }
}
