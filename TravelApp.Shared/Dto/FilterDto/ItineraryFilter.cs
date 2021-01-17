using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class ItineraryFilter : BaseFilterDto
    {
        public long? TripId { get; set; }
    }
}
