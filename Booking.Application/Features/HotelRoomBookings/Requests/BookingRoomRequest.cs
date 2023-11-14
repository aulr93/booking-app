using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Reports.Commands;
using Booking.Application.Features.Reports.Requests;

namespace Booking.Application.Features.HotelRoomBookings.Requests
{
    public class BookingRoomRequest : IMapping
    {
        public DateTime Period { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenerateIncomeReportRequest, GenerateIncomeReportCommand>()
                .ConstructUsing(x => new GenerateIncomeReportCommand(x.Period));
        }
    }
}
