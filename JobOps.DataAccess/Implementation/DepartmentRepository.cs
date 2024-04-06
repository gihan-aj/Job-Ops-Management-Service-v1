﻿using JobOps.DataAccess.Context;
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

        public IEnumerable<Department>? GetByPageNumber(int page, int pageSize)
        {
            try
            {
                var departments = _context.Departments
                    .Where(d => d.DeletedOn == null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return departments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetDataCount()
        {
            try
            {
                int count = _context.Departments
                    .Where (d => d.DeletedOn == null)
                    .ToList()
                    .Count();

                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}