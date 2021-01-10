using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class CountryDto : BaseDto
    {
        public string Name { get; set; }
        public string Latest { get; set; }
        public string History { get; set; }
    }
}
