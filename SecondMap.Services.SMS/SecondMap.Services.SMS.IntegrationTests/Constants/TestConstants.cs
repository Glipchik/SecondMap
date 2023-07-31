namespace SecondMap.Services.SMS.IntegrationTests.Constants
{
	public static class TestConstants
	{
		public const string IN_MEMORY_DB_NAME = "SMSTesting";

		public const string REVIEWS_URL = "api/Reviews";
		public const string SCHEDULES_URL = "api/Schedules";
		public const string STORES_URL = "api/Stores";
		public const string USERS_URL = "api/Users";

		public const int USER_ROLE_ID = 1;
		public const int INVALID_ID = -1;
		
		public static readonly MediaTypeHeaderValue MEDIA_TYPE_APP_JSON = new("application/json");
	}
}
