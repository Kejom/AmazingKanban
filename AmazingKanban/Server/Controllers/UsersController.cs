using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;


        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filter = "")
        {
            try
            {
                var users = await _userRepository.GetUsers(filter);
                var result = users.Select(u => u.ConvertToUserLite()).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByid(userId);
                var result = user.ConvertToUserLite();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("board/{boardId}/role/{role}")]
        public async Task<IActionResult> GetByBoardIdAndRole(int boardId, BoardRoles role, string? filter = "")
        {
            try
            {
                var users = await _userRepository.GetByBoardIdAndRole(boardId, role, filter);
                var result = users.Select(u => u.ConvertToUserLite()).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
