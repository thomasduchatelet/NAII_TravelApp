using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TravelApp.Backend.Data
{
    public class DataSeed
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;


        public DataSeed(AppDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        public async Task InitializeData()
        {
            //_context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                Category clothes = new Category("Clothes");
                Category toiletries = new Category("Toiletries");
                Category miscellaneous = new Category("Miscellaneous");
                Category tickets = new Category("Tickets");
                Category research = new Category("Research");

                Item underwear = new Item("Underwear", 10, clothes);
                Item socks = new Item("Socks", 10, clothes);
                Item shirts = new Item("Shirts", 6, clothes);
                Item pants = new Item("Pants", 4, clothes);

                Item tootbrush = new Item("Tootbrush", 1, toiletries);
                Item sunscreen = new Item("Sunscreen", 1, toiletries);

                Item camera = new Item("Camera", 1, miscellaneous);
                Item visum = new Item("Visum", 1, miscellaneous);


                Todo todo1 = new Todo("Book plane tickets", tickets);
                Todo todo2 = new Todo("Find nice restaurants", research);
                Todo todo3 = new Todo("Look for nearby concerts", research);

                Itinerary itinerary = new Itinerary("Route");
                Location ghent = new Location("Ghent", 51.0543422, 3.7174243);
                Location berlin = new Location("Berlin", 52.5200066, 13.404954);
                Location prague = new Location("Prague", 50.0755381, 14.4378005);
                itinerary.AddLocation(ghent, 0);
                itinerary.AddLocation(prague, 1);
                itinerary.AddLocation(berlin, 1);
                List<Item> items = new List<Item> { underwear, socks, shirts, pants, tootbrush, sunscreen, camera, visum };
                List<Todo> todos = new List<Todo> { todo1, todo2, todo3 };

                Trip trip = new Trip("Prague - Summer 2021", new DateTime(2021, 7, 20), new DateTime(2021,8,3),itinerary,items,todos);

                User user = new User
                {
                    UserName = "jdoe",
                    Email = "jdoe@mail.com",
                    Trips = new List<Trip> { trip},
                    Items = items,
                    Todos = todos,
                    Itineraries = new List<Itinerary> { itinerary },
                    Locations = new List<Location> {ghent, berlin, prague },
                    Categories = new List<Category> { clothes, toiletries, miscellaneous, tickets, research}
                };

                //var countries = JsonConvert.DeserializeObject<List<Country>>(COUNTRY_JSON);
                //_context.Countries.AddRange(countries);
                await _userManager.CreateAsync(user, "123aze");
                await _context.SaveChangesAsync();



            }

        }
    }
}
