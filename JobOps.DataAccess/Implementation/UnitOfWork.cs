using JobOps.DataAccess.Context;
using JobOps.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobOpsDbContext _context;

        public UnitOfWork(JobOpsDbContext context)
        {
            _context = context;

            Department = new DepartmentRepository(_context);
        }
        public IDepartmentRepository Department {  get; private set; }

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
