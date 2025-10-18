using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Data;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<Customer> GetCustomerDB() { return _context.Customers; }

        public IEnumerable<Customer> GetAllCustomers() { return _context.Customers.ToList(); }

        public Customer GetCustomerById(int id) { return _context.Customers.Find(id); }

        public void AddCustomer(Customer customer) { _context.Customers.Add(customer); }

        public void UpdateCustomer(Customer customer)
        {
            Customer existingCustomer = GetCustomerById(customer.Id);
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Password = customer.Password;  // AVOID ??
            existingCustomer.Role = customer.Role;          // AVOID ??
            existingCustomer.RentalHistory = customer.RentalHistory;
        }

        public void DeleteCustomer(Customer customer) { _context.Customers.Remove(customer); }

        public IEnumerable<Rental> GetCustomerRentalHistoryById(int id)
        {
            Customer customer = _context.Customers.Find(id);
            return customer.RentalHistory;
        }

        public IEnumerable<Rental> GetCustomerActiveRentalsById(int id)
        {
            Customer customer = _context.Customers.Find(id);
            return customer.RentalHistory.Where(r => r.Status == RentalStatus.Active);
        }
    }
}
