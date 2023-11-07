using Booking.Application.Features.HotelRooms.Queries;
using FluentValidation;

namespace Booking.Application.Features.HotelRooms.Valildators
{
    public class GetDetailHotelRoomValidator : AbstractValidator<GetDetailHotelRoomBookingQuery>
    {
        public GetDetailHotelRoomValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
