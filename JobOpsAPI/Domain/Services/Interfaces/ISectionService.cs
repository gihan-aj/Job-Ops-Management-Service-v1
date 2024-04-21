using JobOpsAPI.Domain.DTOs.Section;

namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<SectionGetDTO>? GetByPageNumber(int page, int pageSize, string departmentId);
        int GetCount(string departmentId);
        SectionGetDTO? GetById(string id);
        SectionGetByIdDTO? GetByIdWithParent(string id);
        void AddSingle(int user, SectionPostDTO request);
        void UpdateSingle(int user, SectionPutDTO request);
        void Activate(int user, string[] ids);
        void Deactivate(int user, string[] ids);
        void SoftDeleteSingle(int user, string id);
        void SoftDeleteMultiple(int user, string[] ids);

    }
}
