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
    }
}
