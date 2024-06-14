using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Repository.Services
{
	public class AuthRepository : IAuthRepository
	{
		private readonly IConfiguration configuration;	
		private readonly IUserRepository userRepository;

		public AuthRepository(IConfiguration configuration, IUserRepository userRepository)
		{
			this.configuration = configuration;
			this.userRepository = userRepository;
		}

		public async Task<(int, string)> Login(Login login)
		{
			var user = await userRepository.GetUserByUsername(login.Username);
			if (user == null)
			{
				return (0, "Invalid username");
			}

			var password = await userRepository.GetUserByPassword(login.Password);
			if (password == null)
			{
				return (0, "Invalid password");
			}
			
			var userRoles = await userRepository.GetUserRole(login.Username, login.Password);

			var authClaims = new List<Claim>
			{
			   new Claim(ClaimTypes.Name, user.Username),
			   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			authClaims.Add(new Claim(ClaimTypes.Role, userRoles.Role));

			string token = GenerateToken(authClaims);
			return (1, token);			
		}


		private string GenerateToken(IEnumerable<Claim> claims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = configuration["JWT:ValidIssuer"],
				Audience = configuration["JWT:ValidAudience"],
				Expires = DateTime.UtcNow.AddHours(3),
				SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
				Subject = new ClaimsIdentity(claims)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
