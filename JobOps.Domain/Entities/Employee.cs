using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Entities
{
    public class Employee
    {
        public required string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobId { get; set; } = string.Empty;
        public JobTitle JobTitle { get; set; } = null!;
        public string SectionId { get; set; } = string.Empty;
        public List<Section> Sections { get; } = [];
    }
}
