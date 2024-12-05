public void AddUserInputToCookie(string userInput)
{
    // Validate and sanitize user input
    if (IsValidInput(userInput))
    {
        string sanitizedInput = HttpUtility.HtmlEncode(userInput);
        Response.Cookies.Add(new HttpCookie("SessionID", sanitizedInput));
        Console.WriteLine("Cookie added with user input: " + sanitizedInput);
    }
    else
    {
        Console.WriteLine("Invalid user input.");
    }
}

private bool IsValidInput(string input)
{
    // Example validation: input should be alphanumeric and between 1 and 20 characters
    return Regex.IsMatch(input, @"^[a-zA-Z0-9]{1,20}$");
}