using JobOpsAPI.Domain.DTOs.Department;

namespace JobOpsAPI.Domain.DTOs.Section
{
    public class SectionGetByIdDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public string Description { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
        public DepartmentGetDTO? Department { get; set; }
    }
}
