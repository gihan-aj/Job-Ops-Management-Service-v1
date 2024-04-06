using Azure.Core;
using JobOps.DataAccess.Context;
using JobOps.DataAccess.Implementation;
using JobOps.Domain.DTOs.Department;
using JobOps.Domain.Entities;
using JobOps.Domain.Repository;
using JobOps.Domain.Services.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOps.Domain.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly JobOpsDbContext _context;

        public DepartmentService(JobOpsDbContext context)
        {
            _context = context;
            Repository = new DepartmentRepository(_context);
        }

        public IDepartmentRepository Repository { get; private set; }
        public void AddSingle(int user, DepartmentPostRequest request)
        {
            try
            {
                if(string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if(string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));

                var newDepartment = new Department() 
                { 
                    Id = request.Id,
                    Name = request.Name,
                    CreatedBy = user,
                    CreatedOn = DateTime.Now,

                };

                Repository.Add(newDepartment);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteSingle(int user, string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

                var department = Repository.GetById(id);
                if (department != null)
                {
                    department.DeletedBy = user;
                    department.DeletedOn = DateTime.Now;
                }
                else
                {
                    throw new Exception($"Department({id}) does not exist");
                }

                Repository.Update(department);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DepartmentGetResponse? GetById(string id)
        {
            try
            {
                var department = Repository.GetById(id);
                if(department != null)
                {
                    var response = new DepartmentGetResponse()
                    {
                        Id= department.Id,
                        Name = department.Name,
                    };
                    return response;
                }

                return null;

          
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DepartmentGetResponse> GetByPageNumber(int page, int pageSize)
        {
            try
            {
                List<DepartmentGetResponse> response = new List<DepartmentGetResponse>();

                var departments = Repository.GetByPageNumber(page, pageSize);
                if(departments != null)
                {
                    foreach(var department in departments)
                    {
                        response.Add(new DepartmentGetResponse()
                        {
                            Id = department.Id,
                            Name = department.Name,
                        });
                    }
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSingle(int user, DepartmentPutRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));

                var department = Repository.GetById(request.Id);
                if (department != null)
                {
                    department.Name = request.Name;
                }
                else
                {
                    throw new Exception($"Department({request.Id}) does not exist");
                }

                Repository.Update(department);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
