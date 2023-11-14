using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRooms.Queries;

namespace Booking.Application.Features.HotelRooms.Requests
{
    public class GetDetailHotelRoomRequest : IMapping
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetDetailHotelRoomRequest, GetDetailHotelRoomQuery>()
                .ConstructUsing(x => new GetDetailHotelRoomQuery(x.Id));
        }
    }
}

