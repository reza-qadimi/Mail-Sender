var mailSettings =
	new Rez.Mail.Utilities.Settings.MailSettings
	{
		Port = 587,
		Host = "smtp.Gmail.com",
		DisplayName = "XXX",
		Password = " XXXXXXX ",
		Username = "XXXXXXX@GMail.com",
	};

var mailRequest =
	new Rez.Mail.ViewModels.MailRequestViewModel
	{
		Subject = "Message",
		Body = "Hello, World",
		To = "XXXXXXX@GMail.com",
	};

var result =
	new Rez.Mail.Services.MailsService(mainSettings: mailSettings);

await result.SendEmailAsync(viewModel: mailRequest);
