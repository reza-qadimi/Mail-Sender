namespace Rez.Mail.Utilities.Settings;

public class MailSettings : object
{
	public MailSettings() : base()
	{
	}

	public int Port { get; set; }

	public string Username { get; set; }

	public string Host { get; set; }

	public string Password { get; set; }

	public string DisplayName { get; set; }
}
