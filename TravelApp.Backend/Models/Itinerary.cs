using System.Collections.Generic;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public class Itinerary : TravelAppClass
    {
        public string Name { get; set; }
        public virtual List<Location> Locations { get; set; }
        public long TripId { get; set; }

        protected Itinerary()
        {
            Locations = new List<Location>();
        }

        public Itinerary(string name)
        {
            Name = name;
            Locations = new List<Location>();
        }

        public void AddLocation(Location location, int position)
        {
            Locations = Locations.OrderBy(l => l.Order).ToList();
            if (Locations.Count == 0)
                location.Order = 10;
            else if (position <= 0)
                location.Order = Locations[0].Order / 2;
            else if (position >= Locations.Count)
                location.Order = Locations.Max(l => l.Order) + 10;
            else
                location.Order = (Locations[position - 1].Order + Locations[position].Order) / 2;

            Locations.Add(location);
            Locations = Locations.OrderBy(l => l.Order).ToList();
            for (int i = 0; i < Locations.Count; i++)
                Locations[i].Order = 10 * (i + 1);
        }
        
    }
}
