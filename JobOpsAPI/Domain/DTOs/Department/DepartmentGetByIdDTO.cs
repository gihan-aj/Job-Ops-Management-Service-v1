using JobOpsAPI.Domain.DTOs.Section;

namespace JobOpsAPI.Domain.DTOs.Department
{
    public class DepartmentGetByIdDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public List<SectionGetDTO>? Sections { get; set; }
    }
}
