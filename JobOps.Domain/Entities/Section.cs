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
    }
}
