using System.Security.Cryptography;
using System.Text;

namespace Booking.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Create new hash string using SHA256
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSHA256(this string s)
        {
            using (SHA256 shaManager = new SHA256Managed())
            {
                string hash = string.Empty;
                byte[] bytes = shaManager.ComputeHash(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));
                foreach (byte b in bytes)
                {
                    hash += b.ToString("x2");
                }

                return hash;
            }
        }

        /// <summary>
        /// Create new hash string using SHA512
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSHA512(this string s)
        {
            using (SHA512 shaManager = new SHA512Managed())
            {
                string hash = string.Empty;
                byte[] bytes = shaManager.ComputeHash(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));
                foreach (byte b in bytes)
                {
                    hash += b.ToString("x2");
                }

                return hash;
            }
        }
    }
}
