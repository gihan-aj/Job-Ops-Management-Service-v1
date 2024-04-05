using JobOps.DataAccess.Context;
using JobOps.Domain.Entities;
using JobOps.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.DataAccess.Implementation
{
    public class DepartmentRepository : GenericRepository<Department> ,IDepartmentRepository
    {
        private readonly JobOpsDbContext _context;

        public DepartmentRepository(JobOpsDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
