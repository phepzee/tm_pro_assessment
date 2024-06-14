using ThandoMazibuko.Models;

namespace ThandoMazibuko.Repository.Interface
{
	public interface IAuthRepository
	{		
		Task<(int, string)> Login(Login login);
	}
}
