using System.Linq.Expressions;

namespace JobOpsAPI.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
