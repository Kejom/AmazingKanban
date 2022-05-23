using AmazingKanban.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace AmazingKanban.Client.Utility
{
    public class UserUtility : IUserUtility
    {
        private readonly AuthenticationStateProvider _authProvider;
        public UserUtility(AuthenticationStateProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public async Task<string> GetCurrentUserId()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(c => c.Type == "sub").Value;
            
        }

        public async Task<bool> IsAdmin()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var adminClaim = user.Claims.FirstOrDefault(c => c.Value == UserRoles.Admin.ToString());
            return adminClaim is not null;
        }
    }
}
