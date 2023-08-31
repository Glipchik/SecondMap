namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class UsersControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
	{
		private readonly TestWebApplicationFactory<Program> _factory;
		private readonly DataSeeder _dataSeeder;
		private readonly HttpClient _client;
		public UsersControllerTests(TestWebApplicationFactory<Program> factory)
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
			await _dataSeeder.CreateUserAsync();

			// Act
			var response = await _client.GetAsync(PathConstants.API_USERS);
			var dto = await RequestSerializer.DeserializeFromResponseAsync<List<UserDto>>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validViewModel = await _dataSeeder.CreateUserAsync();

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_USERS, $"{validViewModel.Id}"));
			var dto = await RequestSerializer.DeserializeFromResponseAsync<UserDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validViewModel.Id);
			dto.Username.ShouldBe(validViewModel.Username);
			dto.Role.ShouldBe(UserRole.Admin);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(String.Concat(PathConstants.API_USERS, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_ShouldReturnMethodNotAllowed(
			UserViewModel validViewModel)
		{
			// Act
			var response = await _client.PostAsync(PathConstants.API_USERS, RequestSerializer.SerializeRequestBody(validViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			UserViewModel validViewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateUserAsync();

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_USERS, $"{entityToUpdate.Id}"), RequestSerializer.SerializeRequestBody(validViewModelToUpdate));

			var updatedDto = await RequestSerializer.DeserializeFromResponseAsync<UserDto>(response);

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			UserViewModel validViewModel)
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_USERS, $"{invalidId}"), RequestSerializer.SerializeRequestBody(validViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidIdButInvalidModel_ShouldReturnBadRequest(
			UserViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateUserAsync()).Id;

			invalidViewModel.Email = "invalidEmail";

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_USERS, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenInvalidIdAndInvalidModel_ShouldReturnBadRequest(
			UserViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateUserAsync()).Id;

			invalidViewModel.Email = "invalidEmail";

			// Act
			var response = await _client.PatchAsync(String.Concat(PathConstants.API_USERS, $"{validId}"), RequestSerializer.SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateUserAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_USERS, $"{validId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(String.Concat(PathConstants.API_USERS, $"{invalidId}"));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
