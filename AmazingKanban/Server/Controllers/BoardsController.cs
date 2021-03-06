using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Server.Utility;
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
        private readonly IBoardAccessRepository _boardAccessRepository;
        private readonly IModelFactory _modelFactory;
        private readonly IUserValidationHelper _validationHelper;
        public BoardsController(IBoardRepository boardRepository, IBoardAccessRepository boardAccessRepository, IModelFactory modelFactory, IUserValidationHelper validationHelper)
        {
            _boardRepository = boardRepository;
            _boardAccessRepository = boardAccessRepository;
            _modelFactory = modelFactory;
            _validationHelper = validationHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

  
        }
        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetById(int boardId)
        {
            var canAccess = await _validationHelper.ValidateBoardAccess(boardId, User, BoardRoles.User);
            if (!canAccess)
                return Forbid("access forbidden");
            try
            {
                var board = await _boardRepository.GetById(boardId);
                return Ok(board);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(BoardSubmitVM submitVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                submitVM.Board.OwnerId = userId;
                await _boardRepository.Add(submitVM.Board);

                submitVM.UserAccesses.Add(
                    new BoardAccess<UserLite>
                    {
                        UserId = userId,
                        BoardId = submitVM.Board.Id,
                        Role = BoardRoles.Admin
                    });

                foreach (var userAccess in submitVM.UserAccesses)
                {
                    var accessToAdd = _modelFactory.Convert(userAccess);
                    accessToAdd.BoardId = submitVM.Board.Id;
                    await _boardAccessRepository.Add(accessToAdd);
                }
                return Ok(submitVM.Board);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Board board)
        {
            var canAccess = await _validationHelper.ValidateBoardAccess(board.Id, User, BoardRoles.Admin);
            if (!canAccess)
                return Forbid("access forbidden");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _boardRepository.Update(board);
                return Ok(board);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
