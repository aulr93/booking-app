using Booking.Application.Features.Reports.Commands;
using FluentValidation;

namespace Booking.Application.Features.Reports.Valildators
{
    public class GenerateIncomeReportCommandValidator : AbstractValidator<GenerateIncomeReportCommand>
    {
        public GenerateIncomeReportCommandValidator()
        {
            RuleFor(x => x.Period).NotNull().NotEmpty();
        }
    }
}
