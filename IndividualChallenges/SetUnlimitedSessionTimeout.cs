public void SetLimitedSessionTimeout()
{
    // Set a reasonable session timeout
    Session.Timeout = 20; // Set to 20 minutes or another appropriate value
    Console.WriteLine("Session timeout set to a limited duration.");
}
