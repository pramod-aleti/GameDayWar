public void CreateSecureTcpClient(string host)
{
    using (var tcpClient = new TcpClient(host, 443)) // Secure: Uses HTTPS port
    using (var sslStream = new SslStream(tcpClient.GetStream(), false))
    {
        // Authenticate the server
        sslStream.AuthenticateAsClient(host);

        Console.WriteLine("Secure TCP client connected to: " + host);

        // Communication with the server goes here using sslStream
    }
}
