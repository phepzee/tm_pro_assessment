using Microsoft.EntityFrameworkCore;
using ThandoMazibuko.Data;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Repository.Services
{
    public class CustomerFeedbackRepository : ICustomerFeedbackRepository
    {
        private readonly CustomerFeedbackDbContext customerFeedbackDbContext;

		public CustomerFeedbackRepository(CustomerFeedbackDbContext customerFeedbackDbContext)
        {
			this.customerFeedbackDbContext = customerFeedbackDbContext;
		}

        public async Task<IEnumerable<CustomerFeedback>> GetCustomersFeedback()
        {
            return await customerFeedbackDbContext.CustomerFeedbacks.ToListAsync();
        }

		public async Task<CustomerFeedback> GetCustomerFeedback(int customerFeedbackId)
		{
			return await customerFeedbackDbContext.CustomerFeedbacks.FindAsync(customerFeedbackId);			
		}

		public async Task<CustomerFeedback> AddCustomerFeedback(CustomerFeedback customerFeedback)
		{
			var result = await customerFeedbackDbContext.CustomerFeedbacks.AddAsync(customerFeedback);
			await customerFeedbackDbContext.SaveChangesAsync();
			return result.Entity;
		}
	}
}
