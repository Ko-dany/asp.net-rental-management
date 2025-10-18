using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Services
{
    public interface IAuthService
    {
        Customer? ValidateCredentials(LoginRequest loginRequest);
        string GenerateToken(Customer customer);
    }
}
