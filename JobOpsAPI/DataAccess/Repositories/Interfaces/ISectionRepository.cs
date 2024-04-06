using JobOpsAPI.Domain.Entities;

namespace JobOpsAPI.DataAccess.Repositories.Interfaces
{
    public interface ISectionRepository : IGenericRepository<Section>
    {
        IEnumerable<Section>? GetByPageNumber(int page, int pageSize);
        int GetDataCount();
        Section? GetByIdWithNavigationProperty(string id);
    }
}
