using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _context;

        public UserService(IUnitOfWork context)
        {
            _context = context;
        }
    }
}
