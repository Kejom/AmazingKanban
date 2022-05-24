using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IKanbanTaskRepository _taskRepository;

        public TaskController(IKanbanTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskRepository.GetAll();
                var result = tasks.Select(t => ConvertTask(t)).ToList();
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
                var result = ConvertTask(task);
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
                var taskToAdd = ConvertTask(kanbanTask);
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
                var taskToUpdate = ConvertTask(kanbanTask);
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
                var result = tasks.Select(t => ConvertTask(t)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private KanbanTask<UserLite> ConvertTask(KanbanTask<ApplicationUser> task)
        {
            return new KanbanTask<UserLite>
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Title = task.Title,
                Description = task.Description,
                CreatedOn = task.CreatedOn,
                UpdatedOn = task.UpdatedOn,
                ClosedOn = task.ClosedOn,
                ShowOnBoard = task.ShowOnBoard,
                Priority = task.Priority,
                State = task.State,
                CreatedById = task.CreatedById,
                CreatedBy = task.CreatedBy is null ? null : task.CreatedBy.ConvertToUserLite(),
                AssignedToId = task.AssignedToId,
                AssignedTo = task.AssignedTo is null ? null : task.AssignedTo.ConvertToUserLite(),
                ValidatorId = task.ValidatorId,
                Validator = task.Validator is null ? null : task.Validator.ConvertToUserLite()
            };
        }

        private KanbanTask<ApplicationUser> ConvertTask(KanbanTask<UserLite> task)
        {
            return new KanbanTask<ApplicationUser>
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Title = task.Title,
                Description = task.Description,
                CreatedOn = task.CreatedOn,
                UpdatedOn = task.UpdatedOn,
                ClosedOn = task.ClosedOn,
                ShowOnBoard = task.ShowOnBoard,
                Priority = task.Priority,
                State = task.State,
                CreatedById = task.CreatedById,
                AssignedToId = task.AssignedToId,
                ValidatorId = task.ValidatorId,
            };
        }
    }
}
