namespace Booking.Application.Features.HotelRoomBookings.Models
{
    public class HotelRoomAvailabilityVM
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Floor { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
