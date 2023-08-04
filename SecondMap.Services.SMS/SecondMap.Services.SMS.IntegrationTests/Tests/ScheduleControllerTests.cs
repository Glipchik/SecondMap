﻿using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;

namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class SchedulesControllerTests : BaseControllerTests, IClassFixture<TestWebApplicationFactory<Program>>
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
		public async Task GetAllByStoreId_WhenValidStoreId_ShouldReturnSuccessAndDtoList()
		{
			// Arrange
			var scheduleEntity = await _dataSeeder.CreateScheduleAsync();
			var validId = scheduleEntity.StoreId;

			// Act
			var response =
				await _client.GetAsync(TestConstants.SCHEDULES_URL + "/" + ApiEndpoints.STORE_ID_EQUALS + $"{validId}");
			var dto = JsonConvert.DeserializeObject<List<ScheduleDto>>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			dto.ShouldNotBeEmpty();
			dto.ShouldAllBe(r => r.StoreId == validId);
		}

		[Fact]
		public async Task GetAllByStoreId_WhenInvalidStoreId_ShouldReturnNotFound()
		{
			// Arrange
			await _dataSeeder.CreateScheduleAsync();
			var invalidId = ValidationConstants.INVALID_ID;

			// Act
			var response =
				await _client.GetAsync(TestConstants.SCHEDULES_URL + "/" + ApiEndpoints.STORE_ID_EQUALS + $"{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
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
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.GetAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			ScheduleAddViewModel validViewModel)
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
			ScheduleAddViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Day = (DayOfWeekEu)(-1);

			// Act
			var response = await _client.PostAsync(TestConstants.SCHEDULES_URL, SerializeRequestBody(invalidViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			ScheduleUpdateViewModel validViewModelToUpdate)
		{
			// Arrange
			var entityToUpdate = await _dataSeeder.CreateScheduleAsync();

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{entityToUpdate.Id}", SerializeRequestBody(validViewModelToUpdate));

			var updatedDto = JsonConvert.DeserializeObject<ScheduleDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);

			updatedDto.ShouldNotBeNull();
			updatedDto.Id.ShouldBe(entityToUpdate.Id);
			updatedDto.OpeningTime.ShouldBe(validViewModelToUpdate.OpeningTime);
			updatedDto.ClosingTime.ShouldBe(validViewModelToUpdate.ClosingTime);
			updatedDto.IsClosed.ShouldBe(validViewModelToUpdate.IsClosed);
		}

		[Theory]
		[IntegrationTestsAutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			ScheduleUpdateViewModel validViewModel)
		{
			// Arrange
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.PutAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}", SerializeRequestBody(validViewModel));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
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
			var invalidId = TestConstants.INVALID_ID;

			// Act
			var response = await _client.DeleteAsync(TestConstants.SCHEDULES_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
