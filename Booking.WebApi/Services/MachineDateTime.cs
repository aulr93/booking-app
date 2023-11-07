using Booking.Common.Interfaces;

namespace Booking.WebApi.Services
{
    public class MachineDateTime : IMachineDateTime
    {
        public const int Hours = 7;
        public const int Minutes = 0;

        public MachineDateTime()
        {
            UtcNowRequest = DateTime.UtcNow;
            ServerTimeRequest = UtcNowRequest.AddHours(Hours).AddMinutes(Minutes);
        }

        public DateTime UtcNowRequest { get; }
        public DateTime ServerTimeRequest { get; }
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime ServerTime => UtcNow.AddHours(Hours).AddMinutes(Minutes);
    }
}
