using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public interface ICustomerRepository
    {
        DbSet<Customer> GetCustomerDB();
        public IEnumerable<Customer> GetAllCustomers();
        public Customer GetCustomerById(int id);
        public void AddCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public void DeleteCustomer(Customer customer);
        public IEnumerable<Rental> GetCustomerRentalHistoryById(int id);
        public IEnumerable<Rental> GetCustomerActiveRentalsById(int id);
    }
}
