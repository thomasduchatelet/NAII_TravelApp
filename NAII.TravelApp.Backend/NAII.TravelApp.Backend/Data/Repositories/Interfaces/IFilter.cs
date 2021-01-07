using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Interfaces
{
    public interface IFilter<T> where T : TravelAppClass
    {
        IEnumerable<T> Filter(IEnumerable<T> input, string userId);
    }
}
