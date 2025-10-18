using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public RentalController(IUnitOfWork context)
        {
            _context = context;
        }
    }
}
