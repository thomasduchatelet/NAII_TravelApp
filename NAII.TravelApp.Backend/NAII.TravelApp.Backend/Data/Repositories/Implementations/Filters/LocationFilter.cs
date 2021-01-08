using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class LocationFilter : FilterBase<Location>
    {
        public long? ItineraryId { get; set; }
        public override IEnumerable<Location> FilterAfterUsersFiltered(IEnumerable<Location> input)
        {
            if (ItineraryId.HasValue)
                input = input.Where(i => i.ItineraryId == ItineraryId.Value);
            return input;
        }
    }
}
