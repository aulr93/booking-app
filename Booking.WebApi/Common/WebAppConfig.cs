namespace Booking.WebApi.Common
{
    public class WebAppConfig
    {
        public WebAppConfig()
        {
            SecretKey = string.Empty;
            Duration = 86400;
        }

        public string SecretKey { get; set; }
        public int Duration { get; set; }
    }
}
