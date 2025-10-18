using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _context;
        public EquipmentService(IUnitOfWork context)
        {
            _context = context;
        }

        public IEnumerable<Equipment> GetAllEquipment()
        {
            return _context.Equipments.GetAllEquipment();
        }

        public Equipment GetEquipmentById(int id)
        {
            return _context.Equipments.GetEquipmentById(id);
        }

        public void AddEquipment(Equipment equipment)
        {
            _context.Equipments.AddEquipment(equipment);
            _context.Complete();
        }

        public void UpdateEquipment(Equipment equipment)
        {
            _context.Equipments.UpdateEquipment(equipment);
            _context.Complete();
        }

        public void DeleteEquipment(Equipment equipment)
        {
            _context.Equipments.DeleteEquipment(equipment);
            _context.Complete();
        }

        public IEnumerable<Equipment> GetAllAvailableEquipment()
        {
            return _context.Equipments.GetAllAvailableEquipment();
        }

        public IEnumerable<Equipment> GetAllRentedEquipment()
        {
            return _context.Equipments.GetAllRentedEquipment();
        }
    }
}
