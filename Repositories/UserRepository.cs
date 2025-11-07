using Midterm_EquipmentRental.Data;

namespace Midterm_EquipmentRental.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
