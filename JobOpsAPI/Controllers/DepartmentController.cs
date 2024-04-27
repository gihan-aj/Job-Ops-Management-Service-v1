using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.Entities;
using JobOpsAPI.Domain.Services.Interfaces;
using LoggerLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using static JobOpsAPI.Domain.DTOs.ServiceResponses;

namespace JobOpsAPI.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMasterDataService _dataService;
        private readonly IFileLogger _logger;

        public DepartmentController(IMasterDataService dataService, IFileLogger logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<DepartmentGetResponse>> Get(int page, int pageSize)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Get() called");

                List<DepartmentGetDTO> departments = _dataService.Department.GetByPageNumber(page, pageSize).ToList();

                if(departments != null && departments.Count > 0)
                {
                    _logger.LogInfo("DepartmentController : Get() successful");
                }
                else
                {
                    _logger.LogInfo("DepartmentController : Get() No data found");
                    departments = [];
                }

                int count = _dataService.Department.GetCount();

                return Ok(new DepartmentGetResponse(true, count, departments));
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Get() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("search")]
        public ActionResult<List<DepartmentGetResponse>> GetBySearch(int page, int pageSize, string keyWord)
        {
            try
            {
                _logger.LogInfo("DepartmentController : GetBySearch() called");

                List<DepartmentGetDTO> departments = _dataService.Department.GetBySearch(page, pageSize, keyWord).ToList();

                if (departments != null && departments.Count > 0)
                {
                    _logger.LogInfo("DepartmentController : GetBySearch() successful");
                }
                else
                {
                    _logger.LogInfo("DepartmentController : GetBySearch() No data found");
                    departments = [];
                }

                int count = _dataService.Department.GetSearchResultCount(keyWord);

                return Ok(new DepartmentGetResponse(true, count, departments));
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Get() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("id")]
        public ActionResult<DepartmentGetByIdDTO> GetById(string id)
        {
            try
            {
                _logger.LogInfo("DepartmentController : GetById() called");

                DepartmentGetByIdDTO? department = _dataService.Department.GetByIdWithChildEntities(id);
                if(department == null)
                {
                    _logger.LogInfo($"DepartmentController : GetById() : Department({id}) not found");
                    return NotFound($"Department({id}) not found.");
                }

                _logger.LogInfo("DepartmentController : GetById() successful");
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : GetById() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<DepartmentGetDTO> Add(int user, [FromBody]DepartmentPostDTO department)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Add() called");

                _dataService.Department.AddSingle(user, department);
                _dataService.Save();

                _logger.LogInfo("DepartmentController : Add() successful");

                return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Add() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPut("update")]
        public ActionResult<DepartmentGetDTO> Update(int user, [FromBody]DepartmentPutDTO request)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Update() called");

                _dataService.Department.UpdateSingle(user, request);
                _dataService.Save();

                DepartmentGetDTO? department = _dataService.Department.GetById(request.Id);
                if (department == null)
                {
                    _logger.LogInfo($"DepartmentController : Update() : Department({request.Id}) not found");
                    return NotFound($"Department({request.Id}) not found.");
                }

                _logger.LogInfo("DepartmentController : Update() successful");

                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Update() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPut("bulk-activate")]
        public ActionResult Activate(int user, [FromBody] string[] ids)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Activate() called");

                _dataService.Department.Activate(user, ids);
                _dataService.Save();

                _logger.LogInfo("DepartmentController : Activate() successful");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Activate() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPut("bulk-deactivate")]
        public ActionResult Deactivate(int user, [FromBody] string[] ids)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Deactivate() called");

                _dataService.Department.Deactivate(user, ids);
                _dataService.Save();

                _logger.LogInfo("DepartmentController : Deactivate() successful");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Deactivate() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPut("delete")]
        public ActionResult Delete(int user, string id)
        {
            try
            {
                _logger.LogInfo("DepartmentController : Delete() called");

                _dataService.Department.SoftDeleteSingle(user, id);
                _dataService.Save();

                _logger.LogInfo("DepartmentController : Delete() successful");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : Delete() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPut("bulk-delete")]
        public ActionResult MultipleDelete(int user, [FromBody]string[] ids)
        {
            try
            {
                _logger.LogInfo("DepartmentController : MultipleDelete() called");

                _dataService.Department.SoftDeleteMultiple(user, ids);
                _dataService.Save();

                _logger.LogInfo("DepartmentController : MultipleDelete() successful");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DepartmentController : MultipleDelete() -> {ex}");
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
