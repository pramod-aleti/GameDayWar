public void GetUserData(int id)
{
    string connectionString = "YourConnectionStringHere";
    string query = "SELECT * FROM Users WHERE id = @Id";

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (var command = new SqlCommand(query, connection))
        {
            // Use a parameterized query to prevent SQL injection
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"User ID: {reader["Id"]}, Username: {reader["Username"]}");
                }
            }
        }
    }

}
