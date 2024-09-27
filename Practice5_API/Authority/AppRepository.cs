namespace Practice5_API.Authority
{
	public static class AppRepository
	{
		private static List<Application> _application = new List<Application>()
		{
			new Application
			{
				ApplicationId = 1,
				ApplicationName = "Practice5",
				ClientId = "123456",
				Secret = "4321",
				Scopes = "read,write"
			}
		};

		

		public static Application? GetApplicationByClientId(string clientId)
		{
			return _application.FirstOrDefault(x => x.ClientId == clientId);
		}
	}
}
