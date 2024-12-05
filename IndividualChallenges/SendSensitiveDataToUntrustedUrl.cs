public void SendSensitiveDataToTrustedUrl(string sensitiveData)
{
    // Secure: Sends sensitive data to a trusted URL using HTTPS and POST method
    var request = WebRequest.Create("https://trusted.com/secure-endpoint");
    request.Method = "POST";
    request.ContentType = "application/x-www-form-urlencoded";

    string postData = "data=" + Uri.EscapeDataString(sensitiveData);
    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

    using (var dataStream = request.GetRequestStream())
    {
        dataStream.Write(byteArray, 0, byteArray.Length);
    }

    using (var response = request.GetResponse())
    {
        Console.WriteLine("Data sent securely.");
    }
}
