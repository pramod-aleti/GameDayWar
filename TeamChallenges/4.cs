using System;
using System.IO;
using System.Data.SqlClient;

namespace SecureConsoleApp
{
    class Program
    {
        private static string connectionString = "Server=myServer;Database=myDB;User Id=admin;Password=admin123;";

        static void Main(string[] args)
        {
            // 1. SQL Injection (Fixed with parameterised queries)
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("Login Successful");
                }
                else
                {
                    Console.WriteLine("Login Failed");
                }
            }

            // 2. Insecure File Handling (Avoid writing sensitive information to insecure locations)
            string securePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "user_data.txt");
            File.WriteAllText(securePath, "Sensitive Information");

            // 3. Hardcoded Credentials (Use secure methods to handle sensitive information)
            string adminPassword = GetAdminPassword();
            if (adminPassword == "admin123")
            {
                Console.WriteLine("Admin access granted");
            }
            else
            {
                Console.WriteLine("Admin access denied");
            }

            // 4. Logging Sensitive Information (Avoid logging sensitive information directly)
            string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "login_attempts.txt");
            File.AppendAllText(logPath, $"{username} attempted to log in at {DateTime.Now}\n");
        }

        private static string GetAdminPassword()
        {
            // Implement a secure method to retrieve the admin password
            return "admin123"; // Placeholder, replace with actual secure retrieval method
        }
    }
}
