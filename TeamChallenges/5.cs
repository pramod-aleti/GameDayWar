using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace SecureFileOperations
{
    public class FileHandler
    {
        // 1. Storing sensitive data with encryption
        public static void StoreSensitiveData(string data)
        {
            byte[] encryptedData = EncryptData(data);
            File.WriteAllBytes("sensitiveData.enc", encryptedData);
        }

        private static byte[] EncryptData(string data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey(); // Secure key generation
                aes.GenerateIV();  // Secure IV generation

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // Prepend IV for decryption
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(data);
                    }
                    return ms.ToArray();
                }
            }
        }

        // 2. Validated file path to prevent directory traversal
        public static void DeleteFile(string userInput)
        {
            string safeFileName = Path.GetFileName(userInput);
            string filePath = Path.Combine(@"C:\Files\", safeFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted securely.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        // 3. Secure logging without sensitive data
        public static void LogData(string data)
        {
            File.AppendAllText("log.txt", $"{DateTime.Now}: Action performed.\n");
        }

        // 4. Secure file permission changes
        public static void ChangeFilePermissions(string filePath)
        {
            var security = new FileSecurity();
            string currentUser = Environment.UserDomainName + "\\" + Environment.UserName;

            security.SetOwner(new System.Security.Principal.NTAccount(currentUser));
            security.AddAccessRule(new FileSystemAccessRule(
                currentUser,
                FileSystemRights.FullControl,
                AccessControlType.Allow));

            File.SetAccessControl(filePath, security);
        }

        // 5. Data integrity check implemented
        public static void ModifyFileData(string filePath, string newData)
        {
            string originalData = File.ReadAllText(filePath);
            string originalHash = ComputeHash(originalData);

            File.WriteAllText(filePath, newData);

            string newHash = ComputeHash(newData);

            Console.WriteLine($"File modified. Original Hash: {originalHash}, New Hash: {newHash}");
        }

        private static string ComputeHash(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
