using Booking.Application.Features.HotelRoomBookings.Queries;
using FluentValidation;

namespace Booking.Application.Features.HotelRoomBookings.Valildators
{
    public class GetDetailHotelRoomBookingValidator : AbstractValidator<GetDetailHotelRoomBookingQuery>
    {
        public GetDetailHotelRoomBookingValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
