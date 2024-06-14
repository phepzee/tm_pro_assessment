using Microsoft.EntityFrameworkCore;
using ThandoMazibuko.Data;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Repository.Services
{
	public class UserRepository : IUserRepository
	{
		private readonly CustomerFeedbackDbContext customerFeedbackDbContext;

		public UserRepository(CustomerFeedbackDbContext customerFeedbackDbContext)
		{
			this.customerFeedbackDbContext = customerFeedbackDbContext;
		}		

		public async Task<(int, string)> Registration(UserRegistration userRegistration, string role)
		{
			var userExists = await GetUserByUsername(userRegistration.Username);
			if (userExists != null)
				return (0, "User already exists");

			UserRegistration user = new UserRegistration()
			{
				Username = userRegistration.Username,
				Name = userRegistration.Name,
				Email = userRegistration.Email,
				Password = userRegistration.Password,
				Role = (string.IsNullOrWhiteSpace(userRegistration.Role)) ? role : userRegistration.Role
			};

			var createUserResult = await customerFeedbackDbContext.UsersRegistration.AddAsync(user);
			await customerFeedbackDbContext.SaveChangesAsync();

			if (createUserResult.Entity.Id <= 0)
			{
				return (0, "User creation failed! Please check user details and try again.");
			}

			return (createUserResult.Entity.Id, "User created successfully!");
		}

		public async Task<UserRegistration> GetUserByUsername(string username)
		{
			return await customerFeedbackDbContext.UsersRegistration.FirstOrDefaultAsync(ur => ur.Username == username);
		}

		public async Task<UserRegistration> GetUserByPassword(string password)
		{
			return await customerFeedbackDbContext.UsersRegistration.FirstOrDefaultAsync(ur => ur.Password == password);
		}

		public async Task<UserRegistration> GetUserRole(string username, string password)
		{
			return await customerFeedbackDbContext.UsersRegistration.FirstOrDefaultAsync(ur => ur.Username == username && ur.Password == password);
		}

		public async Task<List<UserRegistration>> GetAdministrators()
		{
			return await customerFeedbackDbContext.UsersRegistration.Where(ur => ur.Role == UserRoles.Admin).ToListAsync();
		}
	}
}
