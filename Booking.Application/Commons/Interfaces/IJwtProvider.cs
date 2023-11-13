using Booking.Application.Commons.Authentications;
using Booking.Application.Commons.Authentications.JsonWebTokens;
using System.Security.Claims;

namespace Booking.Application.Commons.Interfaces
{
    public interface IJwtProvider
    {
        JwtOption Options { get; }
       
        string GetJwtToken(List<Claim> claims, string? issuer = null, string? audidence = null);
    }
}
