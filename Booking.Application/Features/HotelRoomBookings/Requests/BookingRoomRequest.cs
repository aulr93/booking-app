using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRooms.Commands;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class BookingRoomRequest : IMapping
    {
        public Guid RoomId { get; set; }
        public DateTime BookingDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookingRoomRequest, BookingRoomCommand>()
                .ConstructUsing(x => new BookingRoomCommand(x.RoomId, x.BookingDate));
        }
    }
}
