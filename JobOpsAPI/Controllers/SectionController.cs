using JobOpsAPI.Domain.DTOs.Department;
using JobOpsAPI.Domain.DTOs.Section;
using JobOpsAPI.Domain.Services.Interfaces;
using LoggerLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobOpsAPI.Domain.DTOs.ServiceResponses;

namespace JobOpsAPI.Controllers
{
    [Route("api/section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IMasterDataService _dataService;
        private readonly IFileLogger _logger;

        public SectionController(IMasterDataService dataService, IFileLogger logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<SectionGetResponse>> Get(int page, int pageSize, string departmentId)
        {
            try
            {
                _logger.LogInfo("SectionController : Get() called");

                List<SectionGetDTO> sections = _dataService.Section.GetByPageNumber(page, pageSize, departmentId).ToList();

                if (sections != null && sections.Count > 0)
                {
                    _logger.LogInfo("SectionController : Get() successful");
                }
                else
                {
                    _logger.LogInfo("SectionController : Get() No data found");
                    sections = [];
                }

                int count = _dataService.Section.GetCount(departmentId);

                return Ok(new SectionGetResponse(true, count, sections));
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Get() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpGet("id")]
        public ActionResult<SectionGetByIdDTO> GetById(string id)
        {
            try
            {
                _logger.LogInfo("SectionController : GetById() called");

                SectionGetByIdDTO? section = _dataService.Section.GetByIdWithParent(id);
                if (section == null)
                {
                    _logger.LogInfo($"SectionController : GetById() : Section({id}) not found");
                    return NotFound($"Section({id}) not found.");
                }

                _logger.LogInfo("SectionController : GetById() successful");
                return Ok(section);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : GetById() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<SectionGetDTO> Add(int user, [FromBody] SectionPostDTO request)
        {
            try
            {
                _logger.LogInfo("SectionController : Add() called");

                _dataService.Section.AddSingle(user, request);
                _dataService.Save();

                _logger.LogInfo("SectionController : Add() successful");

                return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Add() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("update")]
        public ActionResult<SectionGetDTO> Update(int user, [FromBody] SectionPutDTO request)
        {
            try
            {
                _logger.LogInfo("SectionController : Update() called");

                _dataService.Section.UpdateSingle(user, request);
                _dataService.Save();

                SectionGetDTO? section = _dataService.Section.GetById(request.Id);
                if (section == null)
                {
                    _logger.LogInfo($"SectionController : Update() : Section({request.Id}) not found");
                    return NotFound($"Section({request.Id}) not found.");
                }

                _logger.LogInfo("SectionController : Update() successful");

                return Ok(section);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Update() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("activate")]
        public ActionResult Activate(int user, [FromBody] string[] ids)
        {
            try
            {
                _logger.LogInfo("SectionController : Activate() called");

                _dataService.Section.Activate(user, ids);
                _dataService.Save();

                _logger.LogInfo("SectionController : Activate() successful");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Activate() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("deactivate")]
        public ActionResult Deactivate(int user, [FromBody] string[] ids)
        {
            try
            {
                _logger.LogInfo("SectionController : Activate() called");

                _dataService.Section.Deactivate(user, ids);
                _dataService.Save();

                _logger.LogInfo("SectionController : Activate() successful");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Activate() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("delete")]
        public ActionResult Delete(int user, string id)
        {
            try
            {
                _logger.LogInfo("SectionController : Delete() called");

                _dataService.Section.SoftDeleteSingle(user, id);
                _dataService.Save();

                _logger.LogInfo("SectionController : Delete() successful");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Delete() -> {ex.Message}");
                _logger.LogError($"SectionController : Delete() -> Exception : {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("bulk-delete")]
        public ActionResult DeleteMultiple(int user, [FromBody] string[] ids)
        {
            try
            {
                _logger.LogInfo("SectionController : DeleteMultiple() called");

                _dataService.Section.SoftDeleteMultiple(user, ids);
                _dataService.Save();

                _logger.LogInfo("SectionController : DeleteMultiple() successful");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : DeleteMultiple() -> {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }
    }
}
