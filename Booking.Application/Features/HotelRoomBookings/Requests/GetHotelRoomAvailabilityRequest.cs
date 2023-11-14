using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRoomBookings.Queries;
using static Booking.Application.Features.HotelRoomBookings.Queries.GetHotelRoomAvailabilityQuery;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class GetHotelRoomAvailabilityRequest : IMapping
    {
        public DateTime Date { get; set; }
        public EnumGetData GetData { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetHotelRoomAvailabilityRequest, GetHotelRoomAvailabilityQuery>()
                .ConstructUsing(x => new GetHotelRoomAvailabilityQuery(x.Date, x.GetData));
        }
    }
}
