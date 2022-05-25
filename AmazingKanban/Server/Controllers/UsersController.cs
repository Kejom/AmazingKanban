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


        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filter = "")
        {
            var users = await _userRepository.GetUsers(filter);
            var result = users.Select(u=> u.ConvertToUserLite()).ToList();  
            return Ok(result);
        }
    }
}
