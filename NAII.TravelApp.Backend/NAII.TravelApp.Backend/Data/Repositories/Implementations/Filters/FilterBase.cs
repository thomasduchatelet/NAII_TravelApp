using NAII.TravelApp.Backend.Data.Repositories.Interfaces;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public abstract class FilterBase<T> : IFilter<T> where T : TravelAppClass
    {
        public long? Id { get; set; }
        public abstract IEnumerable<T> FilterAfterUsersFiltered(IEnumerable<T> input);
        public IEnumerable<T> Filter(IEnumerable<T> input, string userId)
        {
            input = input.Where(i => i.UserId == userId);
            if (Id != null)
                input = input.Where(i => i.Id == Id);
            return FilterAfterUsersFiltered(input);
        }
    }
}
