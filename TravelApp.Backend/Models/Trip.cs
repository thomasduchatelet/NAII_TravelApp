using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public class Trip : TravelAppClass
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate {get; set; }
        public string Title { get; set; }
        public virtual Itinerary Itinerary { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<Todo> Todos { get; set; }


        public Trip()
        {
            Items = new List<Item>();
            Todos = new List<Todo>();
        }
        public Trip(string name, DateTime start, DateTime end, Itinerary itinerary, List<Item> items, List<Todo> todos)
        {
            Title = name;
            StartDate = start;
            EndDate = end;
            Itinerary = itinerary;
            Items = items;
            Todos = todos;
        }
    }
}
