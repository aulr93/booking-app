namespace Booking.Application.Commons.Authentications.JsonWebTokens
{
    public class JwtOption
    {
        public const string AuthenticationScheme = "JwtBearer";

        public JwtOption()
        {
            SecretKey = string.Empty;
            Duration = 259200;
        }

        /// <summary>
        /// Secret key minimum 16 length
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// Duration in second. Default value is 3 days (259200 seconds)
        /// </summary>
        public int Duration { get; set; }
    }
}
