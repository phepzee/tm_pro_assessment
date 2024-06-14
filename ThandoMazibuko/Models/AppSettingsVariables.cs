using ThandoMazibuko.Repository.Interface;

namespace ThandoMazibuko.Models
{
	public class AppSettingsVariables
	{
		public string ApiKey { get; set; }
		public string FromEmail { get; set; }
		public string FromName { get; set; }

		private static AppSettingsVariables appSettingsVariables;		

		public static AppSettingsVariables Get(IConfiguration configuration)
		{
			if (appSettingsVariables == null)
			{
				appSettingsVariables = new AppSettingsVariables
				{
					ApiKey = configuration.GetSection("AppSettingsVariables:API_KEY")?.Value,
					FromEmail = configuration.GetSection("AppSettingsVariables:FROM_EMAIL")?.Value,
					FromName = configuration.GetSection("AppSettingsVariables:FROM_NAME")?.Value
				};
			}

			return appSettingsVariables;
		}
	}
}
