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

        public CommentsController(ITaskCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentRepository.GetAll();
                var result = comments.Select(c => Convert(c)).ToList();
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
                var result = Convert(comment);
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
                var commentToAdd = Convert(comment);
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
                var commentToUpdate = Convert(comment);
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
                var result = comments.Select(c => Convert(c)).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        private TaskComment<UserLite> Convert(TaskComment<ApplicationUser> comment)
        {
            return new TaskComment<UserLite>
            {
                Id = comment.Id,
                BoardId = comment.BoardId,
                TaskId = comment.TaskId,
                CreatedOn = comment.CreatedOn,
                UpdatedOn = comment.UpdatedOn,
                CreatedById = comment.CreatedById,
                CreatedBy = comment.CreatedBy is null ? null : comment.CreatedBy.ConvertToUserLite(),
                CommentText = comment.CommentText,
                ShowOnBoard = comment.ShowOnBoard
            };
        }

        private TaskComment<ApplicationUser> Convert(TaskComment<UserLite> comment)
        {
            return new TaskComment<ApplicationUser>
            {
                Id = comment.Id,
                BoardId = comment.BoardId,
                TaskId = comment.TaskId,
                CreatedOn = comment.CreatedOn,
                UpdatedOn = comment.UpdatedOn,
                CreatedById = comment.CreatedById,
                CommentText = comment.CommentText,
                ShowOnBoard = comment.ShowOnBoard
            };
        }
    }
}
