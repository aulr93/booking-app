using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRoomBookings.Queries;
using Booking.Application.Features.HotelRooms.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Booking.Application.Features.HotelRooms.Commands.CheckInOutCommand;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelRoomBookingController : BaseController
    {
        [ResponseType(type: typeof(PaginationResult<HotelRoomAvailabilityVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "availability-list")]
        public async Task<IActionResult> GetPageAvailablity([FromQuery] GetHotelRoomAvailabilityQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "booking")]
        public async Task<IActionResult> Booking([FromBody] BookingRoomCommand command)
        {
            return Wrapper(val: await Mediator.Send(command));
        }

        [ResponseType(type: typeof(PaginationResult<HotelRoomBookingVM>), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "booking-list")]
        public async Task<IActionResult> GetPageBooking([FromQuery] GetHotelRoomBookingQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(HotelRoomBookingVM), statusCode: StatusCodes.Status200OK)]
        [HttpGet(template: "booking-detail")]
        public async Task<IActionResult> GetBookingDetail([FromQuery] GetDetailHotelRoomBookingQuery query)
        {
            return Wrapper(val: await Mediator.Send(query));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPut(template: "check-in")]
        public async Task<IActionResult> CheckIn([FromBody] Guid bookingId)
        {
            return Wrapper(val: await Mediator.Send(new CheckInOutCommand(bookingId, EnumCheckInOut.CheckIn)));
        }

        [ResponseType(type: typeof(Unit), statusCode: StatusCodes.Status200OK)]
        [HttpPut(template: "check-out")]
        public async Task<IActionResult> CheckOut([FromBody] Guid bookingId)
        {
            return Wrapper(val: await Mediator.Send(new CheckInOutCommand(bookingId, EnumCheckInOut.CheckOut)));
        }
    }
}