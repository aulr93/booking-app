using Booking.Application.Features.Visitors.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    public class VisitorController : BaseController
    {
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "register")]
        public async Task<IActionResult> Add([FromBody] CreateVisitorCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}