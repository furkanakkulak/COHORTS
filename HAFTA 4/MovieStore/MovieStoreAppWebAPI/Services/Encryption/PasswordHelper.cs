using System.Security.Cryptography;
using System.Text;

using static System.Net.Mime.MediaTypeNames;

namespace MovieStoreAppWebAPI.Services.Encryption
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Function to generate a random salt value
        /// </summary>
        /// <param name="saltLength"></param>
        /// <returns></returns>
        private static byte[] GenerateSalt()
        {
            const int saltLength = 32;

            byte[] salt = new byte[saltLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Function to create the password hash using SHA-256 with a salt
        /// </summary>
        /// <param name="password">Password to be hashed</param>
        /// <param name="salt">Stored salt value to verify password</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password, out string salt)
        {
            
            byte[] saltBytes = GenerateSalt();
            salt = Convert.ToBase64String(saltBytes);

            // Convert the password string to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Concatenate the password and salt bytes
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);

            var sha256 = SHA256.Create();

            byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Function to verify the password against the stored hash and salt
        /// </summary>
        /// <param name="password">Password to be verified with saved hashed password</param>
        /// <param name="storedHash">Hashed password that already hashed</param>
        /// <param name="storedSalt">Salt value to verify hashed password</param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Convert the stored salt back to bytes
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Convert the password string to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Concatenate the password and salt bytes
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);

            var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            string computedHash = Convert.ToBase64String(hashBytes);

            // Compare the computed hash with the stored hash
            return computedHash.Equals(storedHash);
        }
    }
}
