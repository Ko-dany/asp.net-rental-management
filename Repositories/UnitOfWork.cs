using Midterm_EquipmentRental.Data;

namespace Midterm_EquipmentRental.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IEquipmentRepository Equipments { get; }
        public ICustomerRepository Customers{ get; }

        public UnitOfWork(AppDbContext context, IEquipmentRepository equipments, ICustomerRepository customers)
        {
            _context = context;
            Equipments = equipments;
            Customers = customers;
        }

        public int Complete() { return _context.SaveChanges(); }
    }
}
