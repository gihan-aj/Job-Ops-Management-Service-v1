using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobOpsAPI.DataAccess.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly JobOpsDbContext _context;

        public GenericRepository(JobOpsDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().AddRange(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().Where(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T? GetById(string id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().RemoveRange(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
