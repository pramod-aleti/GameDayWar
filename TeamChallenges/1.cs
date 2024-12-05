using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SecureLoginAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string connectionString = "Server=myServer;Database=myDB;User Id=admin;Password=admin123;";

        // 1. SQL Injection (Use parameterized queries)
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return Ok("Login Successful");
                }
                else
                {
                    return Unauthorized("Login Failed");
                }
            }
        }

        // 2. Secure Password Storage (Hash passwords before storing)
        [HttpPost("storePassword")]
        public IActionResult StorePassword([FromBody] string password)
        {
            string hashedPassword = HashPassword(password);
            System.IO.File.WriteAllText("passwords.txt", hashedPassword); // Storing hashed password
            return Ok("Password stored securely");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // 3. Proper Exception Handling (Avoid logging sensitive info)
        [HttpGet("testError")]
        public IActionResult TestError()
        {
            try
            {
                throw new Exception("Critical Error!"); // Unhandled exception
            }
            catch (Exception ex)
            {
                // Log minimal information
                System.IO.File.AppendAllText("error_log.txt", "An error occurred at " + DateTime.Now);
                return BadRequest("An error occurred");
            }
        }

        // 4. Avoid Hardcoded Credentials (Use secure methods to retrieve credentials)
        [HttpGet("adminAccess")]
        public IActionResult AdminAccess()
        {
            // Retrieve credentials securely (e.g., from a secure vault or environment variables)
            string adminUser = Environment.GetEnvironmentVariable("ADMIN_USER");
            string adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

            if (adminUser == "admin" && adminPassword == "admin123")
            {
                return Ok("Admin Access Granted");
            }
            else
            {
                return Unauthorized("Access Denied");
            }
        }
    }
}
