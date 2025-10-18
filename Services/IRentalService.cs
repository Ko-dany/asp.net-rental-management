using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Services
{
    public interface IRentalService
    {
        IEnumerable<Rental> GetAllRentals();
        Rental GetRentalById(int id);
        void IssueEquipment(Rental rental);
        void ReturnEquipment(Rental rental);
        IEnumerable<Rental> GetActiveRentals();
        IEnumerable<Rental> GetCompletedRentals();
        IEnumerable<Rental> GetOverdueRentals();
        IEnumerable<Rental> GetRentalHistoryByEquipmentId(int id);
        void ExtendRentalById(int id);
        void CancelRentalById(int id);
    }
}
