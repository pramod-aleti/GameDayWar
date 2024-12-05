using System;
using System.IO;

public void SwallowException()
{
    try
    {
        DoSomething(); // This method might throw an exception
    }
    catch (Exception ex)
    {
        // Log the exception or handle it appropriately
        LogException(ex);
    }
}

private void DoSomething()
{
    throw new InvalidOperationException("An error occurred in DoSomething.");
}

private void LogException(Exception ex)
{
    // Example logging to a file
    string logMessage = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}";
    File.AppendAllText("error.log", logMessage);
}
