public void CreateAuthCookie(string userInput)
{
    // Validate and sanitize user input
    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Invalid user input.");
        return;
    }

    // Encrypt the user input before setting it in the cookie
    string encryptedValue = Encrypt(userInput);

    // Creating a new cookie with the encrypted user input
    HttpCookie authCookie = new HttpCookie("auth", encryptedValue)
    {
        HttpOnly = true,
        Secure = true
    };
    HttpContext.Current.Response.Cookies.Add(authCookie);
    Console.WriteLine("Cookie has been set.");
}

private string Encrypt(string value)
{
    // Implement your encryption logic here
    // For demonstration purposes, we'll just return the original value
    // In a real application, use a proper encryption algorithm
    return value;
}
