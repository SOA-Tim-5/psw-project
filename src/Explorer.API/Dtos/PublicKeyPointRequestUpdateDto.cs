using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointRequestUpdateDto
    {
        public string Id { get; set; }
        public string KeyPointId { get; set; }
        public PublicStatus Status { get; set; }
        public string Comment { get; set; }
        // public KeyPointDto KeyPoint { get; set; }
        public string AuthorName { get; set; }
    }
}

