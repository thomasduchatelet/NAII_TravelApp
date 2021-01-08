using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ItineraryDto : BaseDto
    {
        public string Name { get; set; }
        public List<LocationDto> Locations { get; set; }
    }

    public class CreateItineraryDto : ItineraryDto
    {
        public long TripId { get; set; }
    }
}
