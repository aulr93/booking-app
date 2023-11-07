using Booking.Application.Features.HotelRooms.Commands;
using FluentValidation;

namespace Booking.Application.Features.HotelRooms.Valildators
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.RoomNumber).MaximumLength(5).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotNull().NotEmpty();
            RuleFor(x => x.Floor).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty();
        }
    }
}
