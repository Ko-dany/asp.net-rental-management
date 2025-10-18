using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _context;
        public RentalService(IUnitOfWork context)
        {
            _context = context;
        }
        public IEnumerable<Rental> GetAllRentals()
        {
            return _context.Rentals.GetAllRentals();
        }
        public Rental GetRentalById(int id)
        {
            return _context.Rentals.GetRentalById(id);
        }
        public void IssueEquipment(Rental rental)
        {
            _context.Rentals.IssueEquipment(rental);
            _context.Complete();
        }
        public void ReturnEquipment(Rental rental)
        {
            _context.Rentals.ReturnEquipment(rental);
            _context.Complete();
        }
        public IEnumerable<Rental> GetActiveRentals()
        {
            return _context.Rentals.GetActiveRentals();
        }
        public IEnumerable<Rental> GetCompletedRentals()
        {
            return _context.Rentals.GetCompletedRentals();
        }
        public IEnumerable<Rental> GetOverdueRentals()
        {
            return _context.Rentals.GetOverdueRentals();
        }
        public IEnumerable<Rental> GetRentalHistoryByEquipmentId(int id)
        {
            return _context.Rentals.GetRentalHistoryByEquipmentId(id);
        }
        public void ExtendRentalById(int id)
        {
            _context.Rentals.ExtendRentalById(id);
            _context.Complete();
        }
        public void CancelRentalById(int id)
        {
            _context.Rentals.CancelRentalById(id);
            _context.Complete();
        }
    }
}
