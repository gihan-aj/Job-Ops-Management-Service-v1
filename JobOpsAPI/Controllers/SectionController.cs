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
        public ActionResult<List<SectionGetResponse>> Get(int page, int pageSize)
        {
            try
            {
                _logger.LogInfo("SectionController : Get() called");

                List<SectionGetDTO> sections = _dataService.Section.GetByPageNumber(page, pageSize).ToList();

                if (sections != null && sections.Count > 0)
                {
                    _logger.LogInfo("SectionController : Get() successful");
                }
                else
                {
                    _logger.LogInfo("SectionController : Get() No data found");
                    sections = [];
                }

                int count = _dataService.Section.GetCount();

                return Ok(new SectionGetResponse(true, count, sections));
            }
            catch (Exception ex)
            {
                _logger.LogError($"SectionController : Get() -> {ex.Message}");
                _logger.LogError($"SectionController : Get() -> Exception : {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpGet("id")]
        public ActionResult<SectionGetDTO> GetById(string id)
        {
            try
            {
                _logger.LogInfo("SectionController : GetById() called");

                SectionGetDTO? section = _dataService.Section.GetById(id);
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
                _logger.LogError($"SectionController : GetById() -> {ex.Message}");
                _logger.LogError($"SectionController : GetById() -> Exception : {ex}");
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
                _logger.LogError($"SectionController : Add() -> {ex.Message}");
                _logger.LogError($"SectionController : Add() -> Exception : {ex}");
                return StatusCode(500, $"Internal server error occurred. \n{ex.Message}");
            }
        }

        [HttpPut("update")]
        public ActionResult<SectionGetDTO> Update(int user, [FromBody] SectionPostDTO request)
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
                _logger.LogError($"SectionController : Update() -> {ex.Message}");
                _logger.LogError($"SectionController : Update() -> Exception : {ex}");
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
    }
}
