using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRooms.Commands;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class CheckInOutRequest : IMapping
    {
        public Guid Id { get; set; }
        public EnumCheckInOut CheckInOut { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CheckInOutRequest, CheckInOutCommand>()
                .ConstructUsing(x => new CheckInOutCommand(x.Id, x.CheckInOut));
        }
    }
}
