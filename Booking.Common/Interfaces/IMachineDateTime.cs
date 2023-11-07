namespace Booking.Common.Interfaces
{
    public interface IMachineDateTime
    {
        DateTime UtcNowRequest { get; }
        DateTime ServerTimeRequest { get; }
        DateTime UtcNow { get; }
        DateTime ServerTime { get; }
    }
}
