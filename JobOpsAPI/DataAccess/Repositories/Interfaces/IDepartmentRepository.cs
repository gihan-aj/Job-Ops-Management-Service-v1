using JobOpsAPI.Domain.Entities;

namespace JobOpsAPI.DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department>? GetByPageNumber(int page, int pageSize);
        int GetDataCount();
        IEnumerable<Department>? GetBySearch(int page, int pageSize, string keyWord);
        int GetSearchResultDataCount(string keyWord);
        Department? GetByIdWithNavigationProperty(string Id);
    }
}
