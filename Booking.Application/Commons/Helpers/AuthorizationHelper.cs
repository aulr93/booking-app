using System.Text;

namespace Booking.Application.Commons.Helpers
{
    public static class AuthorizationHelper
    {
        public static string GetRandomPassword()
        {
            Random random = new Random();
            int size = 8;
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string numerics = "0123456789";
            string specials = "!@#$%^&*_?><";
            Dictionary<int, char> reserved = new Dictionary<int, char>();

            while (reserved.Count < 3)
            {
                int key = random.Next(1, size);
                switch (reserved.Count)
                {
                    case 0:
                        if (!reserved.ContainsKey(key))
                            reserved.Add(key, letters.ToUpper()[random.Next(0, letters.Length - 1)]);
                        break;
                    case 1:
                        if (!reserved.ContainsKey(key))
                            reserved.Add(key, numerics[random.Next(0, numerics.Length - 1)]);
                        break;
                    case 2:
                        if (!reserved.ContainsKey(key))
                            reserved.Add(key, specials[random.Next(0, specials.Length - 1)]);
                        break;
                    default:
                        break;
                }
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= size; i++)
            {
                if (reserved.ContainsKey(i))
                    builder.Append(reserved[i]);
                else
                    builder.Append(letters[random.Next(0, letters.Length - 1)]);
            }
            return builder.ToString();
        }
    }
}
