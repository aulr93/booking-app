namespace Booking.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Guid? UserIdInGuid { get; }
    }
}
