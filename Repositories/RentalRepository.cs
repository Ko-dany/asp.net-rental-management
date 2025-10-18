using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rental> GetAllRentals() { return _context.Rentals.ToList(); }

        public Rental GetRentalById(int id) { return _context.Rentals.Find(id); }

        public void IssueEquipment(Rental rental) { _context.Rentals.Add(rental); }

        public void ReturnEquipment(Rental rental)
        {
            Rental existingRental = GetRentalById(rental.Id);
            existingRental.EquipmentId = rental.Id;
            existingRental.CustomerId = rental.CustomerId;
            existingRental.IssuedAt = rental.IssuedAt;
            existingRental.ReturnedAt = rental.ReturnedAt;
            existingRental.Status = rental.Status;
        }

        public IEnumerable<Rental> GetActiveRentals()
        {
            return _context.Rentals.Where(r => r.Status == RentalStatus.Active);
        }

        public IEnumerable<Rental> GetCompletedRentals()
        {
            return _context.Rentals.Where(r => r.Status == RentalStatus.Returned);
        }

        public IEnumerable<Rental> GetOverdueRentals()
        {
            return _context.Rentals.Where(r => r.Status == RentalStatus.Overdue);
        }

        public IEnumerable<Rental> GetRentalHistoryByEquipmentId(int id)
        {
            return _context.Rentals.Where(r => r.EquipmentId == id);
        }

        public void ExtendRentalById(int id)
        {
            Rental rentalToExtend = GetRentalById(id);
            rentalToExtend.ReturnedAt.AddDays(5);
        }

        public void CancelRentalById(int id)
        {
            _context.Rentals.Remove(GetRentalById(id));
        }
    }
}
