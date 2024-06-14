using ThandoMazibuko.Models;

namespace ThandoMazibuko.Repository.Interface
{
	public interface IEmailRepository
	{
		Task SendEmail(CustomerFeedback customerFeedback);
	}
}
