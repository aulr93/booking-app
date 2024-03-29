using Booking.Application.Features.Visitors.Commands;
using Booking.Application.Features.Visitors.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [AllowAnonymous]
    public class VisitorController : BaseController
    {
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "register")]
        public async Task<IActionResult> Add([FromBody] CreateVisitorRequest request)
        {
            var command = Mapper.Map<CreateVisitorCommand>(request);
         
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}