namespace Midterm_EquipmentRental.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Cards
        public int TotalEquipment { get; set; }
        public int AvailableCount { get; set; }
        public int RentedCount { get; set; }
        public int OverdueCount { get; set; }
        public int TotalCustomers { get; set; }

        // System Status panel
        public int ActiveRentals { get; set; }
        public int OverdueRentals { get; set; }

        public string UserName { get; set; } = "";
        public string RoleName { get; set; } = "User";
        public bool IsAdmin { get; set; } = false;

        public string SystemStatus { get; set; } = "Online"; // Online/Degraded/Offline
    }
}
