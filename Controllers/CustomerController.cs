using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller // ALL USER ACCESS MUST ONLY ACCESS THE USERS DATA
    {
        private readonly IUnitOfWork _context;

        public CustomerController(IUnitOfWork context)
        {
            _context = context;
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _context.Customers.GetAllCustomers();
            return View("Index", customers);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomertById(int id)
        {
            var customer = _context.Customers.GetCustomerById(id);
            return Ok(customer);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (customer == null) { return BadRequest(); }
            _context.Customers.AddCustomer(customer);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = _context.Customers.GetCustomerById(id);
            if (existingCustomer == null) { return NotFound("Customer with the given ID does not exist"); }
            if (id != existingCustomer.Id) { return BadRequest("ID mismatch"); }
            _context.Customers.UpdateCustomer(customer);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _context.Customers.GetCustomerById(id);
            if (existingCustomer == null) { return NotFound("Customer with the given ID does not exist"); }
            _context.Customers.DeleteCustomer(existingCustomer);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("{id}/rentals")]
        public ActionResult<IEnumerable<Rental>> GetCustomerRentalHistoryById(int id)
        {
            var customerRentals = _context.Customers.GetCustomerRentalHistoryById(id);
            return View("Index", customerRentals);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("{id}/active-rental")]
        public ActionResult<IEnumerable<Rental>> GetCustomerActiveRentalsById(int id)
        {
            var customerActiveRentals = _context.Customers.GetCustomerActiveRentalsById(id);
            return View("Index", customerActiveRentals);
        }
    }
}

