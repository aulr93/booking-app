using Booking.Domain.Entities.Transactions;

namespace Booking.Domain.Entities.Masters
{
    public class Visitor : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NIK { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] Salt { get; set; } = new byte[0];
        public string HashedPassword { get; set; } = string.Empty;
    
        public virtual ICollection<HotelRoomBooking> HotelRoomBookings { get; set; } = null!;
    }
}
