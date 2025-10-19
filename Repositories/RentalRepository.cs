using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Rental> GetRentalsByCustomerId(int customerId)
        {
            return _context.Rentals
                .Where(r => r.CustomerId == customerId).ToList();
        }

        public Rental GetRentalById(int id) { return _context.Rentals.Find(id); }

        public void IssueEquipment(Rental rental) { _context.Rentals.Add(rental); }

        public void ReturnEquipment(Rental rental)
        {
            Rental existingRental = GetRentalById(rental.Id);
            existingRental.EquipmentId = rental.Id;
            existingRental.ReturnedAt = DateTime.Now;
            existingRental.Status = RentalStatus.Returned;
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

        public void ExtendRentalById(int id, DateTime newDate)
        {
            Rental rentalToExtend = GetRentalById(id);
            rentalToExtend.ReturnedAt = newDate;
            rentalToExtend.Status = RentalStatus.Active;
        }

        public void CancelRentalById(int id)
        {
            _context.Rentals.Remove(GetRentalById(id));
        }
    }
}
