using System;
using System.Security.Cryptography;

namespace DPA.SHOPPING.CORE.Core.Security
{
    internal static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            // Format: {iterations}.{saltBase64}.{keyBase64}
            return string.Join('.', Iterations.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(key));
        }

        public static bool Verify(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            if (providedPassword == null) return false;

            var parts = hashedPassword.Split('.');
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations)) return false;
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }
    }
}
