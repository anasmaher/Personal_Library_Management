using System.Security.Claims;

namespace LibraryManagement.Services
{
    public class GetCurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
