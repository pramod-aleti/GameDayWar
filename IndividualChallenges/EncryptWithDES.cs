public void EncryptWithAES(byte[] data, byte[] key, byte[] iv)
{
    using (var aes = Aes.Create())
    {
        aes.Key = key; // Key length should be 16, 24, or 32 bytes
        aes.IV = iv;   // IV length should be 16 bytes

        using (var encryptor = aes.CreateEncryptor())
        {
            var encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            Console.WriteLine($"Encrypted Data (AES): {Convert.ToBase64String(encryptedData)}");
        }
    }
}
