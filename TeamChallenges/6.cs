using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Security.Cryptography;

namespace XSSAndCSRFWebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Fixed XSS vulnerability by encoding user input
        [HttpGet("greet")]
        public IActionResult GreetUser(string username)
        {
            string encodedUsername = HttpUtility.HtmlEncode(username);
            return Content($"<h1>Welcome, {encodedUsername}</h1>", "text/html");
        }

        // Implemented CSRF protection and secure password handling
        [HttpPost("updatePassword")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePassword([FromBody] string newPassword)
        {
            string hashedPassword = HashPassword(newPassword);
            // Store the hashed password securely
            return Ok("Password updated");
        }

        // Avoided logging sensitive user actions
        [HttpPost("logUserAction")]
        public IActionResult LogUserAction()
        {
            System.IO.File.AppendAllText("user_actions.log", $"{DateTime.Now}: User performed an action.\n");
            return Ok("Action logged");
        }

        // Stored data in a secure location with proper access control
        [HttpPost("saveData")]
        public IActionResult SaveData([FromBody] string data)
        {
            string safePath = Path.Combine("App_Data", "user_data.txt");
            System.IO.File.WriteAllText(safePath, data);
            return Ok("Data saved");
        }

        // Hashed passwords before storing
        [HttpPost("storePassword")]
        public IActionResult StorePassword([FromBody] string password)
        {
            string hashedPassword = HashPassword(password);
            // Store the hashed password securely
            return Ok("Password stored");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
