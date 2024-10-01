namespace AssignmentCms.Models;

public class ConfirmationEmailModel
{
	public string To { get; set; } = null!;
	public string Subject { get; set; } = "Your message has been received";
	public string HtmlBody { get; set; } = "<html><body><h3>Your message has been recieved</h3><p>We will get back to you shortly</p></body></html>";
	public string PlainText { get; set; } = "Your message has been recieved. We will get back to you shortly";
}
