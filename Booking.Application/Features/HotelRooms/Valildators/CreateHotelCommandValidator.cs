using Booking.Application.Features.HotelRooms.Commands;
using FluentValidation;

namespace Booking.Application.Features.HotelRooms.Valildators
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.RoomNumber).MaximumLength(5).NotNull().NotEmpty();
            RuleFor(x => x.Class).NotNull().NotEmpty();
            RuleFor(x => x.Floor).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty();
        }
    }
}
