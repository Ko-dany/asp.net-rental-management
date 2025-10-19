namespace Midterm_EquipmentRental.Models.ViewModels
{
    public class RentalDetailsViewModel
    {
        public Rental Rental { get; set; } = new Rental();

        // Optional display data (use if you have them; fall back to IDs in the view)
        public string? CustomerName { get; set; }
        public string? CustomerUserName { get; set; }
        public string? EquipmentName { get; set; }
    }
}
