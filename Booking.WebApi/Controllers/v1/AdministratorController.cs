using Booking.Application.Features.Administrators.Commands;
using Booking.Application.Features.Administrators.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [AllowAnonymous]
    public class AdministratorController : BaseController
    {
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "register")]
        public async Task<IActionResult> Add([FromBody] CreateAdminRequest request)
        {
            var command = Mapper.Map<CreateAdminCommand>(request);

            return Wrapper(val: await Mediator.Send(command));
        }
    }
}