using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CampaignCreateDto
    {
        public long TouristId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> TourIds { get; set; }
    }
}
