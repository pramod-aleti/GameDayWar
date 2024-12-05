public void UseHardcodedSecretKey(string data)
{
    string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
        
        if (string.IsNullOrEmpty(secretKey))
        {
            Console.WriteLine("Secret key is not set.");
            return;
        }

        var hashedKey = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey));
        Console.WriteLine($"Key Derived from Secure Secret: {Convert.ToHexString(hashedKey)}");
}
