using Booking.Application.Features.Reports.Queries;
using FluentValidation;

namespace Booking.Application.Features.Reports.Valildators
{
    public class GetIncomeReportQueryValidator : AbstractValidator<GetIncomeReportQuery>
    {
        public GetIncomeReportQueryValidator()
        {
            RuleFor(x => x.Period).NotNull().NotEmpty();
        }
    }
}
