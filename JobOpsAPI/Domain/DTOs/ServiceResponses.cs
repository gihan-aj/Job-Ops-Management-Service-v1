using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.DTOs.Section;

namespace JobOpsAPI.Domain.DTOs
{
    public class ServiceResponses
    {
        public record class DepartmentGetResponse(bool Flag, int Count, List<DepartmentGetDTO> DataList);
        public record class SectionGetResponse(bool Flag, int Count, List<SectionGetDTO> DataList);
    }
}
