using HMT.LogsService.DTOs;
using HMT.LogsService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HMT.LogsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController (ILogService logService) : Controller
    {
        private readonly ILogService _logService = logService;

        #region get

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllLogs()
        {
            var result = await _logService.GetAllLogsAsync();

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetLogsByType(int type)
        {
            var result = await _logService.GetLogsByTypeAsync(type);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        #endregion

        #region post

        [HttpPost("entry")]
        public async Task<IActionResult> AcceptLog([FromBody] LogAcceptDto log)
        {
            var result = await _logService.AcceptLogAsync(log);

            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        #endregion
    }
}
