using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Controllers
{
    [Route("api/[controller]")]
	[ApiController]	
	public class CustomersFeedbackController : ControllerBase
	{
		private readonly ICustomerFeedbackRepository customerFeedbackRespository;
		private readonly IEmailRepository emailRespository;
		private readonly ILogger<AuthenticationController> logger;

		public CustomersFeedbackController(ICustomerFeedbackRepository customerFeedbackRespository, IEmailRepository emailRespository, ILogger<AuthenticationController> logger)
		{
			this.customerFeedbackRespository = customerFeedbackRespository;
			this.emailRespository = emailRespository;
			this.logger = logger;
		}

		[HttpGet]
		[Route("GetCustomersFeedback")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult> GetCustomersFeedback()
		{	
			try
			{
				return Ok(await customerFeedbackRespository.GetCustomersFeedback());
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database. {ex.Message}");
			}
		}

		[HttpPost]
		[Route("AddCustomerFeedback")]
		[AllowAnonymous]
		public async Task<ActionResult<CustomerFeedback>> AddCustomerFeedback(CustomerFeedback customerFeedback)
		{
			try
			{
				if (customerFeedback == null)
					return BadRequest();

				var createdCustomerFeedback = await customerFeedbackRespository.AddCustomerFeedback(customerFeedback);

				if(createdCustomerFeedback.Id > 0)
				{
					await emailRespository.SendEmail(createdCustomerFeedback);
				}

				return Ok(createdCustomerFeedback);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding customer feedback. {ex.Message}");
			}
		}
	}
}
