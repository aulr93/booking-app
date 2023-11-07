namespace Booking.Application.Features.Reports.Models
{
    public class GetIncomeReportVM
    {
        public DateTime BookingDate { get; set; }
        public int TotalRoomBooked { get; set; }
        public int TotalIncome { get; set; }
    }
}
