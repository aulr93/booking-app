namespace Booking.Domain.Entities.Transactions
{
    public class HotelRoomBooking : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public string NIK { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ActualCheckInDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }

        public virtual HotelRoom HotelRoom { get; set; } = null!;
    }
}
