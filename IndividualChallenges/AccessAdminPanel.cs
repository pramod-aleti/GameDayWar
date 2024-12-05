public void AccessAdminPanel(string username)
    {
        string adminUsername = Environment.GetEnvironmentVariable("ADMIN_USERNAME");

        if (username == adminUsername)
        {
            Console.WriteLine("Access to Admin Panel Granted!");
        }
        else
        {
            Console.WriteLine("Access Denied.");
        }
    }