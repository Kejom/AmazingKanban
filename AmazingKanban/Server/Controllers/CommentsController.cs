using AmazingKanban.Server.Factories;
using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingKanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ITaskCommentRepository _commentRepository;
        private readonly IModelFactory _modelFactory;

        public CommentsController(ITaskCommentRepository commentRepository, IModelFactory modelFactory)
        {
            _commentRepository = commentRepository;
            _modelFactory = modelFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentRepository.GetAll();
                var result = comments.Select(c => _modelFactory.Convert(c)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetById(int commentId)
        {
            try
            {
                var comment = await _commentRepository.GetById(commentId);
                var result = _modelFactory.Convert(comment);
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
        public async Task<IActionResult> Add(TaskComment<UserLite> comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var commentToAdd = _modelFactory.Convert(comment);
                await _commentRepository.Add(commentToAdd);
                return Ok(commentToAdd.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TaskComment<UserLite> comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var commentToUpdate = _modelFactory.Convert(comment);
                await _commentRepository.Update(commentToUpdate);
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

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetByTaskId(int taskId)
        {
            try
            {
                var comments = await _commentRepository.GetByTaskId(taskId);
                var result = comments.Select(c => _modelFactory.Convert(c)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        } 
    }
}
