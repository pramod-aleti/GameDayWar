public void GetUserByUsername(string username)
{
    string connectionString = "YourConnectionStringHere";
    string query = "SELECT * FROM Users WHERE Username = @Username";

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (var command = new SqlCommand(query, connection))
        {
            // Use a parameterized query to prevent SQL injection
            command.Parameters.AddWithValue("@Username", username);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"User: {reader["Username"]}");
                }
            }
        }
    }

}
