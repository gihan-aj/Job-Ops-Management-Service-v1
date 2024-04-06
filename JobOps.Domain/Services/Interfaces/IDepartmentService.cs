using JobOps.Domain.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentGetResponse>? GetByPageNumber(int page, int pageSize);
        DepartmentGetResponse? GetById(string id);
        void AddSingle(int user, DepartmentPostRequest request);
        void UpdateSingle(int user, DepartmentPutRequest request);
        void DeleteSingle(int user, string id);
    }
}
