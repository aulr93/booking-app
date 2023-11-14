using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRooms.Commands;

namespace Booking.Application.Features.HotelRooms.Requests
{
    public class DeleteHotelRequest : IMapping
    {
        public List<Guid> Ids { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteHotelRequest, DeleteHotelCommand>()
                .ConstructUsing(x => new DeleteHotelCommand(x.Ids));
        }
    }
}

