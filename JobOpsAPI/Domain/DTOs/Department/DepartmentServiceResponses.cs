using Microsoft.AspNetCore.Components.Web;

namespace JobOpsAPI.Domain.DTOs.Department
{
    public class DepartmentServiceResponses
    {
        public record class DepartmentGetResponse(bool Flag, int Count, List<DepartmentGetDTO> DataList);

    }
}
