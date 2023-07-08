using System.Security.Cryptography;

namespace NHMatsumotoDemo.Database.Password
{
    public static class PasswordService
    {
        private const int SaltSize = 16;
        private const int HashSize = 20; // SHA-1 hash size
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with PBKDF2
            byte[] hash = GeneratePBKDF2(password, salt, Iterations, HashSize);

            // Combine the salt and hash into a single string for storage
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Convert the hashed password from string to byte array
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt and hash from the combined byte array
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            byte[] hash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, hash, 0, HashSize);

            // Compute the hash of the password and verify it against the stored hash
            byte[] computedHash = GeneratePBKDF2(password, salt, Iterations, HashSize);
            return SlowEquals(hash, computedHash);
        }

        private static byte[] GeneratePBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        // Compare two byte arrays in length-constant time
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
