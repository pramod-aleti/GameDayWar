public void EmbedUserInputInScript(string userInput)
{
    // Secure: Escapes user input before embedding it into a <script> tag
    var encodedUserInput = System.Web.HttpUtility.JavaScriptStringEncode(userInput);
    var html = $"<script>alert('{encodedUserInput}')</script>";
    Console.WriteLine("Generated HTML: " + html);

