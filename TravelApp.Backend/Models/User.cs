using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public List<Trip> Trips { get; set; }
        [PersonalData]
        public List<Category> Categories { get; set; }
        [PersonalData]
        public List<Item> Items { get; set; }
        [PersonalData]
        public List<Todo> Todos { get; set; }
        [PersonalData]
        public List<Itinerary> Itineraries { get; set; }
        [PersonalData]
        public List<Location> Locations { get; set; }


    }
}
