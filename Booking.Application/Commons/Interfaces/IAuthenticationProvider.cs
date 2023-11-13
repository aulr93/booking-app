using Booking.Application.Commons.Authentications;

namespace Booking.Application.Commons.Interfaces
{
    public interface IAuthenticationProvider
    {
        string GetToken(AuthUser user);
    }
}
