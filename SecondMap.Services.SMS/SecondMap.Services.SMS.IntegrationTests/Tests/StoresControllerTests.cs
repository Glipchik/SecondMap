namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class StoresControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
	{
		private readonly TestWebApplicationFactory<Program> _factory;
		private readonly DataSeeder _dataSeeder;
		private readonly HttpClient _client;
		public StoresControllerTests(TestWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_dataSeeder = new DataSeeder((StoreManagementDbContext)factory.Services.CreateScope().ServiceProvider
				.GetService(typeof(StoreManagementDbContext))!);
			_client = _factory.CreateClient();
		}

		[Fact]
		public async Task GetAll_WhenEntitiesExist_ShouldReturnSuccessAndDtoList()
		{
			// Arrange
			await _dataSeeder.CreateStoreAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.STORES_URL);
			var dto = JsonConvert.DeserializeObject<List<StoreDto>>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validViewModel = await _dataSeeder.CreateStoreAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.STORES_URL + $"/{validViewModel.Id}");
			var dto = JsonConvert.DeserializeObject<StoreDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validViewModel.Id);
			dto.Address.ShouldBe(validViewModel.Address);
			dto.Name.ShouldBe(validViewModel.Name);
			dto.Rating.ShouldBe(validViewModel.Rating);
			dto.Price.ShouldBe(validViewModel.Price);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(TestConstants.STORES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			StoreViewModel validViewModel)
		{
			// Arrange

			// Act
			var response = await _client.PostAsync(TestConstants.STORES_URL,
				new StringContent(JsonConvert.SerializeObject(validViewModel),
					new MediaTypeHeaderValue(TestConstants.MEDIA_TYPE_APP_JSON)));

			var dto = JsonConvert.DeserializeObject<StoreDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBeGreaterThan(0);
			dto.Address.ShouldBe(validViewModel.Address);
			dto.Name.ShouldBe(validViewModel.Name);
			dto.Price.ShouldBe(validViewModel.Price);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenInvalidViewModel_ShouldReturnBadRequest(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Price = ValidationConstants.STORE_MIN_PRICE - 1;

			// Act
			var response = await _client.PostAsync(TestConstants.STORES_URL, new StringContent(JsonConvert.SerializeObject(invalidViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			StoreViewModel validViewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateStoreAsync();

			// Act
			var response = await _client.PutAsync(TestConstants.STORES_URL + $"/{entityToUpdate.Id}", new StringContent(JsonConvert.SerializeObject(validViewModelToUpdate), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			var updatedDto = JsonConvert.DeserializeObject<StoreDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
			updatedDto.Address.ShouldBe(validViewModelToUpdate.Address);
			updatedDto.Name.ShouldBe(validViewModelToUpdate.Name);
			updatedDto.Price.ShouldBe(validViewModelToUpdate.Price);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			StoreViewModel validViewModel)
		{
			// Arrange
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(TestConstants.STORES_URL + $"/{invalidId}", new StringContent(JsonConvert.SerializeObject(validViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidIdButInvalidModel_ShouldReturnBadRequest(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateStoreAsync()).Id;

			invalidViewModel.Price = ValidationConstants.STORE_MIN_PRICE - 1;

			// Act
			var response = await _client.PutAsync(TestConstants.STORES_URL + $"/{validId}", new StringContent(JsonConvert.SerializeObject(invalidViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenInvalidIdAndInvalidModel_ShouldReturnBadRequest(
			StoreViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateStoreAsync()).Id;

			invalidViewModel.Price = ValidationConstants.STORE_MIN_PRICE - 1;

			// Act
			var response = await _client.PutAsync(TestConstants.STORES_URL + $"/{validId}", new StringContent(JsonConvert.SerializeObject(invalidViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateStoreAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(TestConstants.STORES_URL + $"/{validId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(TestConstants.STORES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
