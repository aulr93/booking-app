using Booking.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booking.WebApi.Common
{
    public class ModelValidationActionExecutingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ModelStateValidationException(context.ModelState);
            }
        }
    }
}
