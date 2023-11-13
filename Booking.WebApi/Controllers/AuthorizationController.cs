using Booking.Application.Features.Administrators.Commands;
using Booking.Application.Features.Authorizations.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : BaseController
    {
        [ResponseType(type: typeof(LoginVM), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "admin")]
        public async Task<IActionResult> AdminLoginAsync([FromBody] AdminLoginCommand command)
        {
            return Wrapper(await Mediator.Send(command));
        }
    }
}