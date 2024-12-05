public void SaveFile(string userInput, byte[] fileContent)
{
    // Sanitize user input to prevent path traversal
    string sanitizedFileName = Path.GetFileName(userInput);
    string uploadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
    
    // Ensure the upload directory exists
    if (!Directory.Exists(uploadDirectory))
    {
        Directory.CreateDirectory(uploadDirectory);
    }

    string filePath = Path.Combine(uploadDirectory, sanitizedFileName);
    File.WriteAllBytes(filePath, fileContent);
    Console.WriteLine($"File saved to {filePath}");
}
