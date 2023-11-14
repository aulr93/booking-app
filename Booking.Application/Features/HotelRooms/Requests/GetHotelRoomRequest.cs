using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRooms.Queries;

namespace Booking.Application.Features.HotelRooms.Requests
{
    public class GetHotelRoomRequest : IMapping
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int? Floor { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetHotelRoomRequest, GetHotelRoomQuery>()
                .ConstructUsing(x => new GetHotelRoomQuery(x.RoomNumber, x.Type, x.Floor, x.MinPrice, x.MaxPrice));
        }
    }
}

