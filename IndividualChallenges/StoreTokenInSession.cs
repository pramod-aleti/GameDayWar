using System;
using System.Security.Cryptography;
using System.Text;

public void StoreTokenInSession(string token)
{
    // Encrypt the token before storing it in session
    string encryptedToken = EncryptToken(token);
    Session["AuthToken"] = encryptedToken;
    Console.WriteLine("Token stored in session.");
}

private string EncryptToken(string token)
{
    using (Aes aes = Aes.Create())
    {
        aes.Key = Encoding.UTF8.GetBytes("A very strong key"); // Use a secure key
        aes.IV = Encoding.UTF8.GetBytes("An init vector"); // Use a secure IV

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(token);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
