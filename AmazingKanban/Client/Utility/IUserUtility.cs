
namespace AmazingKanban.Client.Utility
{
    public interface IUserUtility
    {
        Task<string> GetCurrentUserId();
        Task<bool> IsAdmin();
    }
}