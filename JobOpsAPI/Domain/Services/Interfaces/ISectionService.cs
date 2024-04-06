using JobOpsAPI.Domain.DTOs.Section;

namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<SectionGetDTO>? GetByPageNumber(int page, int pageSize);
        int GetCount();
        SectionGetDTO? GetById(string id);
        void AddSingle(int user, SectionPostDTO request);
        void UpdateSingle(int user, SectionPostDTO request);
        void SoftDeleteSingle(int user, string id);
    }
}
