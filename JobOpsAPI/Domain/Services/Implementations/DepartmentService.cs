using Azure.Core;
using JobOpsAPI.DataAccess.Context;
using JobOpsAPI.DataAccess.Repositories.Implementations;
using JobOpsAPI.DataAccess.Repositories.Interfaces;
using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.DTOs.Section;
using JobOpsAPI.Domain.Entities;
using JobOpsAPI.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobOpsAPI.Domain.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly JobOpsDbContext _context;
        private IDepartmentRepository _repository;

        public DepartmentService(JobOpsDbContext context)
        {
            _context = context;
            _repository = new DepartmentRepository(_context);
        }      

        public void AddSingle(int user, DepartmentPostDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));

                var existingData = _repository.GetById(request.Id);
                if (existingData != null)
                {
                    throw new InvalidOperationException($"Id already exists.");
                }

                var newDepartment = new Department()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Status = true,
                    CreatedBy = user,
                    CreatedOn = DateTime.Now,

                };

                _repository.Add(newDepartment);

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

                var department = _repository.GetById(id);
                if (department != null)
                {
                    department.DeletedBy = user;
                    department.DeletedOn = DateTime.Now;
                }
                else
                {
                    throw new Exception($"Id does not exist");
                }

                _repository.Update(department);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DepartmentGetByIdDTO? GetByIdWithChildEntities(string id)
        {
            try
            {
                var department = _repository.GetByIdWithNavigationProperty(id);
                if (department != null)
                {
                    if(department.DeletedBy != null)
                    {
                        throw new Exception("Data already been deleted");
                    }

                    var associatedSections = new List<SectionGetDTO>();
                    if(department.Sections != null && department.Sections.Count > 0)
                    {
                        foreach (var section in department.Sections)
                        {
                            if(section.DeletedOn == null && section.Status == true)
                            {
                                var sectionDTO = new SectionGetDTO()
                                {
                                    Id = section.Id,
                                    Name = section.Name,
                                    Description = section.Description,
                                    Status = section.Status,
                                };

                                associatedSections.Add(sectionDTO);
                            }

                        }
                    }

                    var response = new DepartmentGetByIdDTO()
                    {
                        Id = department.Id,
                        Name = department.Name,
                        Status = department.Status,
                        Sections = associatedSections
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

        public DepartmentGetDTO? GetById(string id)
        {
            try
            {
                var department = _repository.GetById(id);
                if (department != null)
                {
                    if (department.DeletedBy != null)
                    {
                        throw new Exception("Data already been deleted");
                    }

                    var response = new DepartmentGetDTO()
                    {
                        Id = department.Id,
                        Name = department.Name,
                        Status = department.Status,
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

        public IEnumerable<DepartmentGetDTO>? GetByPageNumber(int page, int pageSize)
        {
            try
            {
                List<DepartmentGetDTO> response = new List<DepartmentGetDTO>();

                var departments = _repository.GetByPageNumber(page, pageSize);
                if (departments != null)
                {
                    foreach (var department in departments)
                    {
                        response.Add(new DepartmentGetDTO()
                        {
                            Id = department.Id,
                            Name = department.Name,
                            Status = department.Status,
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
                return _repository.GetDataCount();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSingle(int user, DepartmentPutDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) throw new ArgumentNullException(nameof(request.Id));
                if (string.IsNullOrEmpty(request.Name)) throw new ArgumentNullException(nameof(request.Name));

                var department = _repository.GetById(request.Id);
                if (department != null)
                {
                    department.Name = request.Name;
                    department.UpdatedBy = user;
                    department.UpdatedOn = DateTime.Now;
                }
                else
                {
                    throw new Exception($"Id does not exist");
                }

                _repository.Update(department);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Activate(int user, string[] departmentIds)
        {
            try
            {
                if (departmentIds == null || departmentIds.Count() == 0) throw new ArgumentNullException(nameof(departmentIds));

                foreach (var departmentId in departmentIds)
                {
                    var department = _repository.GetById(departmentId);
                    if (department != null)
                    {
                        department.Status = true;
                        _repository.Update(department);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deactivate(int user, string[] departmentIds)
        {
            try
            {
                if (departmentIds == null || departmentIds.Count() == 0) throw new ArgumentNullException(nameof(departmentIds));

                foreach (var departmentId in departmentIds)
                {
                    var department = _repository.GetById(departmentId);
                    if (department != null)
                    {
                        department.Status = false;
                        _repository.Update(department);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SoftDeleteMultiple(int user, string[] departmentIds)
        {
            try
            {
                if (departmentIds == null || departmentIds.Count() == 0) throw new ArgumentNullException(nameof(departmentIds));

                foreach (var departmentId in departmentIds)
                {
                    var department = _repository.GetById(departmentId);
                    if (department != null)
                    {
                        department.DeletedBy = user;
                        department.DeletedOn = DateTime.Now;

                        _repository.Update(department);
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
