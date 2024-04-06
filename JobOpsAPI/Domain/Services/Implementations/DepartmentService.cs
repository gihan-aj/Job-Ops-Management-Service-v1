using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.DataAccess.Repositories.Implementations;
using JobOpsAPI.DataAccess.Repositories.Interfaces;
using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.Entities;
using JobOpsAPI.Domain.Services.Interfaces;

namespace JobOpsAPI.Domain.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly JobOpsDbContext _context;

        public DepartmentService(JobOpsDbContext context)
        {
            _context = context;
            Repository = new DepartmentRepository(_context);
        }

        private IDepartmentRepository Repository { get; set; }

        public void AddSingle(int user, DepartmentPostDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));

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

        public DepartmentGetDTO? GetById(string id)
        {
            try
            {
                var department = Repository.GetById(id);
                if (department != null)
                {
                    var response = new DepartmentGetDTO()
                    {
                        Id = department.Id,
                        Name = department.Name,
                    };
                    return response;
                }

                throw new Exception($"Department({id}) does not exist");


            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DepartmentGetDTO>? GetByPageNumber(int page, int pageSize)
        {
            try
            {
                List<DepartmentGetDTO> response = new List<DepartmentGetDTO>();

                var departments = Repository.GetByPageNumber(page, pageSize);
                if (departments != null)
                {
                    foreach (var department in departments)
                    {
                        response.Add(new DepartmentGetDTO()
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

        public int GetCount()
        {
            try
            {
                return Repository.GetDataCount();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSingle(int user, DepartmentPostDTO request)
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
