using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Equipment> GetAllEquipment() { return _context.Equipments.ToList(); }

        public Equipment GetEquipmentById(int id) { return _context.Equipments.Find(id); }

        public void AddEquipment(Equipment equipment) { _context.Equipments.Add(equipment); }

        public void UpdateEquipment(Equipment equipment)
        {
            Equipment existingEquipment = GetEquipmentById(equipment.Id);
            existingEquipment.Name = equipment.Name;
            existingEquipment.Description = equipment.Description;
            existingEquipment.Category = equipment.Category;
            existingEquipment.Condition = equipment.Condition;
            existingEquipment.RentalPrice = equipment.RentalPrice;
        }

        public void DeleteEquipment(Equipment equipment) { _context.Equipments.Remove(equipment); }

        public IEnumerable<Equipment> GetAllAvailableEquipment() { return _context.Equipments.Where(e => e.IsAvailable == true).ToList(); }

        public IEnumerable<Equipment> GetAllRentedEquipment() { return _context.Equipments.Where(e => e.IsAvailable == false).ToList(); }
    }
}
