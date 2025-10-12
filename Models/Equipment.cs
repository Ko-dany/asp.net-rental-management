namespace Midterm_EquipmentRental.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EquipmentCategory Category { get; set; }
        public EquipmentCondition Condition { get; set; }
        public double RentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
