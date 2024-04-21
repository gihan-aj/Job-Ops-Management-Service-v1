using JobOpsAPI.Domain.Entities;

namespace JobOpsAPI.DataAccess.Repositories.Interfaces
{
    public interface ISectionRepository : IGenericRepository<Section>
    {
        IEnumerable<Section>? GetByPageNumber(int page, int pageSize, string departmentId);
        int GetDataCount(string departmentId);
        Section? GetByIdWithNavigationProperty(string id);
    }
}
