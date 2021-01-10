using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;

namespace TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class CountryFilter : IFilter<Country>
    {
        public long? Id { get; set; }
        public IEnumerable<Country> Filter(IEnumerable<Country> input, string userId)
        {
            if (Id.HasValue)
                input = input.Where(i => i.Id == Id.Value);
            return input;
        }
    }
}
