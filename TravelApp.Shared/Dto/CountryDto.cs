﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class CountryDto : BaseDto
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int Cases { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
