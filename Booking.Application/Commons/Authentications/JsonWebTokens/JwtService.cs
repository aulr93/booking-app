using Booking.Application.Commons.Interfaces;
using Booking.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Application.Commons.Authentications.JsonWebTokens
{
    public class JwtService : IJwtProvider
    {
        public JwtOption Options { get; }
        private readonly IMachineDateTime _dateTime;

        public JwtService(IOptions<JwtOption> options, IMachineDateTime dateTime)
        {
            Options = options.Value;
            _dateTime = dateTime;
        }

        public string GetJwtToken(List<Claim> claims, string? issuer = null, string? audience = null)
        {
            if (claims is null || claims.Count < 1)
                throw new ArgumentNullException(nameof(claims));

            var utcNow = _dateTime.UtcNow;

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Options.SecretKey));
            SigningCredentials credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(Options.Duration),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
