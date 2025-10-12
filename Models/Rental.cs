namespace Midterm_EquipmentRental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int CustomerId { get; set; }
        public int IssuedAt { get; set; }
        public int ReturnedAt { get; set; }

    }
}
