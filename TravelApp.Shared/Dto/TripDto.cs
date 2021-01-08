using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class TripDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
    }

    public class GetTripDto : TripDto
    {
        public ItineraryDto Itinerary { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<TodoDto> Todos { get; set; }
    }
}
