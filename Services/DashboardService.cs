using Midterm_EquipmentRental.Models.ViewModels;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _context;
        public DashboardService(IUnitOfWork context)
        {
            _context = context;
        }
        public DashboardViewModel GetDashboardInfo()
        {
            return new DashboardViewModel()
            {
                TotalEquipment = _context.Equipments.GetAllEquipment().Count(),
                AvailableCount = _context.Equipments.GetAllAvailableEquipment().Count(),
                RentedCount = _context.Equipments.GetAllRentedEquipment().Count(),
                OverdueCount = 0,
                TotalCustomers = _context.Customers.GetAllCustomers().Count(),
                ActiveRentals = _context.Rentals.GetActiveRentals().Count(),
                OverdueRentals = _context.Rentals.GetOverdueRentals().Count(),
            };
        }
    }
}
