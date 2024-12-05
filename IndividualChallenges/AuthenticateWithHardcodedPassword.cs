public void AuthenticateWithHardcodedPassword()
{
    string password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD"); // Secure: Use environment variable
    Console.WriteLine("Authenticating with environment password.");

    // Simulate authentication
    if (password == "P@ssword!")
    {
        Console.WriteLine("Authentication successful.");
    }
    else
    {
        Console.WriteLine("Authentication failed.");
    }
}
