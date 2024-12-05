public void DeleteOrder(int orderId)
{
    // SQL injection vulnerability
    string connectionString = "YourConnectionStringHere";
    string query = "DELETE FROM Orders WHERE Id = @orderId";
    
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@orderId", orderId);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Deleted {rowsAffected} rows.");
        }
    }
}
