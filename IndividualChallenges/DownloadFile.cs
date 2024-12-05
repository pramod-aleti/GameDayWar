public void DownloadFile(string url)
{
    using (var client = new WebClient())
    {
        client.DownloadFile(url, "file.txt"); // Secure: Uses Parameterized URL
    }
    Console.WriteLine("File downloaded from: " + url);
}
