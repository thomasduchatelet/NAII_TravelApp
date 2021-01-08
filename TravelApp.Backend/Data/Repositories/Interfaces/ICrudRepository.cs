using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TravelApp.Backend.Data.Repositories.Interfaces
{
    public interface ICrudRepository<T, I> where T : TravelAppClass where I : IFilter<T>
    {
        IEnumerable<T> GetAll(I filter, string userId);
        T Create(T input, string userId);
        T Update(T input, string userId);
        bool Delete(long id, string userId);

        IEnumerable<T> GetAllEager(I Filter, string userId);

        void SaveChanges();

    }
}
