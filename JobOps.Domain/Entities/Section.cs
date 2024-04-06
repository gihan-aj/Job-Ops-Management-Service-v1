using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Entities
{
    public class Section
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
        public Department? Department { get; set; }
        public ICollection<Machine>? Machines { get; set; }
        public List<Employee> Employees { get; } = [];
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
