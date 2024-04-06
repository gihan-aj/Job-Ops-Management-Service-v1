using JobOpsAPI.Domain.DTOs.Department;

namespace JobOpsAPI.Domain.DTOs
{
    public class ServiceResponses
    {
        public record class DepartmentGetResponse(bool Flag, int Count, List<DepartmentGetDTO> DataList);
    }
}
