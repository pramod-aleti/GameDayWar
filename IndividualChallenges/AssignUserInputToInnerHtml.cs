public void AssignUserInputToInnerHtml(string userInput)
{
    // Insecure: Assigns raw user input to InnerHtml
    string sanitizedInput = HttpUtility.HtmlEncode(userInput);

    var res = new System.Web.UI.HtmlControls.HtmlGenericControl();
    // res.InnerHtml = userInput; // Vulnerable to XSS
    string script = $"document.getElementById('elementId').innerHTML = '{sanitizedInput}';";

    Console.WriteLine("Set InnerHtml to: " + userInput);
}
