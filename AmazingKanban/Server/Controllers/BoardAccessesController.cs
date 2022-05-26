using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/boards/access")]
    [ApiController]
    [Authorize]
    public class BoardAccessesController : ControllerBase
    {
        private readonly IBoardAccessRepository _boardAccessRepository;
        private readonly IModelFactory _modelFactory;

        public BoardAccessesController(IBoardAccessRepository boardAccessRepository, IModelFactory modelFactory)
        {
            _boardAccessRepository = boardAccessRepository;
            _modelFactory = modelFactory;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accesses = await _boardAccessRepository.GetAll();
                var result = accesses.Select(a => _modelFactory.Convert(a)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(BoardAccess<UserLite> access)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var accessToAdd = _modelFactory.Convert(access);
                await _boardAccessRepository.Add(accessToAdd);
                return Ok(accessToAdd.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(BoardAccess<UserLite> access)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var accessToUpdate = _modelFactory.Convert(access);
                await _boardAccessRepository.Update(accessToUpdate);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var accesses = await _boardAccessRepository.GetByUserId(userId);
                var result = accesses.Select(a => _modelFactory.Convert(a)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetByBoardId(int boardId)
        {
            try
            {
                var accesses = await _boardAccessRepository.GetByBoardId(boardId);
                var result = accesses.Select(a => _modelFactory.Convert(a)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("board/{boardId}/user/{userId}")]
        public async Task<IActionResult> GetByBoardAndUserId(int boardId, string userId)
        {
            try
            {
                var access = await _boardAccessRepository.GetByUserAndBoardId(userId, boardId);
                var result = _modelFactory.Convert(access);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("board/{boardId}")]
        public async Task<IActionResult> UpdateBoardAccesses(int boardId, List<BoardAccess<UserLite>> accesses)
        {
            try
            {
                var newAccesses = accesses.Select((a) => _modelFactory.Convert(a)).ToList();
                var currentAccesses = await _boardAccessRepository.GetByBoardId(boardId);
                var accessesToAdd = newAccesses.Except(currentAccesses);
                var accessesToRemove = currentAccesses.Except(newAccesses);
                var accessesToUpdate = newAccesses.Intersect(currentAccesses);

                foreach (var access in accessesToAdd)
                    await _boardAccessRepository.Add(access);

                foreach (var access in accessesToRemove)
                    await _boardAccessRepository.Delete(access.Id);

                foreach (var access in accessesToUpdate)
                    await _boardAccessRepository.Update(access);

                return Ok();
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
    }
}
