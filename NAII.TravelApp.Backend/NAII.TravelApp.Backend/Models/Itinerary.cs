using System.Collections.Generic;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Itinerary
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Location> Locations { get; set; }
        public long TripId { get; set; }

    }
}
