﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicFacilityRequestCreateDto
    {
        public string Id { get; set; }
        public long AuthorId {  get; set; }
        public string FacilityId { get; set; }
        public PublicStatus Status { get; set; }
        public DateTime? Created { get; set; }
        public string AuthorName { get; set; }
    }
}

