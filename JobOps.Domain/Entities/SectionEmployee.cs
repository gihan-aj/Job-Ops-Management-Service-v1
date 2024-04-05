using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Entities
{
    public class SectionEmployee
    {
        public required string SectionId { get; set; }
        public required string EmployeeId { get; set; }
        public Section Section { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
