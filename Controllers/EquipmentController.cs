using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;
using Midterm_EquipmentRental.Services;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _context;
        
        public EquipmentController(IEquipmentService context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipment()
        {
            var equipments = _context.GetAllEquipment();
            return Ok(equipments);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Equipment> GetEquipmentById(int id)
        {
            var equipment = _context.GetEquipmentById(id);
            return Ok(equipment);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddEquipment(Equipment equipment)
        {
            if(equipment == null) { return BadRequest(); }
            _context.AddEquipment(equipment);
            return Ok();            
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateEquipment(int id, Equipment equipment)
        {
            var existingEquipment = _context.GetEquipmentById(id);
            if (existingEquipment == null) { return NotFound("Equipment with the given ID does not exist"); }
            if(id != existingEquipment.Id) { return BadRequest("ID mismatch"); }
            _context.UpdateEquipment(equipment);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteEquipment(int id)
        {
            var existingEquipment = _context.GetEquipmentById(id);
            if (existingEquipment == null) { return NotFound("Equipment with the given ID does not exist"); }
            _context.DeleteEquipment(existingEquipment);
            return Ok();
        }

        [Authorize]
        [HttpGet("available")]
        public ActionResult<IEnumerable<Equipment>> GetAllAvailableEquipment()
        {
            var equipments = _context.GetAllAvailableEquipment();
            return Ok(equipments);
        }

        [Authorize]
        [HttpGet("rented")]
        public ActionResult<IEnumerable<Equipment>> GetAllRentedEquipment()
        {
            var equipments = _context.GetAllRentedEquipment();
            return Ok(equipments);
        }
    }
}
