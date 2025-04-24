using HMT.Foundation.DTOs;
using HMT_UserService.Interfaces;
using HMT_UserService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMT_UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController (IUserRepository userInterface, ILoggingRepository loggingInterface) : Controller
    {
        private readonly IUserRepository _userInterface = userInterface;
        private readonly ILoggingRepository _loggingInterface = loggingInterface;

        #region get

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userInterface.GetAllAsync();

            if (result is not null)
                return Ok(result);

            return StatusCode(404);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userInterface.GetByIdAsync(id);

            if (result is not null)
                return Ok(result);

            return StatusCode(404);
        }

        [HttpGet("getPass/{id}")]
        public async Task<IActionResult> GetPassById(Guid id)
        {
            var result = await _userInterface.GetPassByIdAsync(id);

            if (result is not null)
                return Ok(result);

            return StatusCode(404);
        }

        #endregion

        #region post

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UsersDataDto userData)
        {
            var result = await _userInterface.AddAsync(userData);

            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        #endregion

        #region patch/put/delete

        [HttpPatch("change/{id}")]
        public async Task<IActionResult> Update([FromBody] UsersDataDto userData, Guid id)
        {
            var result = await _userInterface.UpdateAsync(userData, id);

            if (result is not null)
                return Ok(result);

            return StatusCode(404);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userInterface.DeleteAsync(id);

            if (result is not null)
                return Ok(result);

            return StatusCode(404);
        }

        #endregion
    }
}
