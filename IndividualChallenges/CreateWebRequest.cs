public void CreateWebRequest(string url)
{
    // Validate and sanitize the URL
    if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
    {
        Console.WriteLine("Invalid URL.");
        return;
    }

    // Ensure the URL uses HTTPS
    var uriBuilder = new UriBuilder(url)
    {
        Scheme = Uri.UriSchemeHttps,
        Port = -1 // Default port for the scheme
    };

    var request = WebRequest.Create(uriBuilder.Uri);
    using (var response = request.GetResponse())
    {
        Console.WriteLine("Response received.");
    }
}
