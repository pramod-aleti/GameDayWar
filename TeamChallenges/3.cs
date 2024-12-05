using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureWebAPIWithXSSAndFileOperations
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // 1. XSS Vulnerability (Fixed by escaping user input)
        [HttpGet("greet")]
        public IActionResult GreetUser(string username)
        {
            var encodedUsername = System.Net.WebUtility.HtmlEncode(username);
            return Content($"<h1>Welcome, {encodedUsername}</h1>");
        }

        // 2. Insecure File Handling (Avoid storing data in sensitive locations)
        [HttpPost("storeData")]
        public IActionResult StoreData([FromBody] string data)
        {
            string securePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "user_info.txt");
            File.WriteAllText(securePath, data);
            return Ok("Data stored");
        }

        // 3. Weak Cryptography (Use a stronger hashing algorithm)
        [HttpPost("storePassword")]
        public IActionResult StorePassword([FromBody] string password)
        {
            string hashedPassword = SecurePasswordManager.HashPassword(password);
            string securePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "passwords.txt");
            File.WriteAllText(securePath, hashedPassword);
            return Ok("Password stored");
        }

        // 4. CSRF Vulnerability (Implement token validation)
        [HttpPost("updateEmail")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmail([FromBody] string email)
        {
            string securePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "user_email.txt");
            File.WriteAllText(securePath, email);
            return Ok("Email updated");
        }

        // 5. Logging Sensitive Data (Avoid logging sensitive information directly)
        [HttpPost("logUserAction")]
        public IActionResult LogUserAction([FromBody] string action)
        {
            string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "user_actions.log");
            File.AppendAllText(logPath, $"{DateTime.Now}: {action}\n");
            return Ok("Action logged");
        }
    }

    public static class SecurePasswordManager
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
