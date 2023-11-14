using Booking.Application.Commons.Constants;
using Booking.Common.Interfaces;
using System.Security.Claims;

namespace Booking.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ApplicationClaimConstant.UserId);
        public string? Username => _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationClaimConstant.Username);
        public string? Role => _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationClaimConstant.Role);
    }
}
