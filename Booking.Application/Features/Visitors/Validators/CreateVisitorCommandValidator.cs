using Booking.Application.Features.Visitors.Commands;
using FluentValidation;

namespace Booking.Application.Features.Administrators.Valildators
{
    public class CreateVisitorCommandValidator : AbstractValidator<CreateVisitorCommand>
    {
        public CreateVisitorCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty()
                                    .Must(x => !x.Any(char.IsUpper) && !x.Any(char.IsSymbol) && !x.Any(char.IsWhiteSpace))
                                    .WithMessage("Username cannot have uppercase, special character and contains a space");

            RuleFor(x => x.Nik).NotNull().NotEmpty();

            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleFor(x => x.Email).NotNull().NotEmpty();

            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
