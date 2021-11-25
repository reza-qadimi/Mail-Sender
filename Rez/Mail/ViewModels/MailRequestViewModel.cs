namespace Rez.Mail.ViewModels;

public class MailRequestViewModel : object
{
	public MailRequestViewModel() : base()
	{
	}

	public string To { get; set; }

	public string Body { get; set; }

	public string Subject { get; set; }

	public System.Collections.Generic.IList
		<Microsoft.AspNetCore.Http.IFormFile> Attachments
	{ get; set; }
}
