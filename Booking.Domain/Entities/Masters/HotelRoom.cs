using Booking.Domain.Entities.Transactions;

namespace Booking.Domain.Entities
{
    public class HotelRoom : AuditableEntity
    {
        public HotelRoom()
        {
            Id = Guid.NewGuid();
            RoomNumber = string.Empty;
            Type = string.Empty;
        }

        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int Floor { get; set; }

        public virtual ICollection<HotelRoomBooking> HotelRoomBookings { get; set; } = null!;
    }
}
