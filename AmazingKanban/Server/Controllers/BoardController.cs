using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpGet("getBoards")]
        public async Task<IActionResult> Get()
        {
            var boards = new List<Board>();

            var isAdmin = User.IsInRole(UserRoles.Admin.ToString());
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (isAdmin)
                boards = await _boardRepository.GetAll();
            else
                boards = await _boardRepository.GetByUserId(userId);

 

            return Ok(boards);
  
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Board board)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            board.OwnerId = userId;
            await _boardRepository.AddBoard(board);
            return Ok(board);
        }

        private BoardRoles? GetBoardAccessLevel(List<BoardUserAccess> accesses, int boardId)
        {
            var access = accesses.FirstOrDefault(b =>b.BoardId == boardId);

            return access is null ? null : access.Role;
        }
    }
}
