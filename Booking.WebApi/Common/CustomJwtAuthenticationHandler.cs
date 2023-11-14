using Booking.Application.Common.Exceptions;
using Booking.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Booking.WebApi.Common
{
    public class CustomJwtAuthenticationHandler : AuthenticationHandler<CustomJwtAuthenticationOptions>
    {
        private readonly ApplicationJwtManager _jwtManager;

        public CustomJwtAuthenticationHandler(
            IOptionsMonitor<CustomJwtAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApplicationJwtManager jwtManager) : base(options, logger, encoder, clock)
        {
            _jwtManager = jwtManager;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var reqHeader = Context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(reqHeader)) throw new UnauthorizeException();

            string token = reqHeader.Split(" ").Last();

            if (string.IsNullOrEmpty(token)) throw new UnauthorizeException();

            try
            {
                JwtSecurityToken jwtToken = _jwtManager.ValidateToken(token);

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims: jwtToken.Claims, authenticationType: CustomJwtAuthenticationOptions.DefaultSchemeName);

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(userIdentity), Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch
            {
                throw new UnauthorizeException();
            }
        }
    }
}