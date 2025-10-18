using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private readonly IUnitOfWork _context;
        
        public EquipmentController(IUnitOfWork context)
        {
            _context = context;
        }

        [Authorize (Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipment()
        {
            var equipments = _context.Equipments.GetAllEquipment();
            ViewBag.Status = "All";
            return View("Index", equipments);
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("{id}")]
        public ActionResult<Equipment> GetEquipmentById(int id)
        {
            var equipment = _context.Equipments.GetEquipmentById(id);
            return Ok(equipment);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult AddEquipment(Equipment equipment)
        {
            if(equipment == null) { return BadRequest(); }
            _context.Equipments.AddEquipment(equipment);
            _context.Complete();
            return Ok();            
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("{id}")]
        public ActionResult UpdateEquipment(int id, Equipment equipment)
        {
            var existingEquipment = _context.Equipments.GetEquipmentById(id);
            if (existingEquipment == null) { return NotFound("Equipment with the given ID does not exist"); }
            if(id != existingEquipment.Id) { return BadRequest("ID mismatch"); }
            _context.Equipments.UpdateEquipment(equipment);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public ActionResult DeleteEquipment(int id)
        {
            var existingEquipment = _context.Equipments.GetEquipmentById(id);
            if (existingEquipment == null) { return NotFound("Equipment with the given ID does not exist"); }
            _context.Equipments.DeleteEquipment(existingEquipment);
            _context.Complete();
            return Ok();
        }

        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [HttpGet("available")]
        public ActionResult<IEnumerable<Equipment>> GetAllAvailableEquipment()
        {
            var equipments = _context.Equipments.GetAllAvailableEquipment();
            ViewBag.Status = "Available";
            return View("Index", equipments);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("rented")]
        public ActionResult<IEnumerable<Equipment>> GetAllRentedEquipment()
        {
            var equipments = _context.Equipments.GetAllRentedEquipment();
            ViewBag.Status = "Rented";
            return View("Index", equipments);
        }
    }
}
