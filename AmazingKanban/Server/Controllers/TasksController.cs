using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Server.Utility;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IKanbanTaskRepository _taskRepository;
        private readonly IModelFactory _modelFactory;
        private readonly IUserValidationHelper _validationHelper;

        public TasksController(IKanbanTaskRepository taskRepository, IModelFactory modelFactory, IUserValidationHelper validationHelper)
        {
            _taskRepository = taskRepository;
            _modelFactory = modelFactory;
            _validationHelper = validationHelper;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskRepository.GetAll();
                var result = tasks.Select(t => _modelFactory.Convert(t)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetById(int taskId)
        {
            try
            {
                var task = await _taskRepository.GetById(taskId);

                var canAccess = await _validationHelper.ValidateBoardAccess(task.BoardId, User, BoardRoles.User);

                if (!canAccess)
                    return Forbid("access forbidden");

                var result = _modelFactory.Convert(task);
                return Ok(result);
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
        public async Task<IActionResult> Add(KanbanTask<UserLite> kanbanTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var canAccess = await _validationHelper.ValidateBoardAccess(kanbanTask.BoardId, User, BoardRoles.User);

                if (!canAccess)
                    return Forbid("access forbidden");

                var taskToAdd = _modelFactory.Convert(kanbanTask);
                await _taskRepository.Add(taskToAdd);
                return Ok(taskToAdd.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(KanbanTask<UserLite> kanbanTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var canAccess = await _validationHelper.ValidateBoardAccess(kanbanTask.BoardId, User, BoardRoles.Developer);

                if (!canAccess)
                    return Forbid("access forbidden");

                var taskToUpdate = _modelFactory.Convert(kanbanTask);
                await _taskRepository.Update(taskToUpdate);
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

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetByBoardId(int boardId)
        {
            try
            {
                var tasks = await _taskRepository.GetByBoardId(boardId);
                var result = tasks.Select(t => _modelFactory.Convert(t)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("board/{boardId}/state/{state}")]
        public async Task<IActionResult> GetByBoardIdAndState(int boardId, KanbanTaskStates state)
        {
            try
            {
                var tasks = await _taskRepository.GetByBoardIdAndState(boardId, state);
                var result = tasks.Select(t => _modelFactory.Convert(t)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
