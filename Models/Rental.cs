namespace Midterm_EquipmentRental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int CustomerId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ReturnedAt { get; set; }

    }
}
