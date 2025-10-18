using Midterm_EquipmentRental.Models;
using Midterm_EquipmentRental.Repositories;

namespace Midterm_EquipmentRental.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _context;

        public CustomerService(IUnitOfWork context)
        {
            _context = context;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.GetAllCustomers();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.GetCustomerById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.AddCustomer(customer);
            _context.Complete();
        }
        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.UpdateCustomer(customer);
            _context.Complete();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.DeleteCustomer(customer);
            _context.Complete();
        }
        public IEnumerable<Rental> GetCustomerRentalHistoryById(int id)
        {
            return _context.Customers.GetCustomerRentalHistoryById(id);
        }

        public IEnumerable<Rental> GetCustomerActiveRentalsById(int id)
        {
            return _context.Customers.GetCustomerActiveRentalsById(id);
        }

    }
}
