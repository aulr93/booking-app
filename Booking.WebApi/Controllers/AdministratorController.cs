using Booking.Application.Features.Administrators.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    public class AdministratorController : BaseController
    {
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "register")]
        public async Task<IActionResult> Add([FromBody] CreateAdminCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}