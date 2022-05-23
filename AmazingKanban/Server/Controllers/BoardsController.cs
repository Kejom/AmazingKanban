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
    public class BoardsController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        public BoardsController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpGet()]
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
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetById(int boardId)
        {
            var board = await _boardRepository.GetById(boardId);

            if(board is null) return NotFound();

            var acccesses = await _boardRepository.GetAccessesByBoardId(boardId);

            var boardVM = new BoardVM
            {
                Board = board,
                UserAccesses = acccesses
            };

            return Ok(boardVM);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(BoardVM boardVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            boardVM.Board.OwnerId = userId;
            await _boardRepository.AddBoard(boardVM.Board);

            foreach (var userAccess in boardVM.UserAccesses)
            {
                userAccess.BoardId = boardVM.Board.Id;
                await _boardRepository.AddAccess(userAccess);
            }
            return Ok(boardVM.Board);
        }
        [HttpPost("access/{boardId}")]
        public async Task<IActionResult> UpdateBoardAccesses(int boardId, List<BoardUserAccess> accesses)
        {
            var currentAccesses = await _boardRepository.GetAccessesByBoardId(boardId);
            var accessesToAdd = accesses.Except(currentAccesses);
            var accessesToRemove = currentAccesses.Except(accesses);
            var accessesToUpdate = accesses.Intersect(currentAccesses);

            foreach (var access in accessesToAdd)
                await _boardRepository.AddAccess(access);

            foreach (var access in accessesToRemove)
                await _boardRepository.DeleteAccess(access);

            foreach(var access in accessesToUpdate)
                await _boardRepository.UpdateAccess(access);

            return Ok();
        }
    }
}
