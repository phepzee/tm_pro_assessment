using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ThandoMazibuko.Models
{
	[Keyless]
	public class Login
	{	
		public string Username { get; set; } 
		public string Password { get; set; }
	}
}
