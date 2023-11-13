using Booking.Application.Commons.Interfaces;
using Booking.Common.Interfaces;

namespace Booking.Application.Commons.Authentications
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IMachineDateTime _machineDateTime;

        public AuthenticationProvider(IJwtProvider jwtProvider, IMachineDateTime machineDateTime)
        {
            _jwtProvider = jwtProvider;
            _machineDateTime = machineDateTime;
        }

        public string GetToken(AuthUser user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var result = new AuthResult
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Role = user.Role,
                Created = _machineDateTime.UtcNow,
                Expired = _machineDateTime.UtcNow.AddSeconds(_jwtProvider.Options.Duration)
            };

            return _jwtProvider.GetJwtToken(result.GetClaims());
        }
    }
}
