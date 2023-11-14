namespace Booking.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Username { get; }
        string Role { get; }
    }
}
