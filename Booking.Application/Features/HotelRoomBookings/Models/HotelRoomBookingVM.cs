using Booking.Application.Features.HotelRooms.Models;

namespace Booking.Application.Features.HotelRoomBookings.Models
{
    public class HotelRoomBookingVM
    {
        public Guid Id { get; set; }
        public HotelRoomVM RoomDetail { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public string NIK { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ActualCheckInDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
    }
}
