using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRoomBookings.Queries;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class GetHotelRoomAvailabilityRequest : IMapping
    {
        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetHotelRoomAvailabilityRequest, GetHotelRoomAvailabilityQuery>()
                .ConstructUsing(x => new GetHotelRoomAvailabilityQuery(x.Date));
        }
    }
}
