namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
    public class ReviewsControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
	{
		private readonly TestWebApplicationFactory<Program> _factory;
		private readonly DataSeeder _dataSeeder;
		private readonly HttpClient _client;
		public ReviewsControllerTests(TestWebApplicationFactory<Program> factory)
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
			await _dataSeeder.CreateReviewAsync();

			// Act
			var response = await _client.GetAsync(PathConstants.API_REVIEWS);
			var dto = JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetAllByStoreId_WhenValidStoreId_ShouldReturnSuccessAndDtoList()
		{
			// Arrange
			var reviewEntity = await _dataSeeder.CreateReviewAsync();
			var validId = reviewEntity.StoreId;
			
			// Act
			var response =
				await _client.GetAsync(String.Concat(PathConstants.API_REVIEWS, PathConstants.STORE_ID_EQUALS, $"{validId}"));
			
			var dto = await RequestSerializer.DeserializeFromResponseAsync<List<ReviewDto>>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeEmpty();
			dto.ShouldAllBe(r => r.StoreId == validId);
		}

		[Fact]
		public async Task GetAllByStoreId_WhenInvalidStoreId_ShouldReturnNotFound()
		{
			// Arrange
			await _dataSeeder.CreateReviewAsync();
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response =
				await _client.GetAsync(String.Concat(PathConstants.API_REVIEWS, PathConstants.STORE_ID_EQUALS, $"{invalidId}"));
			
			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validEntity = await _dataSeeder.CreateReviewAsync();

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_REVIEWS, $"{validEntity.Id}"));

			var dto = await RequestSerializer.DeserializeFromResponseAsync<ReviewDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validEntity.Id);
			dto.UserId.ShouldBe(validEntity.UserId);
			dto.Rating.ShouldBe(validEntity.Rating);
			dto.Description.ShouldBe(validEntity.Description);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_REVIEWS, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			ReviewAddViewModel validViewModel)
		{
			// Arrange
			validViewModel.StoreId = (await _dataSeeder.CreateStoreAsync()).Id;
			validViewModel.UserId = (await _dataSeeder.CreateUserAsync()).Id;

			// Act
			var response = await _client.PostAsync(PathConstants.API_REVIEWS, RequestSerializer.SerializeRequestBody(validViewModel));

			var dto = await RequestSerializer.DeserializeFromResponseAsync<ReviewDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBeGreaterThan(0);
			dto.UserId.ShouldBe(validViewModel.UserId);
			dto.StoreId.ShouldBe(validViewModel.StoreId);
			dto.Rating.ShouldBe(validViewModel.Rating);
			dto.Description.ShouldBe(validViewModel.Description);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenInvalidViewModel_ShouldReturnBadRequest(
			ReviewAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Rating = -1;

			// Act
			var response = await _client.PostAsync(PathConstants.API_REVIEWS, RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			ReviewUpdateViewModel viewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateReviewAsync();

			// Act
			var response = await _client.PutAsync(String.Concat(PathConstants.API_REVIEWS, $"{entityToUpdate.Id}"), RequestSerializer.SerializeRequestBody(viewModelToUpdate));

			var updatedDto = await RequestSerializer.DeserializeFromResponseAsync<ReviewDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
			updatedDto.Rating.ShouldBe(viewModelToUpdate.Rating);
			updatedDto.Description.ShouldBe(viewModelToUpdate.Description);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			ReviewUpdateViewModel validViewModel)
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(String.Concat(PathConstants.API_REVIEWS, $"{invalidId}"), RequestSerializer.SerializeRequestBody(validViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidIdButInvalidModel_ShouldReturnBadRequest(
			ReviewUpdateViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateReviewAsync()).Id;

			invalidViewModel.Rating = -1;

			// Act
			var response = await _client.PutAsync(String.Concat(PathConstants.API_REVIEWS, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenInvalidIdAndInvalidModel_ShouldReturnBadRequest(
			ReviewUpdateViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateReviewAsync()).Id;

			invalidViewModel.Rating = -1;

			// Act
			var response = await _client.PutAsync(String.Concat(PathConstants.API_REVIEWS, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateReviewAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_REVIEWS, $"{validId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_REVIEWS, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task RestoreByIdAsync_WhenReviewIsDeleted_ShouldReturnRestoredReview()
		{
			// Arrange
			var reviewEntity = await _dataSeeder.CreateReviewAsync();
			await _dataSeeder.SoftDeleteReviewAsync(reviewEntity);

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_REVIEWS, PathConstants.RESTORE,
				$"{reviewEntity.Id}"), null);

			var restoredDto = await RequestSerializer.DeserializeFromResponseAsync<ReviewDto>(response);

			// Assert
			restoredDto.ShouldBeOfType<ReviewDto>();
			restoredDto.Id.ShouldBe(reviewEntity.Id);
			restoredDto.StoreId.ShouldBe(reviewEntity.StoreId);
			restoredDto.UserId.ShouldBe(reviewEntity.UserId);
			restoredDto.Description.ShouldBe(reviewEntity.Description);
			restoredDto.Rating.ShouldBe(reviewEntity.Rating);
		}

		[Fact]
		public async Task RestoreByIdAsync_WhenReviewIsNotDeleted_ShouldReturnConflict()
		{
			// Arrange
			var reviewEntity = await _dataSeeder.CreateReviewAsync();

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_REVIEWS, PathConstants.RESTORE,
				$"{reviewEntity.Id}"), null);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.Conflict);

		}

		[Fact]
		public async Task RestoreByIdAsync_WhenDeletedReviewNotFound_ShouldReturnNotFound()
		{
			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_REVIEWS, PathConstants.RESTORE,
				$"{TestConstants.INVALID_ID}"), null);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
