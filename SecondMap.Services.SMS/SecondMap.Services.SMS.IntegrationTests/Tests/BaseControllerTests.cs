namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class BaseControllerTests<TViewModel> where TViewModel : class
	{
		protected static StringContent SerializeRequestBody(TViewModel body)
		{
			return new StringContent(JsonConvert.SerializeObject(body, converters: new TimeOnlyJsonConverter()),
				TestConstants.MEDIA_TYPE_APP_JSON);
		}
	}
}