using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;
using Midterm_EquipmentRental.Services;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : Controller
    {
        private readonly IRentalService _context;

        public RentalsController(IRentalService context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetAllRentals()
        {
            var rentals = _context.GetAllRentals();
            return Ok(rentals);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Rental> GetRentalById(int id)
        {
            var rental = _context.GetRentalById(id);
            return Ok(rental);
        }

        [Authorize]
        [HttpPost("issue")]
        public ActionResult IssueEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.IssueEquipment(rental);
            return Ok();
        }

        [Authorize]
        [HttpPost("return")]
        public ActionResult ReturnEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.ReturnEquipment(rental);
            return Ok();
        }

        [Authorize]
        [HttpGet("active")]
        public ActionResult<IEnumerable<Rental>> GetActiveRentals()
        {
            var rentals = _context.GetActiveRentals();
            return Ok(rentals);
        }

        [Authorize]
        [HttpGet("completed")]
        public ActionResult<IEnumerable<Rental>> GetCompletedRentals()
        {
            var rentals = _context.GetCompletedRentals();
            return Ok(rentals);
        }

        [Authorize]
        [HttpGet("overdue")]
        public ActionResult<IEnumerable<Rental>> GetOverdueRentals()
        {
            var rentals = _context.GetOverdueRentals();
            return Ok(rentals);
        }

        [Authorize]
        [HttpGet("equipment/{id}")]
        public ActionResult<IEnumerable<Rental>> GetRentalHistoryByEquipmentId(int id)
        {
            var rentals = _context.GetRentalHistoryByEquipmentId(id);
            return Ok(rentals);
        }

        [Authorize]
        [HttpPut]
        public ActionResult ExtendRentalById(int id, DateTime newDate)
        {
            _context.ExtendRentalById(id, newDate);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult CancelRentalById(int id)
        {
            _context.CancelRentalById(id);
            return Ok();
        }
    }
}
