using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Models;
using Booking.Application.Features.Authorizations.Models;
using Booking.WebApi.Services;
using System.Security.Claims;

namespace Booking.WebApi
{
    public static class TokenBuilder
    {
        public static AuthenticationResponse Build(ApplicationJwtManager applicationJwtManager, LoginVM user)
        {
            if (applicationJwtManager is null)
                throw new ArgumentNullException(nameof(applicationJwtManager));

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ApplicationClaimConstant.UserId, user.Id.ToString()));
            claims.Add(new Claim(ApplicationClaimConstant.Username, user.Username));
            claims.Add(new Claim(ApplicationClaimConstant.Role, user.Role));

            return new AuthenticationResponse(applicationJwtManager.GenerateJwtToken(claims));
        }
    }
}
