namespace Booking.Application.Commons.Models
{
    public class DeleteVM
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
