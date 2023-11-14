using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRoomBookings.Queries;
using Booking.Application.Features.HotelRoomBookings.Requests;
using Booking.Application.Features.HotelRooms.Commands;
using Booking.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers.v1
{
    [Authorize]
    public class HotelRoomBookingController : BaseController
    {
        [RolePermission(Role.VST)]
        [ResponseType(type: typeof(PaginationResult<HotelRoomAvailabilityVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "availability-list")]
        public async Task<IActionResult> GetPageAvailablity([FromQuery] GetHotelRoomAvailabilityRequest request)
        {
            var query = Mapper.Map<GetHotelRoomAvailabilityQuery>(request);

            return Wrapper(val: await Mediator.Send(query));
        }

        [RolePermission(Role.VST)]
        [ResponseType(type: typeof(PaginationResult<HotelRoomBookingVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "booking-list")]
        public async Task<IActionResult> GetPageBooking([FromQuery] GetHotelRoomBookingRequest request)
        {
            var query = Mapper.Map<GetHotelRoomBookingQuery>(request);

            return Wrapper(val: await Mediator.Send(query));
        }

        [RolePermission(Role.VST)]
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "booking")]
        public async Task<IActionResult> Booking([FromBody] BookingRoomRequest request)
        {
            var command = Mapper.Map<BookingRoomCommand>(request);

            return Wrapper(val: await Mediator.Send(command));
        }

        [RolePermission(Role.VST)]
        [ResponseType(type: typeof(HotelRoomBookingVM), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "booking-detail")]
        public async Task<IActionResult> GetBookingDetail([FromQuery] GetDetailHotelRoomBookingRequest request)
        {
            var query = Mapper.Map<GetDetailHotelRoomBookingQuery>(request);

            return Wrapper(val: await Mediator.Send(query));
        }

        [RolePermission(Role.ADM)]
        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPut(template: "check-in-out")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInOutRequest request)
        {
            var command = Mapper.Map<CheckInOutCommand>(request);

            return Wrapper(val: await Mediator.Send(command));
        }
    }
}