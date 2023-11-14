using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRooms.Commands;
using Booking.Application.Features.HotelRooms.Models;
using Booking.Application.Features.HotelRooms.Queries;
using Booking.Application.Features.HotelRooms.Requests;
using Booking.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [Authorize]
    [RolePermission(Role.ADM)]
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
        public async Task<IActionResult> GetPage([FromQuery] GetHotelRoomRequest request)
        {
            var query = Mapper.Map<GetHotelRoomQuery>(request);

            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(HotelRoomVM), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "detail")]
        public async Task<IActionResult> GetDetail([FromQuery] GetDetailHotelRoomRequest request)
        {
            var query = Mapper.Map<GetDetailHotelRoomQuery>(request);
            
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "add")]
        public async Task<IActionResult> Add([FromBody] CreateHotelRequest request)
        {
            var command = Mapper.Map<CreateHotelCommand>(request);
            
            return Wrapper(val: await Mediator.Send(command));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPut(template: "update")]
        public async Task<IActionResult> Update([FromBody] UpdateHotelRequest request)
        {
            var command = Mapper.Map<UpdateHotelCommand>(request);
           
            return Wrapper(val: await Mediator.Send(command));
        }

        [ResponseType(type: typeof(List<DeleteVM>), statusCode: StatusCodes.Status200OK)]
        [HttpDelete(template: "delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteHotelRequest request)
        {
            var command = Mapper.Map<DeleteHotelCommand>(request);
          
            return Wrapper(val: await Mediator.Send(command));
        }
    }
}