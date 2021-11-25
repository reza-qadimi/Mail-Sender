namespace Rez.Mail.Services;

public interface IMailsService
{
	System.Threading.Tasks.Task
		SendEmailAsync(
		ViewModels.MailRequestViewModel viewModel,
		System.Threading.CancellationToken cancellationToken = default);
}
