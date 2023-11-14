using Booking.Application.Commons.Models;
using Booking.Application.Features.Authorizations.Commands;
using Booking.Application.Features.Authorizations.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [AllowAnonymous]
    public class AuthorizationController : BaseController
    {
        [ResponseType(type: typeof(AuthenticationResponse), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "admin")]
        public async Task<IActionResult> AdminLoginAsync([FromBody] AdminLoginRequest request)
        {
            var command = Mapper.Map<AdminLoginCommand>(request);
            
            var response = await Mediator.Send(command);

            return Wrapper(TokenBuilder.Build(JwtManager, response));
        }

        [ResponseType(type: typeof(AuthenticationResponse), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "visitor")]
        public async Task<IActionResult> VisitorLoginAsync([FromBody] VisitorLoginRequest request)
        {
            var command = Mapper.Map<VisitorLoginCommand>(request);
           
            var response = await Mediator.Send(command);

            return Wrapper(TokenBuilder.Build(JwtManager, response));
        }
    }
}