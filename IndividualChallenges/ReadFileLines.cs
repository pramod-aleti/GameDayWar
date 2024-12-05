public void ReadFileLines(string userInput)
{
    // Reads all lines from a file based on Validated user input
    if (File.Exists(userInput))
    {
        var lines = File.ReadAllLines(userInput);
        Console.WriteLine($"Read {lines.Length} lines from file: {userInput}");
    }
    else
    {
        Console.WriteLine($"File does not exist: {userInput}");
    }
    
}
