public void AuthenticateUser(string password)
{
    string storedHashedPassword = Environment.GetEnvironmentVariable("HASHED_ADMIN_PASSWORD");

    if (VerifyPassword(password, storedHashedPassword))
    {
        GrantAccess();
    }
    else
    {
        Console.WriteLine("Access Denied.");
    }
}

private bool VerifyPassword(string password, string storedHashedPassword)
{
    using (var sha256 = SHA256.Create())
    {
        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        return hashedPassword == storedHashedPassword;
    }
}

private void GrantAccess()
{
    Console.WriteLine("Access Granted!");
}