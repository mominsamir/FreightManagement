using System;
using System.Text;

namespace FreightManagement.Application.Common.Security
{
    public class PasswordEncoder
    {
        public static void Main(string[] args)
        {
            var hash = ConvertPasswordToHash("Welcome123");
            Console.WriteLine(hash);
        }

        public const string HASH = "TYxNTMxNzczNSwiaWF0IjoxNjE1MzE0MTM1fQ";

        public static string ConvertPasswordToHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return string.Empty;
            password += HASH;
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public static bool ComparePassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            password += HASH;
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes)== passwordHash;
        }
    }
}
