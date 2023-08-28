namespace SecondMap.Services.Auth.Infrastructure.Constants
{
	public static class ConfigurationConstants
	{
		public const string SMS_API_SCOPE = "smsAPI";
		public const string SMS_API_PUBLIC = "Store Management Service API scope";

		public const string TEST_CLIENT_ID = "test-client";
		public const string TEST_CLIENT_PUBLIC = "Test client";
		public const string TEST_CLIENT_SECRET = "test-secret";
		public const string TEST_CLIENT_REDIRECT_URI = "https://localhost:5002/signin-oidc";
		public const string TEST_CLIENT_POST_LOGOUT_REDIRECT_URI = "https://localhost:5002/signout-callback-oidc";

		public const string POSTMAN_CLIENT_ID = "postman-client";
		public const string POSTMAN_CLIENT_PUBLIC = "postman client";
		public const string POSTMAN_CLIENT_SECRET = "postman-secret";
		public const string POSTMAN_CLIENT_REDIRECT_URI = "https://www.getpostman.com/oath2/callback";
		public const string POSTMAN_CLIENT_POST_LOGOUT_REDIRECT_URI = "https://www.getpostman.com";
	}
}