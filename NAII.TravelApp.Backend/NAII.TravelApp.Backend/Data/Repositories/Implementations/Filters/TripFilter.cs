using NAII.TravelApp.Backend.Data.Repositories.Interfaces;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class TripFilter : FilterBase<Trip>
    {
        public string Title { get; set; }
        public DateTime? StartsAfter { get; set; }
        public DateTime? StartsBefore { get; set; }
        public override IEnumerable<Trip> FilterAfterUsersFiltered(IEnumerable<Trip> input)
        {
            if (Title != null || Title != "")
                input = input.Where(t => t.Title.Contains(Title));
            if (StartsAfter.HasValue)
                input = input.Where(t => t.StartDate.Ticks >= StartsAfter.Value.Ticks);
            if (StartsBefore.HasValue)
                input = input.Where(t => t.StartDate.Ticks <= StartsBefore.Value.Ticks);

            return input;
        }
    }
}
