namespace Booking.Application.Commons.Constants
{
    public static class ApplicationConstant
    {
        public const string TOKEN_AUTHORIZATION_SCHEME = "Bearer";

        public static List<string> RoomType = new List<string>
        {
            "Standar Room",
            "Superior Room",
            "Deluxe Room",
            "Twin Room",
            "Single Room",
            "Double Room",
            "Family Room"
        };
    }

    public static class Role
    {
        public const string ADM = "ADM";
        public const string VST = "VST";
    }
}
