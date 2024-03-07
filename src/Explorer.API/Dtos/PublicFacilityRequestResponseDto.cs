using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Tours.API.Dtos
{
    public class PublicFacilityRequestResponseDto
    {
        public string Id { get; set; }
        public string FacilityId { get; set; }
        public PublicStatus Status { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
        public string AuthorName { get; set; }
        public string? FacilityName { get; set; }
        public UserResponseDto Author { get; set; }
    }
}
