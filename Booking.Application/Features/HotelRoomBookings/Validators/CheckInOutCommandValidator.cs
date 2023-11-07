using Booking.Application.Features.HotelRooms.Commands;
using FluentValidation;

namespace Booking.Application.Features.HotelRoomBookings.Valildators
{
    public class CheckInOutCommandValidator : AbstractValidator<CheckInOutCommand>
    {
        public CheckInOutCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.CheckInOut).NotNull().NotEmpty();
        }
    }
}
