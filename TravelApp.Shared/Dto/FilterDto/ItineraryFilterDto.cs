using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class ItineraryFilterDto : BaseFilterDto
    {
        public long? TripId { get; set; }
    }
}
