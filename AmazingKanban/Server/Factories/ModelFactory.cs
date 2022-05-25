using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Factories
{
    public class ModelFactory : IModelFactory
    {
        public BoardAccess<UserLite> Convert(BoardAccess<ApplicationUser> access)
        {
            return new BoardAccess<UserLite>
            {
                Id = access.Id,
                BoardId = access.BoardId,
                UserId = access.UserId,
                User = access.User is null ? null : access.User.ConvertToUserLite(),
                Role = access.Role,
            };
        }

        public BoardAccess<ApplicationUser> Convert(BoardAccess<UserLite> access)
        {
            return new BoardAccess<ApplicationUser>
            {
                Id = access.Id,
                BoardId = access.BoardId,
                UserId = access.UserId,
                Role = access.Role,
            };
        }

        public KanbanTask<UserLite> Convert(KanbanTask<ApplicationUser> task)
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

        public KanbanTask<ApplicationUser> Convert(KanbanTask<UserLite> task)
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

        public TaskComment<UserLite> Convert(TaskComment<ApplicationUser> comment)
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

        public TaskComment<ApplicationUser> Convert(TaskComment<UserLite> comment)
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
