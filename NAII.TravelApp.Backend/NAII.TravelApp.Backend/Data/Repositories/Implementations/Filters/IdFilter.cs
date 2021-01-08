using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class IdFilter<T> : FilterBase<T> where T : TravelAppClass
    {
        public override IEnumerable<T> FilterAfterUsersFiltered(IEnumerable<T> input)
        {
            return input;
        }
    }
}
