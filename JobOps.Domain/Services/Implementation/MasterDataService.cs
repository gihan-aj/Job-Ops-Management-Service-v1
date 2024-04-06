using JobOps.DataAccess.Context;
using JobOps.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Services.Implementation
{
    public class MasterDataService : IMasterDataService
    {
        private readonly JobOpsDbContext _context;

        public MasterDataService(JobOpsDbContext context) 
        {
            _context = context;
            Department = new DepartmentService(context);
        }

        public IDepartmentService Department { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
