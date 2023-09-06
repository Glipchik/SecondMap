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
			var response = await _client.GetAsync(PathConstants.API_STORES);
			var dto = await RequestSerializer.DeserializeFromResponseAsync<List<StoreDto>>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validEntity = await _dataSeeder.CreateStoreAsync();

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_STORES, $"{validEntity.Id}"));
			var dto = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validEntity.Id);
			dto.Address.ShouldBe(validEntity.Address);
			dto.Name.ShouldBe(validEntity.Name);
			dto.Price.ShouldBe(validEntity.Price);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_STORES, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task GetByIdWithDetails_WhenValidId_ShouldReturnStoreWithDetails()
		{
			// Arrange
			var (reviewEntity, scheduleEntity) = await _dataSeeder.CreateReviewAndScheduleAsync();

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_STORES, PathConstants.DETAILS, $"{reviewEntity.StoreId}"));

			var dtoWithDetails = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dtoWithDetails.ShouldNotBeNull();
			dtoWithDetails.Schedules.ShouldNotBeEmpty();
			dtoWithDetails.Reviews.ShouldNotBeEmpty();
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			StoreViewModel validViewModel)
		{
			// Arrange

			// Act
			var response = await _client.PostAsync(PathConstants.API_STORES,
				RequestSerializer.SerializeRequestBody(validViewModel));

			var dto = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

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
			var response = await _client.PostAsync(PathConstants.API_STORES, RequestSerializer.SerializeRequestBody(invalidViewModel));

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
			var response = await _client.PutAsync(String.Concat(PathConstants.API_STORES, $"{entityToUpdate.Id}"), RequestSerializer.SerializeRequestBody(validViewModelToUpdate));

			var updatedDto = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

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
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(String.Concat(PathConstants.API_STORES, $"{invalidId}"), RequestSerializer.SerializeRequestBody(validViewModel));

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
			var response = await _client.PutAsync(String.Concat(PathConstants.API_STORES, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

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
			var response = await _client.PutAsync(String.Concat(PathConstants.API_STORES, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateStoreAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_STORES, $"{validId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_STORES, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task RestoreByIdAsync_WhenStoreIsDeleted_ShouldReturnRestoredStore()
		{
			// Arrange
			var storeEntity = await _dataSeeder.CreateStoreAsync();
			await _dataSeeder.SoftDeleteStoreAsync(storeEntity);

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_STORES, PathConstants.RESTORE,
				$"{storeEntity.Id}"), null);

			var restoredDto = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

			// Assert
			restoredDto.ShouldBeOfType<StoreDto>();
			restoredDto.Id.ShouldBe(storeEntity.Id);
			restoredDto.Name.ShouldBe(storeEntity.Name);
			restoredDto.Address.ShouldBe(storeEntity.Address);
			restoredDto.Price.ShouldBe(storeEntity.Price);
		}

		[Fact]
		public async Task RestoreByIdAsync_WhenStoreIsNotDeleted_ShouldReturnConflict()
		{
			// Arrange
			var storeEntity = await _dataSeeder.CreateStoreAsync();

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_STORES, PathConstants.RESTORE,
				$"{storeEntity.Id}"), null);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.Conflict);

		}

		[Fact]
		public async Task RestoreByIdAsync_WhenDeletedStoreNotFound_ShouldReturnNotFound()
		{
			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_STORES, PathConstants.RESTORE,
				$"{TestConstants.INVALID_ID}"), null);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task RestoreByIdWithReviewsAsync_WhenStoreIsDeleted_ShouldReturnRestoredStore()
		{
			// Arrange
			var storeEntity = await _dataSeeder.CreateStoreAsync();
			await _dataSeeder.CreateReviewAsync();
			await _dataSeeder.SoftDeleteStoreAsync(storeEntity);

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_STORES, PathConstants.RESTORE,
				$"{storeEntity.Id}", PathConstants.WITH_REVIEWS_TRUE), null);

			var restoredDto = await RequestSerializer.DeserializeFromResponseAsync<StoreDto>(response);

			// Assert
			restoredDto.ShouldBeOfType<StoreDto>();
			restoredDto.Id.ShouldBe(storeEntity.Id);
			restoredDto.Name.ShouldBe(storeEntity.Name);
			restoredDto.Address.ShouldBe(storeEntity.Address);
			restoredDto.Price.ShouldBe(storeEntity.Price);
			restoredDto.Reviews.ShouldNotBeNull();
		}
	}
}
