public void GetProductByName(string productName)
{
    // SQL injection vulnerability
    string query = "SELECT * FROM Products WHERE Name = @productName";
    using (var context = new YourDbContext())
    {
        var products = context.Database.SqlQuery<Product>(query, new SqlParameter("@productName", productName)).ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}");
        }
    }
}
