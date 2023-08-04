namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class BaseControllerTests
	{
		protected static StringContent SerializeRequestBody(IViewModel body)
		{
			return new StringContent(JsonConvert.SerializeObject(body, converters: new TimeOnlyJsonConverter()),
				TestConstants.MEDIA_TYPE_APP_JSON);
		}
	}
}