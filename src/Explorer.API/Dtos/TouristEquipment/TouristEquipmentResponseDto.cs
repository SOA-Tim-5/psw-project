namespace Explorer.Tours.API.Dtos.TouristEquipment
{
    public class TouristEquipmentResponseDto
    {
        public string Id { get; set; }
        public int TouristId { get; set; }
        public List<string>? EquipmentIds { get; set; }
    }
}
