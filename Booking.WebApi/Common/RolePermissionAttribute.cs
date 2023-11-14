using Booking.Application.Common.Exceptions;
using Booking.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booking.WebApi.Common
{
    public class RolePermissionAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RolePermissionAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUserService = context.HttpContext.RequestServices.GetService<ICurrentUserService>();
            if (currentUserService != null)
            {
                if (!_roles.Contains(currentUserService.Role))
                    throw new ForbiddenException();
            }
        }
    }
}
