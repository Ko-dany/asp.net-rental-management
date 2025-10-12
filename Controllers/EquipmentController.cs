using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipment()
        {
            return _context.Equipments.ToList();
        }
    }
}
