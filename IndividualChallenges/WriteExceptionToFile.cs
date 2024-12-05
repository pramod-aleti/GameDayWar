using NLog;

public class ExceptionLogger
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public void WriteExceptionToFile(Exception ex)
    {
        try
        {
            DoSomethingRisky();
        }
        catch (Exception caughtEx)
        {
            logger.Error(caughtEx, "An error occurred while doing something risky.");
        }
    }
}
