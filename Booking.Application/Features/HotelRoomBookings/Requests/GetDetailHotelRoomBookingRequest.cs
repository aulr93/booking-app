using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRoomBookings.Queries;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class GetDetailHotelRoomBookingRequest : IMapping
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetDetailHotelRoomBookingRequest, GetDetailHotelRoomBookingQuery>()
                .ConstructUsing(x => new GetDetailHotelRoomBookingQuery(x.Id));
        }
    }
}
