using ThandoMazibuko.Models;

namespace ThandoMazibuko.Repository.Interface
{
	public interface IUserRepository
	{
		Task<(int, string)> Registration(UserRegistration userRegistration, string role);
		Task<UserRegistration> GetUserByUsername(string username);
		Task<UserRegistration> GetUserByPassword(string password);
		Task<UserRegistration> GetUserRole(string username, string password);
		Task<List<UserRegistration>> GetAdministrators();
	}
}
