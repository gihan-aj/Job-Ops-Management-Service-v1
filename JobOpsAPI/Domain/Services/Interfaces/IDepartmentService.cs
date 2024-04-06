using JobOpsAPI.Domain.DTOs.Department;

namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentGetResponse>? GetByPageNumber(int page, int pageSize);
        DepartmentGetResponse? GetById(string id);
        void AddSingle(int user, DepartmentPostRequest request);
        void UpdateSingle(int user, DepartmentPostRequest request);
        void DeleteSingle(int user, string id);
    }
}
