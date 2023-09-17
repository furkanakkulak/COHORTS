using System.Collections.Generic;
using webapi.Extensions;

namespace webapi.Services
{
    public class FakeAuthenticationService
    {
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "user", "pass123" }, // Example username and password (the password is currently hashed directly with SHA256)
            // You can add more users here
        };

        public bool Authenticate(string username, string password)
        {
            if (_users.TryGetValue(username, out string hashedPassword))
            {
                string inputHash = StringExtensions.ComputeSha256Hash(password);
                return StringExtensions.ComputeSha256Hash(hashedPassword) == inputHash;
            }
            return false;
        }
    }
}
