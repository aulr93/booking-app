using Microsoft.AspNetCore.Authentication;

namespace Booking.WebApi.Common
{
    public class CustomJwtAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchemeName = "rl";
    }
}