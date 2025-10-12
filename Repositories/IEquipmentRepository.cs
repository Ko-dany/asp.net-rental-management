using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public interface IEquipmentRepository
    {
        public IEnumerable<Equipment> GetAllEquipment();
        public Equipment GetEquipmentById(int id);
        public void AddEquipment(Equipment equipment);
        public void UpdateEquipment(Equipment equipment);
        public void DeleteEquipment(Equipment equipment);
        public IEnumerable<Equipment> GetAllAvailableEquipment();
        public IEnumerable<Equipment> GetAllRentedEquipment();
    }
}
