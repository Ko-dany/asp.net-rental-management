namespace Midterm_EquipmentRental.Models.ViewModels
{
    public class EquipmentCreateViewModel
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = ""; // free text to match screenshot
        public EquipmentCondition Condition { get; set; } = EquipmentCondition.New;
        public bool IsAvailable { get; set; } = false;
    }
}
