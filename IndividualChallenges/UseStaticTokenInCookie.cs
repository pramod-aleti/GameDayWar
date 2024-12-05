public void UseSecureTokenInCookie()
    {
        // Generate a secure random token
        var token = GenerateSecureToken();
        
        // Create a cookie with the secure token
        HttpCookie authCookie = new HttpCookie("auth", token);
        authCookie.HttpOnly = true; // Mitigates XSS attacks
        authCookie.Secure = true;   // Ensures the cookie is sent over HTTPS
        Response.Cookies.Add(authCookie);
        
        Console.WriteLine("Secure authentication token added to cookies.");
    }

    private string GenerateSecureToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[32];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
