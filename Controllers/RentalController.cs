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

        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetAllRentals()
        {
            var rentals = _context.Rentals.GetAllRentals();
            return View("Index", rentals);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Rental> GetRentalById(int id)
        {
            var rental = _context.Rentals.GetRentalById(id);
            return Ok(rental);
        }

        //[Authorize]
        [HttpPost("issue")]
        public ActionResult IssueEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.Rentals.IssueEquipment(rental);
            _context.Complete();
            return Ok();
        }

        //[Authorize]
        [HttpPost("return")]
        public ActionResult ReturnEquipment(Rental rental)
        {
            if (rental == null) { return BadRequest(); }
            _context.Rentals.ReturnEquipment(rental);
            _context.Complete();
            return Ok();
        }

        //[Authorize]
        [HttpGet("active")]
        public ActionResult<IEnumerable<Rental>> GetActiveRentals()
        {
            var rentals = _context.Rentals.GetActiveRentals();
            return View("Index", rentals);
        }

        //[Authorize]
        [HttpGet("completed")]
        public ActionResult<IEnumerable<Rental>> GetCompletedRentals()
        {
            var rentals = _context.Rentals.GetCompletedRentals();
            return View("Index", rentals);
        }

        //[Authorize]
        [HttpGet("overdue")]
        public ActionResult<IEnumerable<Rental>> GetOverdueRentals()
        {
            var rentals = _context.Rentals.GetOverdueRentals();
            return View("Index", rentals);
        }

        //[Authorize]
        [HttpGet("equipment/{id}")]
        public ActionResult<IEnumerable<Rental>> GetRentalHistoryByEquipmentId(int id)
        {
            var rentals = _context.Rentals.GetRentalHistoryByEquipmentId(id);
            return View("Index", rentals);
        }

        //[Authorize]
        [HttpPut]
        public ActionResult ExtendRentalById(int id)
        {
            _context.Rentals.ExtendRentalById(id);
            _context.Complete();
            return Ok();
        }

        //[Authorize]
        [HttpDelete]
        public ActionResult CancelRentalById(int id)
        {
            _context.Rentals.CancelRentalById(id);
            _context.Complete();
            return Ok();
        }
    }
}
