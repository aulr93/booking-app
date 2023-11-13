using Booking.Domain.Entities.Masters;

namespace Booking.Domain.Entities.Transactions
{
    public class HotelRoomBooking : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid VisitorId { get; set; }
        public DateTime Date { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ActualCheckInDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }

        public virtual HotelRoom HotelRoom { get; set; } = null!;
        public virtual Visitor Visitor { get; set; } = null!;
    }
}
