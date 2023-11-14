using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRooms.Commands;

namespace Booking.Application.Features.HotelRooms.Requests
{
    public class CreateHotelRequest : IMapping
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateHotelRequest, CreateHotelCommand>()
                .ConstructUsing(x => new CreateHotelCommand(x.RoomNumber, x.Type, x.Floor, x.Price));
        }
    }
}

