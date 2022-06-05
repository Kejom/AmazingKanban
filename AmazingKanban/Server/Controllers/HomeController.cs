using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IKanbanTaskRepository _taskRepo;
        private readonly IBoardRepository _boardRepo;

        public HomeController(IUserRepository userRepo, IKanbanTaskRepository taskRepo, IBoardRepository boardRepo)
        {
            _userRepo = userRepo;
            _taskRepo = taskRepo;
            _boardRepo = boardRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = new HomeVM
                {
                    Users = await _userRepo.GetCount(),
                    Boards = await _boardRepo.GetCount(),
                    Tasks = await _taskRepo.GetCount()
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
