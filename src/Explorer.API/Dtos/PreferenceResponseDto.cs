﻿

namespace Explorer.Tours.API.Dtos
{
    public class PreferenceResponseDto
    {
        public string Id { get; set; }
        public long UserId { get; set; }
        public int DifficultyLevel {  get; set; }
        public int WalkingRating { get; set; }
        public int CyclingRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> SelectedTags { get; set; }
    }
}   