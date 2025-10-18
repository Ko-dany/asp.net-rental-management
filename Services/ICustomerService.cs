using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        IEnumerable<Rental> GetCustomerRentalHistoryById(int id);
        IEnumerable<Rental> GetCustomerActiveRentalsById(int id);
    }
}
