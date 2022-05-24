using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
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
        private readonly IUserLiteFactory _userLiteFactory;

        public UsersController(IUserRepository userRepository, IUserLiteFactory userLiteFactory)
        {
            _userRepository = userRepository;
            _userLiteFactory = userLiteFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filter = "")
        {
            var users = await _userRepository.GetUsers(filter);
            var result = users.Select(u=> u.ConvertToUserLite()).ToList();  
            return Ok(result);
        }
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetByBoardId(int boardId)
        {
            var users = await _userRepository.GetUsersByBoardId(boardId);

            var result = users.Select(u => new BoardUserVM
            {
                User = _userLiteFactory.Convert(u.User!),
                BoardRole = u.Role
            }).ToList();

            return Ok(result);
        }
    }
}
