namespace Midterm_EquipmentRental.Models.ViewModels
{
    public class EquipmentEditViewModel
    {
        public int Id { get; set; }                      // shown read-only + posted via hidden field
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";       // keep text to match Create view
        public EquipmentCondition Condition { get; set; } = EquipmentCondition.New;
        public bool IsAvailable { get; set; } = false;
    }
}
