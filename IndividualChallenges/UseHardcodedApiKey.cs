public void UseHardcodedApiKey()
{
    string apiKey = Environment.GetEnvironmentVariable("API_KEY");     
    if (string.IsNullOrEmpty(apiKey))
    {
        Console.WriteLine("API key is not set.");
        return;
    }

    Console.WriteLine("Using API key: " + apiKey);

    // Simulate API usage
    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        var response = await client.GetAsync("https://api.example.com/data");
        Console.WriteLine("Response: " + response.StatusCode);
    }
}
