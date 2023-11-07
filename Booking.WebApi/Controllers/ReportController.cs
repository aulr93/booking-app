using Booking.Application.Features.Reports.Commands;
using Booking.Application.Features.Reports.Models;
using Booking.Application.Features.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {

        [ResponseType(type: typeof(IEnumerable<GetIncomeReportVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet("income")]
        public async Task<IActionResult> GetIncomeReport([FromQuery] GetIncomeReportQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "income-generate")]
        public async Task<IActionResult> Add([FromBody] GenerateIncomeReportCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}