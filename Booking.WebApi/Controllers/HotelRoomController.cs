using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRooms.Commands;
using Booking.Application.Features.HotelRooms.Models;
using Booking.Application.Features.HotelRooms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelRoomController : BaseController
    {

        [ResponseType(type: typeof(List<string>), statusCode: StatusCodes.Status200OK)]
        [HttpGet("type")]
        public async Task<IActionResult> GetTypes([FromQuery] GetRoomTypeQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(PaginationResult<HotelRoomVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "list")]
        public async Task<IActionResult> GetPage([FromQuery] GetHotelRoomQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(HotelRoomVM), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "detail")]
        public async Task<IActionResult> GetDetail([FromQuery] GetDetailHotelRoomQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "add")]
        public async Task<IActionResult> Add([FromBody] CreateHotelCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPut(template: "update")]
        public async Task<IActionResult> Update([FromBody] UpdateHotelCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }

        [ResponseType(type: typeof(List<DeleteVM>), statusCode: StatusCodes.Status200OK)]
        [HttpDelete(template: "delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteHotelCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}