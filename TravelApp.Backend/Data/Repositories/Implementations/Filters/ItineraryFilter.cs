using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class ItineraryFilter : FilterBase<Itinerary>
    {
        public long? TripId { get; set; }
        public override IEnumerable<Itinerary> FilterAfterUsersFiltered(IEnumerable<Itinerary> input)
        {
            if (TripId.HasValue)
                input = input.Where(i => i.TripId == TripId.Value);
            return input;
        }
    }
}
