using Booking.Application.Features.HotelRooms.Commands;
using FluentValidation;

namespace Booking.Application.Features.HotelRooms.Valildators
{
    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(x => x.Ids).NotNull().Must(x => x.Distinct().Count() == x.Count());
        }
    }
}
