using ThandoMazibuko.Models;

namespace ThandoMazibuko.Repository.Interface
{
    public interface ICustomerFeedbackRepository
    {
        Task<IEnumerable<CustomerFeedback>> GetCustomersFeedback();
		Task<CustomerFeedback> GetCustomerFeedback(int employeeId);
		Task<CustomerFeedback> AddCustomerFeedback(CustomerFeedback customerFeedback);
	}
}
