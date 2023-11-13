using Booking.Application.Commons.Constants;

namespace Booking.Application.Features.Authorizations.Models
{
    public class LoginVM
    {
        public LoginVM()
        {
            TokenScheme = ApplicationConstant.TOKEN_AUTHORIZATION_SCHEME;
            Token = string.Empty;
        }

        public string Token { get; set; }

        public string TokenScheme { get; }
    }
}
