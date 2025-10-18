namespace Midterm_EquipmentRental.Models.ViewModels
{
    public class EquipmentListViewModel
    {
        public IEnumerable<Equipment> Items { get; set; } = Enumerable.Empty<Equipment>();
        /// <summary>Accepted values: "All" | "Available" | "Rented"</summary>
        public string Filter { get; set; } = "All";
        public bool IsAdmin { get; set; } = false;
    }
}
