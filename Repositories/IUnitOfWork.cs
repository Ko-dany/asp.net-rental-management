namespace Midterm_EquipmentRental.Repositories
{
    public interface IUnitOfWork
    {
        public IEquipmentRepository Equipments { get; }
        public ICustomerRepository Customers { get; }
        public int Complete();
    }
}
