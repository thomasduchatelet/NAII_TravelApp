using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;

namespace TravelApp.Backend.Models
{
    public class Country : TravelAppClass
    {
        public string Name { get; set; }
        public string Latest { get; set; }
        public string History { get; set; }
    }
}
