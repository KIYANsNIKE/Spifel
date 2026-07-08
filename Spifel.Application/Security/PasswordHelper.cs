using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Spifel.Application.Security
{
    public static class PasswordHelper
    {

        /// Size of the salt in bytes

        private const int SaltSize = 16;


        /// Size of the hash in bytes

        private const int HashSize = 32;


        /// Number of threads to use for parallel computation

        private const int DegreeOfParallelism = 2;


        /// Number of iterations for the Argon2id algorithm

        private const int Iterations = 3;


        /// Memory size in KB to use

        private const int MemorySize = 65536; // 19 MB


        /// Generates a secure hash of a password using Argon2id with a random salt

        /// <param name="password">The plain text password to hash</param>
        /// <returns>A Base64 string containing the combined salt and hash</returns>
        /// <exception cref="ArgumentNullException">Thrown when password is null</exception>
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Create hash
            byte[] hash = HashPassword(password, salt);

            // Combine salt and hash
            var combinedBytes = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
            Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);

            // Convert to base64 for storage
            return Convert.ToBase64String(combinedBytes);
        }


        /// Generates a password hash using Argon2id with a specific salt

        /// <param name="password">The plain text password</param>
        /// <param name="salt">The salt to use for hashing</param>
        /// <returns>A byte array containing the password hash</returns>
        private static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                Iterations = Iterations,
                MemorySize = MemorySize
            };
            return argon2.GetBytes(HashSize);
        }


        /// Verifies if a password matches a stored hash

        /// <param name="password">The plain text password to verify</param>
        /// <param name="hashedPassword">The stored hash in Base64 format</param>
        /// <returns>True if the password matches the hash, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown when password or hashedPassword are null</exception>
        /// <exception cref="FormatException">Thrown when hashedPassword is not in valid Base64 format</exception>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Decode the stored hash
            byte[] combinedBytes = Convert.FromBase64String(hashedPassword);

            // Extract salt and hash
            byte[] salt = new byte[SaltSize];
            byte[] hash = new byte[HashSize];
            Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
            Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

            // Compute hash for the input password
            byte[] newHash = HashPassword(password, salt);

            // Compare the hashes
            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }
    }
}

