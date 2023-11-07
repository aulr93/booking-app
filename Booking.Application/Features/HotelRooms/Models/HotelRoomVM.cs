namespace Booking.Application.Features.HotelRooms.Models
{
    public class HotelRoomVM
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Floor { get; set; }
        public int Price { get; set; }
    }
}
