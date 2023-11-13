using Booking.Application.Features.Administrators.Commands;
using FluentValidation;

namespace Booking.Application.Features.Administrators.Valildators
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty()
                                    .Must(x => !x.Any(char.IsUpper) && !x.Any(char.IsSymbol) && !x.Any(char.IsWhiteSpace))
                                    .WithMessage("Username cannot have uppercase, special character and contains a space");

            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
