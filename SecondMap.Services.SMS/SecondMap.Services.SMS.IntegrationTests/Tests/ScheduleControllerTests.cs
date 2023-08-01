namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class SchedulesControllerTests : BaseControllerTests<ScheduleViewModel>, IClassFixture<TestWebApplicationFactory<Program>>
	{
		private readonly TestWebApplicationFactory<Program> _factory;
		private readonly DataSeeder _dataSeeder;
		private readonly HttpClient _client;
		public SchedulesControllerTests(TestWebApplicationFactory<Program> factory)
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
			await _dataSeeder.CreateScheduleAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.SCHEDULES_URL);
			var dto = JsonConvert.DeserializeObject<List<ScheduleDto>>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			var validViewModel = await _dataSeeder.CreateScheduleAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.SCHEDULES_URL + $"/{validViewModel.Id}");
			var dto = JsonConvert.DeserializeObject<ScheduleDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validViewModel.Id);
			dto.Day.ShouldBe(validViewModel.Day);
			dto.OpeningTime.ShouldBe(validViewModel.OpeningTime);
			dto.ClosingTime.ShouldBe(validViewModel.ClosingTime);
			dto.IsClosed.ShouldBe(validViewModel.IsClosed);
			dto.StoreId.ShouldBe(validViewModel.StoreId);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			ScheduleViewModel validViewModel)
		{
			// Arrange
			validViewModel.StoreId = (await _dataSeeder.CreateStoreAsync()).Id;

			// Act

			// Using custom TimeOnlyJsonConverter (from Utilities) to parse TimeOnly as hh:mm:ss
			// since default converter parses it as hh:mm (which won't pass validation for TimeOnly)
			var response = await _client.PostAsync(TestConstants.SCHEDULES_URL,
				SerializeRequestBody(validViewModel));

			var dto = JsonConvert.DeserializeObject<ScheduleDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeNull();
			dto.Id.ShouldBeGreaterThan(0);
			dto.Day.ShouldBe(validViewModel.Day);
			dto.OpeningTime.ShouldBe(validViewModel.OpeningTime);
			dto.ClosingTime.ShouldBe(validViewModel.ClosingTime);
			dto.IsClosed.ShouldBe(validViewModel.IsClosed);
			dto.StoreId.ShouldBe(validViewModel.StoreId);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenInvalidViewModel_ShouldReturnBadRequest(
			ScheduleViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Day = (DAL.Enums.DayOfWeekEu)(-1);

			// Act
			var response = await _client.PostAsync(TestConstants.SCHEDULES_URL, SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			ScheduleViewModel validViewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateScheduleAsync();
			validViewModelToUpdate.StoreId = entityToUpdate.StoreId;

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{entityToUpdate.Id}", SerializeRequestBody(validViewModelToUpdate));

			var updatedDto = JsonConvert.DeserializeObject<ScheduleDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
			updatedDto.Day.ShouldBe(validViewModelToUpdate.Day);
			updatedDto.OpeningTime.ShouldBe(validViewModelToUpdate.OpeningTime);
			updatedDto.ClosingTime.ShouldBe(validViewModelToUpdate.ClosingTime);
			updatedDto.IsClosed.ShouldBe(validViewModelToUpdate.IsClosed);
			updatedDto.StoreId.ShouldBe(validViewModelToUpdate.StoreId);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			ScheduleViewModel validViewModel)
		{
			// Arrange
			validViewModel.StoreId = (await _dataSeeder.CreateStoreAsync()).Id;
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}", SerializeRequestBody(validViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidIdButInvalidModel_ShouldReturnBadRequest(
			ScheduleViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateScheduleAsync()).Id;

			invalidViewModel.Day = (DAL.Enums.DayOfWeekEu)(-1);

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenInvalidIdAndInvalidModel_ShouldReturnBadRequest(
			ScheduleViewModel invalidViewModel)
		{
			// Arrange
			var validId = (await _dataSeeder.CreateScheduleAsync()).Id;

			invalidViewModel.Day = (DAL.Enums.DayOfWeekEu)(-1);

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{validId}", SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			var validId = (await _dataSeeder.CreateScheduleAsync()).Id;

			// Act
			var response = await _client.DeleteAsync(TestConstants.SCHEDULES_URL + $"/{validId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		private static readonly JsonSerializerSettings JsonSerializerSettings = new()
		{
			DateFormatString = "HH:mm:ss",
		};
	}
}
