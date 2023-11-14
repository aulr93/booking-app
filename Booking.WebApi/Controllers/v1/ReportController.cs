using Booking.Application.Commons.Constants;
using Booking.Application.Features.Reports.Commands;
using Booking.Application.Features.Reports.Models;
using Booking.Application.Features.Reports.Queries;
using Booking.Application.Features.Reports.Requests;
using Booking.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [Authorize]
    [RolePermission(Role.ADM)]
    public class ReportController : BaseController
    {

        [ResponseType(type: typeof(IEnumerable<GetIncomeReportVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet("income")]
        public async Task<IActionResult> GetIncomeReport([FromQuery] GetIncomeReportRequest request)
        {
            var query = Mapper.Map<GetIncomeReportQuery>(request);
           
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "income-generate")]
        public async Task<IActionResult> Add([FromBody] GenerateIncomeReportRequest request)
        {
            var command = Mapper.Map<GenerateIncomeReportCommand>(request);
          
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}