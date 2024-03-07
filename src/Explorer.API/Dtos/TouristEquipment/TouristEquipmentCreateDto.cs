namespace Explorer.Stakeholders.API.Dtos.TouristEquipment
{
    public class TouristEquipmentCreateDto
    {
        public int TouristId { get; set; }
        public List<string>? EquipmentIds { get; set; }
    }
}
