public void DeleteFile(string userInput)
{
    // Deletes a file based on unvalidated user input
    if (File.Exists(userInput))
    {
        File.Delete(userInput); 
        Console.WriteLine($"Deleted file: {userInput}");
    }
    else
    {
        Console.WriteLine($"File does not exist: {userInput}");
    }
    
}
