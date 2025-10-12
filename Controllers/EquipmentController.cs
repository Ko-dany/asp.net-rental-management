using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        
        public EquipmentController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipment()
        {
            var equipments = _context.Equipments.GetAllEquipment();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public ActionResult<Equipment> GetEquipmentById(int id)
        {
            var equipment = _context.Equipments.GetEquipmentById(id);
            return Ok(equipment);
        }

        [HttpPost]
        public ActionResult AddEquipment(Equipment equipment)
        {
            if(equipment == null) { return BadRequest(); }
            _context.Equipments.AddEquipment(equipment);
            _context.Complete();
            return Ok();            
        }

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

        [HttpDelete("{id}")]
        public ActionResult DeleteEquipment(int id)
        {
            var existingEquipment = _context.Equipments.GetEquipmentById(id);
            if (existingEquipment == null) { return NotFound("Equipment with the given ID does not exist"); }
            _context.Equipments.DeleteEquipment(existingEquipment);
            _context.Complete();
            return Ok();
        }

        [HttpGet("available")]
        public ActionResult<IEnumerable<Equipment>> GetAllAvailableEquipment()
        {
            var equipments = _context.Equipments.GetAllAvailableEquipment();
            return Ok(equipments);
        }

        [HttpGet("rented")]
        public ActionResult<IEnumerable<Equipment>> GetAllRentedEquipment()
        {
            var equipments = _context.Equipments.GetAllRentedEquipment();
            return Ok(equipments);
        }
    }
}
