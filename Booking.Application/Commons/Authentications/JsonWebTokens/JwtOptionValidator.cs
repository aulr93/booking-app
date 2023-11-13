using FluentValidation;

namespace Booking.Application.Commons.Authentications.JsonWebTokens
{
    public class JwtOptionValidator : AbstractValidator<JwtOption>
    {
        public JwtOptionValidator()
        {
            RuleFor(x => x.SecretKey).NotEmpty();
        }
    }
}
