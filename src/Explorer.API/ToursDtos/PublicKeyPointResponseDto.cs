﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ImagePath { get; set; }
        public long Order { get; set; }
        public string LocationAddress { get; set; }
    }
}