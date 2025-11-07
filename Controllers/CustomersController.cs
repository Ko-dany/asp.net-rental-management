using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Services;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _context;

        public CustomersController(ICustomerService context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _context.GetAllCustomers();
            return Ok(customers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _context.GetCustomerById(id);
            return Ok(customer);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (customer == null) { return BadRequest(); }
            _context.AddCustomer(customer);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = _context.GetCustomerById(id);
            if (existingCustomer == null) { return NotFound("Customer with the given ID does not exist"); }
            if (id != existingCustomer.Id) { return BadRequest("ID mismatch"); }
            _context.UpdateCustomer(customer);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _context.GetCustomerById(id);
            if (existingCustomer == null) { return NotFound("Customer with the given ID does not exist"); }
            _context.DeleteCustomer(existingCustomer);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}/rentals")]
        public ActionResult<IEnumerable<Rental>?> GetCustomerRentalHistoryById(int id)
        {
            var customerRentals = _context.GetCustomerRentalHistoryById(id);
            return Ok(customerRentals);
        }

        [Authorize]
        [HttpGet("{id}/active-rental")]
        public ActionResult<IEnumerable<Rental>?> GetCustomerActiveRentalsById(int id)
        {
            var customerActiveRentals = _context.GetCustomerActiveRentalsById(id);
            return Ok(customerActiveRentals);
        }
    }
}

