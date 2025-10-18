using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : Controller
    {
        private readonly IUnitOfWork _context;

        public RentalController(IUnitOfWork context)
        {
            _context = context;
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetAllRentals()
        {
            var rentals = _context.Rentals.GetAllRentals();
            return View("Index", rentals);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("{id}")]
        public ActionResult<Rental> GetRentalById(int id)
        {
            var rental = _context.Rentals.GetRentalById(id);
            return Ok(rental);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpPost("issue")]
        public ActionResult IssueEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.Rentals.IssueEquipment(rental);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpPost("return")]
        public ActionResult ReturnEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.Rentals.ReturnEquipment(rental);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("active")]
        public ActionResult<IEnumerable<Rental>> GetActiveRentals()
        {
            var rentals = _context.Rentals.GetActiveRentals();
            return View("Index", rentals);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("completed")]
        public ActionResult<IEnumerable<Rental>> GetCompletedRentals()
        {
            var rentals = _context.Rentals.GetCompletedRentals();
            return View("Index", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("overdue")]
        public ActionResult<IEnumerable<Rental>> GetOverdueRentals()
        {
            var rentals = _context.Rentals.GetOverdueRentals();
            return View("Index", rentals);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("equipment/{id}")]
        public ActionResult<IEnumerable<Rental>> GetRentalHistoryByEquipmentId(int id)
        {
            var rentals = _context.Rentals.GetRentalHistoryByEquipmentId(id);
            return View("Index", rentals);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut]
        public ActionResult ExtendRentalById(int id)
        {
            _context.Rentals.ExtendRentalById(id);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete]
        public ActionResult CancelRentalById(int id)
        {
            _context.Rentals.CancelRentalById(id);
            _context.Complete();
            return Ok();
        }
    }
}
