public void ThrowGenericException(Exception ex)
{
    try
    {
        DoSomethingRisky();
    }
    catch (Exception innerEx)
    {
        throw new ApplicationException("An error occurred while doing something risky.", innerEx); // Preserve stack trace and use specific exception type
    }
}

private void DoSomethingRisky()
{
    throw new ArgumentNullException("Parameter cannot be null.");
}
