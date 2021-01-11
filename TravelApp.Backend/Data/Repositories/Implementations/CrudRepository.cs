using Microsoft.EntityFrameworkCore;
using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TravelApp.Backend.Data.Repositories.Implementations
{
    public class CrudRepository<T, I> : ICrudRepository<T, I> where I : IFilter<T> where T : TravelAppClass
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IQueryable<T> _query;
        public CrudRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _query = IncludeAll(_dbSet);
        }
        public virtual T Create(T input, string userId)
        {
            input.UserId = userId;
            _dbSet.Add(input);
            SaveChanges();
            return input;
        }

        public virtual bool Delete(long id, string userId)
        {
            var t = _dbSet.SingleOrDefault(c => c.Id == id && c.UserId == userId);
            if (t == null)
                return false;
            _dbSet.Remove(t);
            SaveChanges();
            return true;
        }

        public virtual IEnumerable<T> GetAll(I filter, string userId)
        {
            return filter.Filter(_dbSet, userId);
        }
        public virtual IEnumerable<T> GetAllEager(I filter, string userId)
        {
            return filter.Filter(_query, userId);
        }
        public virtual T Update(T input, string userId)
        {
            input.UserId = userId;
                _dbSet.Update(input);
                SaveChanges();
                return input;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        private IQueryable<T> IncludeAll(IQueryable<T> queryable)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
                if (property.GetGetMethod().IsVirtual)
                    queryable = queryable.Include(property.Name);
            return queryable;
        }
    }
}
