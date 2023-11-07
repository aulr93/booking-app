namespace Booking.Domain.Entities
{
    public class AuditableEntity
    {
        public AuditableEntity()
        {
            UserIn = string.Empty;
            UserUp = string.Empty;
        }

        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}
