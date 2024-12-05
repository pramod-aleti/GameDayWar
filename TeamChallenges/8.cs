using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SecureCryptographyLibrary
{
    public class SecureCryptography
    {
        // 1. SHA-256 Hashing (Strong Cryptography)
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // 2. Encrypting sensitive data before storing it
        public static void StoreData(string data)
        {
            string encryptedData = EncryptDataWithAES(data);
            File.WriteAllText("sensitiveData.txt", encryptedData); // Storing data securely
        }

        // 3. Secure Token Generation
        public static string GenerateToken()
        {
            byte[] tokenBytes = new byte[32]; // Use 32 bytes for a stronger token
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
        }

        // 4. Using strong ciphers (AES)
        public static string EncryptDataWithAES(string data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("A very strong key"); // Use a secure key
                aes.IV = Encoding.UTF8.GetBytes("An init vector"); // Use a secure IV

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    return Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                }
            }
        }

        // 5. Encrypting session tokens before storing them
        public static void StoreSessionToken(string token)
        {
            string encryptedToken = EncryptDataWithAES(token);
            File.AppendAllText("sessionTokens.txt", encryptedToken); // Storing session tokens securely
        }
    }
}
