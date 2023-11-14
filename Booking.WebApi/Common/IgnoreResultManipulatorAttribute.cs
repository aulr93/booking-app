namespace Booking.WebApi.Common
{
    public class IgnoreResultManipulatorAttribute : Attribute
    {
        public bool IgnoreSetResult { get; set; }
    }
}
