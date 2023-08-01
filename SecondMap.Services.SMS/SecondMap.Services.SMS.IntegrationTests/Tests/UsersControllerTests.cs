namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class UsersControllerTests : BaseControllerTests, IClassFixture<TestWebApplicationFactory<Program>>
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
			var response = await _client.GetAsync(TestConstants.USERS_URL);
			var dto = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());

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
			var response = await _client.GetAsync(TestConstants.USERS_URL + $"/{validViewModel.Id}");
			var dto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validViewModel.Id);
			dto.Username.ShouldBe(validViewModel.Username);
			dto.Role!.Id.ShouldBe(TestConstants.USER_ROLE_ID);
			dto.Role!.RoleName.ShouldBe(nameof(AppRoles.Customer));
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(TestConstants.USERS_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenInvalidViewModel_ShouldReturnBadRequest(
			UserViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Username =
				invalidViewModel.Username!.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH + 1, 'a');

			// Act
			var response = await _client.PostAsync(TestConstants.USERS_URL, SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			UserViewModel validViewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateUserAsync();

			// Act
			var response = await _client.PutAsync(TestConstants.USERS_URL + $"/{entityToUpdate.Id}", SerializeRequestBody(validViewModelToUpdate));

			var updatedDto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
			updatedDto.Username.ShouldBe(validViewModelToUpdate.Username);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			UserViewModel validViewModel)
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(TestConstants.USERS_URL + $"/{invalidId}", SerializeRequestBody(validViewModel));

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

			invalidViewModel.Username =
				invalidViewModel.Username!.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH + 1, 'a');

			// Act
			var response = await _client.PutAsync(TestConstants.USERS_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

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

			invalidViewModel.Username =
				invalidViewModel.Username!.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH + 1, 'a');

			// Act
			var response = await _client.PutAsync(TestConstants.USERS_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateUserAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(TestConstants.USERS_URL + $"/{validId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(TestConstants.USERS_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
