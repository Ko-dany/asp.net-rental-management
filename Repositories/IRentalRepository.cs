using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetAllRentals();
        public Rental GetRentalById(int id);
        public void IssueEquipment(Rental rental);
        public void ReturnEquipment(Rental rental);
        public IEnumerable<Rental> GetActiveRentals();
        public IEnumerable<Rental> GetCompletedRentals();
        public IEnumerable<Rental> GetOverdueRentals();
        public IEnumerable<Rental> GetRentalHistoryByEquipmentId(int id);
        public void ExtendRentalById(int id, DateTime newDate);
        public void CancelRentalById(int id);
    }
}
