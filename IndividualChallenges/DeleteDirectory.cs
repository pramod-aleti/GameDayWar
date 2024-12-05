public void DeleteDirectory(string folderName)
{
    // Deletes a directory based on unvalidated user input
    if (Directory.Exists("C:\\Sensitive\\" + folderName)
    {
        Directory.Delete("C:\\Sensitive\\" + folderName, true);
        Console.WriteLine($"Deleted directory: C:\\Sensitive\\{folderName}");
    }
    else
    {
        Console.WriteLine($"Deleted directory: C:\\Sensitive\\{folderName}");
    }
    
}
