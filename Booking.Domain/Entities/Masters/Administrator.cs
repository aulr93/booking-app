namespace Booking.Domain.Entities.Masters
{
    public class Administrator : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] Salt { get; set; } = new byte[0];
        public string HashedPassword { get; set; } = string.Empty;
    }
}
