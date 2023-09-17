using System.Security.Cryptography;
using System.Text;

namespace webapi.Extensions
{
    public static class StringExtensions
    {
        // Compute the SHA256 hash of a string
        public static string ComputeSha256Hash(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
