using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByIdAsync(int id);
        IEnumerable<T> GetAllAsync();
        IEnumerable<T> FindAsync(Expression<Func<T, bool>> predicate);
        void AddAsync(T entity);
        void AddRangeAsync (IEnumerable<T> entities);
        T RemoveAsync(T entity);
        IEnumerable<T> RemoveRangeAsync (IEnumerable<T> entities);
        void UpdateAsync(T entity);
    }
}
