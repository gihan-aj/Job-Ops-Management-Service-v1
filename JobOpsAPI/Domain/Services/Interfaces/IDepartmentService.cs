using JobOpsAPI.Domain.DTOs.Department;

namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentGetDTO>? GetByPageNumber(int page, int pageSize);
        int GetCount();
        DepartmentGetDTO? GetById(string id);
        void AddSingle(int user, DepartmentPostDTO request);
        void UpdateSingle(int user, DepartmentPostDTO request);
        void DeleteSingle(int user, string id);
    }
}
