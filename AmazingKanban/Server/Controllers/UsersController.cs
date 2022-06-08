using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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

        [HttpGet("admin")]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> GetAllAdminView()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                var result = new List<UserVM>();

                foreach (var user in users)
                {
                    result.Add(new UserVM
                    {
                        User = user.ConvertToUserLite(),
                        IsAdmin = _userManager.IsInRoleAsync(user, nameof(UserRoles.Admin)).GetAwaiter().GetResult(),
                        IsLocked = user.LockoutEnd > DateTime.Now
                    });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("admin/promote/{userId}")]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByid(userId);
                await _userManager.AddToRoleAsync(user, nameof(UserRoles.Admin));
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("admin/demote/{userId}")]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> DemoteAdmin(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByid(userId);
                await _userManager.RemoveFromRoleAsync(user, nameof(UserRoles.Admin));
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("admin/lock/{userId}")]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> LockUser(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByid(userId);
                var lockoutEndDate = new DateTime(2999, 01, 01);
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("admin/unlock/{userId}")]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> UnockUser(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByid(userId);
                await _userRepository.Unlock(userId);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
