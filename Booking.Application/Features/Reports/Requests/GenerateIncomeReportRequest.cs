using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Reports.Commands;

namespace Booking.Application.Features.Reports.Requests
{
    public class GenerateIncomeReportRequest : IMapping
    {
        public DateTime Period { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenerateIncomeReportRequest, GenerateIncomeReportCommand>()
                .ConstructUsing(x => new GenerateIncomeReportCommand(x.Period));
        }
    }
}