using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
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

        public TasksController(IKanbanTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
    }
}
