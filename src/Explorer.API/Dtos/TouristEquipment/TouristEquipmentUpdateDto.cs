namespace Explorer.Stakeholders.API.Dtos.TouristEquipment
{
    public class TouristEquipmentUpdateDto
    {
        public string Id { get; set; }
        public int TouristId { get; set; }
        public List<string> EquipmentIds { get; set; }
    }
}
