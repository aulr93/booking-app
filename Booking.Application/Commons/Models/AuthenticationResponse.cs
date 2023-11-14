namespace Booking.Application.Commons.Models
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse()
        {
            Scheme = "bearer";
            Token = string.Empty;
            RefreshToken = string.Empty;
            RefreshTokenExpiredOn = null;
        }

        public AuthenticationResponse(string token) : this()
        {
            Token = token;
        }

        public AuthenticationResponse(string scheme, string token, string refreshToken, DateTime? refreshTokenExpiredOn)
        {
            Scheme = scheme;
            Token = token;
            RefreshToken = refreshToken;
            RefreshTokenExpiredOn = refreshTokenExpiredOn;
        }

        public string Scheme { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredOn { get; set; }
    }

}
