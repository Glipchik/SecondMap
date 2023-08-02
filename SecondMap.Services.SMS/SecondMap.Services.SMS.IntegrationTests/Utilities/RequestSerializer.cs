namespace SecondMap.Services.SMS.IntegrationTests.Utilities
{
    public static class RequestSerializer
    {
        public static StringContent SerializeRequestBody<TViewModel>(TViewModel body)
        {
            return new StringContent(JsonConvert.SerializeObject(body, converters: new TimeOnlyJsonConverter()),
                TestConstants.MEDIA_TYPE_APP_JSON);
        }

        public static async Task<TDeserialized?> DeserializeFromResponseAsync<TDeserialized>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<TDeserialized>(await response.Content.ReadAsStringAsync());
        }
    }
}