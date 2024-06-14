using SendGrid.Helpers.Mail;
using SendGrid;
using ThandoMazibuko.Data;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;
using System.Text;

namespace ThandoMazibuko.Repository.Services
{
	public class EmailRepository : IEmailRepository
	{
		private readonly IConfiguration configuration;
		private readonly IUserRepository userRepository;

		public EmailRepository(IConfiguration configuration, IUserRepository userRepository)
		{
			this.configuration = configuration;
			this.userRepository = userRepository;
		}

		public async Task SendEmail(CustomerFeedback customerFeedback)
		{
			var emailBody = new StringBuilder();
			var adminUsers = await userRepository.GetAdministrators();
			if (adminUsers.Any())
			{
				foreach (var admin in adminUsers) 
				{	
					emailBody.Append($"Dear {admin.Name},");
					emailBody.Append("<br/>");
					emailBody.Append("<br/>");
					emailBody.Append($"New customer feedback submission below.");
					emailBody.Append("<br/>");
					emailBody.Append("<br/>");
					emailBody.Append($"Firstname: {customerFeedback.Firstname}");
					emailBody.Append("<br/>");
					emailBody.Append($"Lastname : {customerFeedback.Lastname}");
					emailBody.Append("<br/>");
					emailBody.Append($"Service Type: {customerFeedback.ServiceType}");
					emailBody.Append("<br/>");
					emailBody.Append($"Suggestions : {customerFeedback.Suggestions}");

					Environment.SetEnvironmentVariable("TM_2409_SENDGRID_API_KEY", AppSettingsVariables.Get(configuration).ApiKey);
					var apiKey = Environment.GetEnvironmentVariable("TM_2409_SENDGRID_API_KEY");
					var client = new SendGridClient(apiKey);
					var fromEmail = new EmailAddress(AppSettingsVariables.Get(configuration).FromEmail, AppSettingsVariables.Get(configuration).FromName);
					var subject = "Customer Feedback";
					var toEmail = new EmailAddress(admin.Email, admin.Name);
					var plainTextContent = emailBody.ToString();
					var htmlContent = emailBody.ToString();
					var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, plainTextContent, htmlContent);
					var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
					emailBody.Clear();
				}
			}			
		}
	}
}
