using System.Security.Cryptography;
using System.Text;

namespace MoviesAPI.Utils
{
    public class Utilities
    {
        public Utilities() { }

        public String GetSHA256HMACHash(String text, String key)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
