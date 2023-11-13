namespace Booking.Application.Commons.Authentications
{
    public class AuthResult : AuthUser
    {
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
    }
}
