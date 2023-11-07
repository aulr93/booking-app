using Booking.Application.Features.HotelRooms.Commands;
using FluentValidation;

namespace Booking.Application.Features.HotelRoomBookings.Valildators
{
    public class BookingRoomCommandValidator : AbstractValidator<BookingRoomCommand>
    {
        public BookingRoomCommandValidator()
        {
            RuleFor(x => x.RoomId).NotNull().NotEmpty();
            RuleFor(x => x.VisitorName).NotNull().NotEmpty();
            RuleFor(x => x.NIK).NotNull().NotEmpty();
            RuleFor(x => x.BookingDate).NotNull().NotEmpty();
            RuleFor(x => x.CheckInDate).NotNull().NotEmpty();
            RuleFor(x => x.CheckOutDate).NotNull().NotEmpty();
        }
    }
}
