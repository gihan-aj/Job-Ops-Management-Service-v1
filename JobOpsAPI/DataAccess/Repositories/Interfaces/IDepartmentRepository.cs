using JobOpsAPI.Domain.Entities;

namespace JobOpsAPI.DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department>? GetByPageNumber(int page, int pageSize);
        int GetDataCount();
        Department? GetByIdWithNavigationProperty(string Id);
    }
}
