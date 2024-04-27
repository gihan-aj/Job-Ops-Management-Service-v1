using JobOpsAPI.Domain.DTOs.Department;
using Microsoft.AspNetCore.Mvc;

namespace JobOpsAPI.Domain.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentGetDTO>? GetByPageNumber(int page, int pageSize);
        int GetCount();
        DepartmentGetByIdDTO? GetByIdWithChildEntities(string id);
        DepartmentGetDTO? GetById(string id);
        IEnumerable<DepartmentGetDTO>? GetBySearch(int page, int pageSize, string keyWord);
        int GetSearchResultCount(string keyWord);
        void AddSingle(int user, DepartmentPostDTO request);
        void UpdateSingle(int user, DepartmentPutDTO request);
        void SoftDeleteSingle(int user, string id);
        void Activate(int user, string[] departmentIds);
        void Deactivate(int user, string[] departmentIds);
        void SoftDeleteMultiple(int user, string[] departmentIds);
    }
}
