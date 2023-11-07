using Booking.Application.Features.HotelRoomBookings.Queries;
using FluentValidation;

namespace Booking.Application.Features.HotelRoomBookings.Valildators
{
    public class GetHotelRoomAvailabilityQueryValidator : AbstractValidator<GetHotelRoomAvailabilityQuery>
    {
        public GetHotelRoomAvailabilityQueryValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.GetData).NotNull().NotEmpty();
        }
    }
}
