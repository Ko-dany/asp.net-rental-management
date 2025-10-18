using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IEquipmentRepository Equipments { get; }
        public ICustomerRepository Customers{ get; }
        public IRentalRepository Rentals { get; }

        public UnitOfWork(AppDbContext context, IEquipmentRepository equipments, ICustomerRepository customers, IRentalRepository rentals)
        {
            _context = context;
            Equipments = equipments;
            Customers = customers;
            Rentals = rentals;
        }

        public int Complete() { return _context.SaveChanges(); }
    }
}
