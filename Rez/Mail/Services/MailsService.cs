namespace Rez.Mail.Services;

public class MailsService : object, IMailsService
{
	public MailsService
		(Utilities.Settings.MailSettings mainSettings) : base()
	{
		MailSettings = mainSettings;
	}

	public MailsService(
		Microsoft.Extensions.Options.IOptions
		<Utilities.Settings.MailSettings> options) : base()
	{
		MailSettings = options.Value;
	}

	protected Utilities.Settings.MailSettings MailSettings { get; }

	public
		async
		System.Threading.Tasks.Task
		SendEmailAsync(
		ViewModels.MailRequestViewModel viewModel,
		System.Threading.CancellationToken cancellationToken = default)
	{
		var email = new MimeKit.MimeMessage
		{
			Subject = viewModel.Subject,
			Sender = MimeKit.MailboxAddress.Parse(text: MailSettings.Username),
		};

		email.To.Add(MimeKit.MailboxAddress.Parse(text: viewModel.To));

		var builder = new MimeKit.BodyBuilder();

		if (viewModel.Attachments is not null)
		{
			byte[] fileBytes;

			foreach (var item in viewModel.Attachments)
			{
				if (item.Length > 0)
				{
					using (var memoryStream = new System.IO.MemoryStream())
					{
						item.CopyTo(target: memoryStream);

						fileBytes = memoryStream.ToArray();
					}

					builder.Attachments.Add(
						fileName: item.FileName, data: fileBytes,
						contentType: MimeKit.ContentType.Parse(text: item.ContentType));
				}
			}
		}

		builder.HtmlBody = viewModel.Body;

		email.Body = builder.ToMessageBody();

		using var smtp = new MailKit.Net.Smtp.SmtpClient();

		await smtp.ConnectAsync(
			port: MailSettings.Port,
			host: MailSettings.Host,
			options: MailKit.Security.SecureSocketOptions.StartTls,
			cancellationToken: cancellationToken);

		await smtp.AuthenticateAsync(
			userName: MailSettings.Username,
			password: MailSettings.Password,
			cancellationToken: cancellationToken);

		await smtp.SendAsync(
			message: email,
			cancellationToken: cancellationToken);

		await smtp.DisconnectAsync
			(quit: true, cancellationToken: cancellationToken);
	}
}
