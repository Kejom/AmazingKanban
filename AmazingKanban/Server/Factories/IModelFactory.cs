using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Factories
{
    public interface IModelFactory
    {
        BoardAccess<UserLite> Convert(BoardAccess<ApplicationUser> access);
        BoardAccess<ApplicationUser> Convert(BoardAccess<UserLite> access);
        KanbanTask<UserLite> Convert(KanbanTask<ApplicationUser> task);
        KanbanTask<ApplicationUser> Convert(KanbanTask<UserLite> task);
        TaskComment<UserLite> Convert(TaskComment<ApplicationUser> comment);
        TaskComment<ApplicationUser> Convert(TaskComment<UserLite> comment);
    }
}