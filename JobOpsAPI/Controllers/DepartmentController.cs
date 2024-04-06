using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.Entities;
using JobOpsAPI.Domain.Services.Interfaces;
using LoggerLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace JobOpsAPI.Controllers
{
    [Route("api/department")]
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

                List<DepartmentGetResponse>? departments = _dataService.Department.GetByPageNumber(page, pageSize).ToList();

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
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpGet("id")]
        public ActionResult<DepartmentGetResponse> GetById(string id)
        {
            try
            {
                _logger.LogInfo("DepartmentController : GetById() called");

                DepartmentGetResponse? department = _dataService.Department.GetById(id);
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
                _logger.LogError($"DepartmentController : GetById() -> {ex.Message}");
                _logger.LogError($"DepartmentController : GetById() -> Exception : {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<DepartmentGetResponse> Add(int user, [FromBody]DepartmentPostRequest department)
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
                _logger.LogError($"DepartmentController : Add() -> {ex.Message}");
                _logger.LogError($"DepartmentController : Add() -> Exception : {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }
    }
}
