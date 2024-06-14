using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthRepository authRepository;
		private readonly IUserRepository userRepository;
		private readonly ILogger<AuthenticationController> logger;

		public AuthenticationController(IAuthRepository authRepository, IUserRepository userRepository, ILogger<AuthenticationController> logger)
		{
			this.authRepository = authRepository;
			this.userRepository = userRepository;
			this.logger = logger;			
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(Login login)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest("Invalid payload");
				var (status, message) = await authRepository.Login(login);
				if (status == 0)
					return BadRequest(message);
				return Ok(message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		[Route("Registration")]
		public async Task<IActionResult> Register(UserRegistration userRegistration)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest("Invalid payload");
				var (status, message) = await userRepository.Registration(userRegistration, UserRoles.Admin);
				if (status == 0)
				{
					return BadRequest(message);
				}
				
				return CreatedAtAction(nameof(Register),new { id = status}, userRegistration);

			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
