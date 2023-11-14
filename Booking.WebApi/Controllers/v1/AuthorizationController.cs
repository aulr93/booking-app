using Booking.Application.Commons.Models;
using Booking.Application.Features.Administrators.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [AllowAnonymous]
    public class AuthorizationController : BaseController
    {
        [ResponseType(type: typeof(AuthenticationResponse), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "admin")]
        public async Task<IActionResult> AdminLoginAsync([FromBody] AdminLoginCommand command)
        {
            var response = await Mediator.Send(command);

            return Wrapper(TokenBuilder.Build(JwtManager, response));
        }
    }
}