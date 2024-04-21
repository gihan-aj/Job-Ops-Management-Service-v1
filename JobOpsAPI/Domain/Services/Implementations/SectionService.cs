using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.DataAccess.Repositories.Implementations;
using JobOpsAPI.DataAccess.Repositories.Interfaces;
using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.DTOs.Section;
using JobOpsAPI.Domain.Entities;
using JobOpsAPI.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobOpsAPI.Domain.Services.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly JobOpsDbContext _context;
        private ISectionRepository _repository;

        public SectionService(JobOpsDbContext context)
        {
            _context = context;
            _repository = new SectionRepository(_context);
        }

        public IEnumerable<SectionGetDTO>? GetByPageNumber(int page, int pageSize, string departmentId )
        {
            try
            {
                List<SectionGetDTO> response = new List<SectionGetDTO>();

                var sections = _repository.GetByPageNumber(page, pageSize, departmentId).ToList();
                if (sections != null && sections.Count > 0)
                {
                    foreach (var section in sections)
                    {
                        response.Add(new SectionGetDTO()
                        {
                            Id = section.Id,
                            Name = section.Name,
                            Status = section.Status,
                            DepartmentId = section.DepartmentId,
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

        public int GetCount(string departmentId)
        {
            try
            {
                return _repository.GetDataCount(departmentId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SectionGetDTO? GetById(string id)
        {
            try
            {
                var section = _repository.GetById(id);
                if (section != null)
                {
                    if (section.DeletedBy != null)
                    {
                        throw new Exception("Data already been deleted");
                    }
                    var response = new SectionGetDTO()
                    {
                        Id = section.Id,
                        Name = section.Name,
                        Description = section.Description,
                        Status = section.Status,
                        DepartmentId = section.DepartmentId,
                    };
                    return response;
                }

                throw new Exception($"Data does not exist");


            }
            catch (Exception)
            {
                throw;
            }
        }

        public SectionGetByIdDTO? GetByIdWithParent(string id)
        {
            try
            {
                var section = _repository.GetByIdWithNavigationProperty(id);
                if (section != null)
                {
                    if (section.DeletedBy != null)
                    {
                        throw new Exception("Data already been deleted");
                    }

                    var parentDepartment = new DepartmentGetDTO();
                    if (section.Department != null)
                    {
                        parentDepartment = new DepartmentGetDTO()
                        {
                            Id = section.Department.Id,
                            Name = section.Department.Name,
                            Status = section.Department.Status,
                        };
                    }

                    var response = new SectionGetByIdDTO()
                    {
                        Id = section.Id,
                        Name = section.Name,
                        Description = section.Description,
                        Status = section.Status,
                        DepartmentId = section.DepartmentId,
                        Department = parentDepartment,
                    };
                    return response;
                }

                throw new Exception($"Data does not exist");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddSingle(int user, SectionPostDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));
                if (string.IsNullOrEmpty(request.DepartmentId)) throw new ArgumentNullException(nameof(request.DepartmentId));

                var dep = _context.Departments.Find(request.DepartmentId);
                if(dep == null)
                {
                    throw new InvalidDataException("Deaprtment id is incorrect");
                }

                var existingData = _repository.GetById(request.Id);
                if (existingData != null)
                {
                    throw new InvalidOperationException($"Id already exists.");
                }

                var newData = new Section()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Status = true,
                    DepartmentId = request.DepartmentId,
                    CreatedBy = user,
                    CreatedOn = DateTime.Now,

                };

                _repository.Add(newData);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSingle(int user, SectionPutDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));
                if (string.IsNullOrEmpty(request.DepartmentId)) throw new ArgumentNullException(nameof(request.DepartmentId));

                var section = _repository.GetById(request.Id);
                if (section != null)
                {
                    section.Name = request.Name;
                    section.Description = request.Description;
                    section.DepartmentId = request.DepartmentId;
                    section.UpdatedBy = user;
                    section.UpdatedOn = DateTime.Now;
                }
                else
                {
                    throw new Exception($"Id does not exist");
                }

                _repository.Update(section);

            }
            catch (Exception)
            {
                throw;
            }
        }



        public void Activate(int user, string[] ids)
        {
            try
            {
                if (ids == null || ids.Count() == 0) throw new ArgumentNullException(nameof(ids));

                foreach (var id in ids)
                {
                    var section = _repository.GetById(id);
                    if (section != null)
                    {
                        section.Status = true;

                        _repository.Update(section);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deactivate(int user, string[] ids)
        {
            try
            {
                if (ids == null || ids.Count() == 0) throw new ArgumentNullException(nameof(ids));

                foreach (var id in ids)
                {
                    var section = _repository.GetById(id);
                    if (section != null)
                    {
                        section.Status = false;

                        _repository.Update(section);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void SoftDeleteSingle(int user, string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

                var section = _repository.GetById(id);
                if (section != null)
                {
                    section.DeletedBy = user;
                    section.DeletedOn = DateTime.Now;
                }
                else
                {
                    throw new Exception($"Id does not exist");
                }

                _repository.Update(section);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SoftDeleteMultiple(int user, string[] ids)
        {
            try
            {
                if(ids == null || ids.Count() == 0) throw new ArgumentNullException(nameof(ids));

                foreach (var id in ids)
                {
                    var section = _repository.GetById(id);
                    if(section != null)
                    {
                        section.DeletedBy = user;
                        section.DeletedOn = DateTime.Now;

                        _repository.Update(section);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
