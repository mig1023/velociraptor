using System.Security.Cryptography;
using System.Text;

namespace velociraptor.ORM
{
    public class Cryptography
    {
        public static string ProtectPassword(string password, out string salt)
        {
            salt = GetByteLine(RandomNumberGenerator.GetBytes(8));
            return CreateSHA1(password + salt);
        }

        public static bool ValidatePassword(string passwordHash, string salt, string password)
        {
            return CreateSHA1(password + salt) == passwordHash;
        }

        private static string GetByteLine(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }

        private static string CreateSHA1(string rawData)
        {
            using (SHA1 sha1Hash = SHA1.Create())
                return GetByteLine(sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData)));
        }
    }
}
