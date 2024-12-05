using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Text;

class SecureCLI
{
    // Securely retrieve the database connection string and API key
    private static readonly string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    private static readonly string remoteServer = "https://remoteserver.com/api/upload"; // Use HTTPS
    private static readonly string apiKey = Environment.GetEnvironmentVariable("API_KEY");

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Secure CLI!");

        // Prompt user for login credentials
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        // Avoid logging sensitive information
        // File.AppendAllText("log.txt", $"Login attempt: {username}\n");

        // Use parameterized queries to prevent SQL Injection
        string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("Login successful!");
                PerformFileOperation();
                UploadDataToServer(username, password);
            }
            else
            {
                Console.WriteLine("Login failed.");
            }
        }
    }

    static void PerformFileOperation()
    {
        Console.Write("Enter filename to modify: ");
        string fileName = Console.ReadLine();

        // Validate and sanitize user input to prevent path traversal
        string sanitizedFileName = Path.GetFileName(fileName);
        string filePath = Path.Combine("C:\\SecureData\\", sanitizedFileName);

        // Secure file operations
        if (File.Exists(filePath))
        {
            File.AppendAllText(filePath, "Secure data appended.\n");
            Console.WriteLine($"Data appended to {filePath}");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    static void UploadDataToServer(string username, string password)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(remoteServer, content).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data uploaded successfully.");
            }
            else
            {
                Console.WriteLine("Failed to upload data.");
            }
        }
    }
}
