using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository, IBoardRepository boardRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filter = "")
        {
            var result = await _userRepository.GetUsersAsVM(filter);
            return Ok(result);
        }
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetByBoardId(int boardId)
        {
            var result = await _userRepository.GetUsersAsVmByBoardId(boardId);
            return Ok(result);
        }
    }
}
