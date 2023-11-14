using Booking.Common.Interfaces;
using Booking.WebApi.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.WebApi.Services
{
    public class ApplicationJwtManager
    {
        private readonly WebAppConfig _option;
        private readonly IMachineDateTime _machineDateTime;

        public ApplicationJwtManager(IOptions<WebAppConfig> options, IMachineDateTime machineDateTime)
        {
            _option = options.Value;
            if (string.IsNullOrWhiteSpace(_option.SecretKey))
                throw new ArgumentException("Secret key is null");

            _machineDateTime = machineDateTime;
        }

        public string GenerateJwtToken(List<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(_option.SecretKey);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims: claims);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,

                // generate token that is valid for 7 days
                Expires = _machineDateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_option.SecretKey);
            _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }
    }
}