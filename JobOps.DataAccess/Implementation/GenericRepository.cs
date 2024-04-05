using JobOps.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public void AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
