using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Repositories
{
    public interface ICustomerRepository
    {
        DbSet<Customer> GetCustomerDB();
    }
}
