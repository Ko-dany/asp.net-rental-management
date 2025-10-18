using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Services
{
    public interface IEquipmentService
    {
        IEnumerable<Equipment> GetAllEquipment();
        Equipment GetEquipmentById(int id);
        void AddEquipment(Equipment equipment);
        void UpdateEquipment(Equipment equipment);
        void DeleteEquipment(Equipment equipment);
        IEnumerable<Equipment> GetAllAvailableEquipment();
        IEnumerable<Equipment> GetAllRentedEquipment();
    }
}
