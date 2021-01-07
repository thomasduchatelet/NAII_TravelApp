using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Trip
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate {get; set; }
        public string Title { get; set; }
        public Itinerary Itinerary { get; set; }
        public List<Item> Items { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
