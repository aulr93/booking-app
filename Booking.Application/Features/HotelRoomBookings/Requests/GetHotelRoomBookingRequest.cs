using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRoomBookings.Queries;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class GetHotelRoomBookingRequest : IMapping
    {
        public DateTime Date { get; set; }
        public EnumGetRoomData GetRoomData { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetHotelRoomBookingRequest, GetHotelRoomBookingQuery>()
                .ConstructUsing(x => new GetHotelRoomBookingQuery(x.Date, x.GetRoomData));
        }
    }
}
