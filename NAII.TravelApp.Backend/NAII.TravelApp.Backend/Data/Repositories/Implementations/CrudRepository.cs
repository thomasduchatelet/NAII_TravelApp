using Microsoft.EntityFrameworkCore;
using NAII.TravelApp.Backend.Data.Repositories.Interfaces;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Data.Repositories.Implementations
{
    public class CrudRepository<T, I> : ICrudRepository<T, I> where I : IFilter<T> where T : TravelAppClass
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public CrudRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual T Create(T input, string userId)
        {
            input.UserId = userId;
            _dbSet.Add(input);
            _context.SaveChanges();
            return input;
        }

        public virtual void Delete(long id, string userId)
        {
            var t = _dbSet.SingleOrDefault(c => c.Id == id && c.UserId == userId);
            _dbSet.Remove(t);
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll(I filter, string userId)
        {
            return filter.Filter(_dbSet, userId);
        }

        public virtual T Update(T input, string userId)
        {
            input.UserId = userId;
            var t = _dbSet.SingleOrDefault(c => c.UserId == userId && c.Id == input.Id);
            if (t != null)
            {
                _dbSet.Update(input);
                _context.SaveChanges();
                return input;
            }
            return null;
        }
    }
}
