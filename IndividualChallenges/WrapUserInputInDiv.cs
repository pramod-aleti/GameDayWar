public void WrapUserInputInDiv(string userInput)
{
    // Insecure: Wraps user input directly into a <div> without encoding
    var encodedInput = HttpUtility.HtmlEncode(userInput);
    var content = "<div>" + encodedInput + "</div>";
    Console.WriteLine("Generated content: " + content);
}
