using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Reports.Queries;

namespace Booking.Application.Features.Reports.Requests
{
    public class GetIncomeReportRequest : IMapping
    {
        public DateTime Period { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetIncomeReportRequest, GetIncomeReportQuery>()
                .ConstructUsing(x => new GetIncomeReportQuery(x.Period));
        }
    }
}
