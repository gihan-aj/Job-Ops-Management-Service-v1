using JobOps.Domain.Entities;
using JobOps.Domain.Repository;
using LoggerLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobOpsAPI.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileLogger _logger;

        public DepartmentController(IUnitOfWork unitOfWork, IFileLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Department>> Get()
        {
            try
            {
                _logger.LogInfo("DepartmentController : Get() called");

                List<Department> departments = _unitOfWork.Department.GetAll().ToList();

                if(departments != null && departments.Count > 0)
                {
                    _logger.LogInfo("DepartmentController : Get() successful");
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Get() -> {ex.Message}");
                _logger.LogError($"DepartmentController : Get() -> Exception : {ex}");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        [HttpGet("id")]
        public ActionResult<Department> GetById(string id)
        {
            try
            {
                Department department = _unitOfWork.Department.GetById(id);
                if(department == null)
                {
                    return NotFound("Department not found.");
                }
                return Ok(department);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult<Department> Add(Department department)
        {
            try
            {
                _unitOfWork.Department.Add(department);
                _unitOfWork.Save();

                return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
