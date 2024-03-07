using Explorer.Tours.API.Dtos;

namespace Explorer.API.Dtos
{
    public class CampaignResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public double AverageDifficulty { get; set; }
        public ICollection<EquipmentResponseDto> Equipments { get; set; }
        public ICollection<KeyPointResponseDto> KeyPoints { get; set; }
    }
}
