using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class LocationFilterDto : BaseFilterDto
    {
        public long? ItineraryId { get; set; }
    }
}
