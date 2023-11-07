using Booking.Common.Interfaces;
using System.Security.Claims;

namespace Booking.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("");
        }

        public string UserId { get; }
        public Guid? UserIdInGuid => string.IsNullOrWhiteSpace(UserId) ? (Guid?)null : Guid.Parse(UserId);
    }
}
