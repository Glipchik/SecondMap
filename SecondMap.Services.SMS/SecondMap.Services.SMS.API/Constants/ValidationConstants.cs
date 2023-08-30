namespace SecondMap.Services.SMS.API.Constants
{
	public static class ValidationConstants
	{
		public const int REVIEW_MIN_RATING = 0;
		public const int REVIEW_MAX_RATING = 5;
		public const int REVIEW_MAX_DESCRIPTION_LENGTH = 300;

		public const int STORE_MAX_NAME_LENGTH = 200;
		public const int STORE_MAX_ADDRESS_LENGTH = 200;
		public const int STORE_MIN_PRICE = 1;

		public const int USER_NAME_MAX_LENGTH = 20;
		public const int USER_PASSWORD_MIN_LENGTH = 8;
		public const int USER_PASSWORD_MAX_LENGTH = 32;

		public const int INVALID_ID = -1;
	}
}
