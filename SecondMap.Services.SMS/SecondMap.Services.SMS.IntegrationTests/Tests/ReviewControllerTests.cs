using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;

namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class ReviewsControllerTests : BaseControllerTests, IClassFixture<TestWebApplicationFactory<Program>>
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
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL);
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
				await _client.GetAsync(TestConstants.REVIEWS_URL + "/" + ApiEndpoints.STORE_ID_EQUALS + $"{validId}");
			var dto = JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());

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
				await _client.GetAsync(TestConstants.REVIEWS_URL + "/" + ApiEndpoints.STORE_ID_EQUALS + $"{invalidId}");
			
			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validEntity = await _dataSeeder.CreateReviewAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL + $"/{validEntity.Id}");
			var dto = JsonConvert.DeserializeObject<ReviewDto>(await response.Content.ReadAsStringAsync());

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
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL + $"/{invalidId}");

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
			var response = await _client.PostAsync(TestConstants.REVIEWS_URL, SerializeRequestBody(validViewModel));

			var dto = JsonConvert.DeserializeObject<ReviewDto>(await response.Content.ReadAsStringAsync());

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
			var response = await _client.PostAsync(TestConstants.REVIEWS_URL, SerializeRequestBody(invalidViewModel));

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
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{entityToUpdate.Id}", SerializeRequestBody(viewModelToUpdate));

			var updatedDto = JsonConvert.DeserializeObject<ReviewDto>(await response.Content.ReadAsStringAsync());

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
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{invalidId}", SerializeRequestBody(validViewModel));

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
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

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
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateReviewAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(TestConstants.REVIEWS_URL + $"/{validId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(TestConstants.REVIEWS_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
