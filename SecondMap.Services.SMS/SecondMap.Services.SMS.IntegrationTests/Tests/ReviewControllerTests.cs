using System.Net;
using System.Net.Http.Json;
using System.Text;
using AutoFixture.Xunit2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop.Infrastructure;
using Newtonsoft.Json;
using SecondMap.Services.SMS.API;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels;
using SecondMap.Services.SMS.BLL.Exceptions;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.IntegrationTests.Constants;
using SecondMap.Services.SMS.IntegrationTests.Utilities;
using Shouldly;

namespace SecondMap.Services.SMS.IntegrationTests.Tests
{
	public class ReviewControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
	{
		private readonly TestWebApplicationFactory<Program> _factory;
		private readonly DataSeeder _dataSeeder;
		private readonly HttpClient _client;

		public ReviewControllerTests(TestWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_dataSeeder = new DataSeeder((StoreManagementDbContext)factory.Services.CreateScope().ServiceProvider.GetService(typeof(StoreManagementDbContext))!);
			_client = _factory.CreateClient();
		}

		[Fact]
		public async Task GetAll_WhenEntitiesExist_ShouldReturnSuccessAndDtoList()
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			// Act
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL);
			var dto = JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeEmpty();
		}

		[Fact]
		public async Task GetAll_WhenEntitiesNotExist_ShouldReturnSuccessAndEmptyDto()
		{
			// Arrange

			// Act
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL);
			var dto = JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());
			
			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldBeEmpty();
		}

		[Fact]
		public async Task GetById_WhenValidEntity_ShouldReturnSuccessAndFoundDto()
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			var validReview = _dataSeeder.ReviewEntities[0];

			// Act
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL + $"/{validReview.Id}");
			var dto = JsonConvert.DeserializeObject<ReviewDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			dto.ShouldNotBeNull();
			dto.Id.ShouldBe(validReview.Id);
			dto.UserId.ShouldBe(validReview.UserId);
			dto.Rating.ShouldBe(validReview.Rating);
			dto.Description.ShouldBe(validReview.Description);
		}

		[Fact]
		public async Task GetById_WhenInvalidEntity_ShouldReturnNotFound()
		{
			// Arrange
			var invalidId = -1;

			// Act
			var response = await _client.GetAsync(TestConstants.REVIEWS_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[AutoData]
		public async Task AddAsync_WhenValidViewModel_ShouldReturnSuccessAndAddedDto(
			ReviewViewModel validViewModel)
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();
			// Act
			var response = await _client.PostAsync(TestConstants.REVIEWS_URL, new StringContent(JsonConvert.SerializeObject(validViewModel), encoding:Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

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
		[AutoData]
		public async Task AddAsync_WhenInvalidViewModel_ShouldReturnBadRequest(
			ReviewViewModel invalidViewModel)
		{
			// Arrange
			invalidViewModel.Rating = -1;

			// Act
			var response = await _client.PostAsync(TestConstants.REVIEWS_URL, new StringContent(JsonConvert.SerializeObject(invalidViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[AutoData]
		public async Task UpdateAsync_WhenValidViewModel_ShouldReturnSuccessAndUpdatedDto(
			ReviewViewModel reviewViewModelToUpdate)
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();
			var reviewEntityToUpdate = _dataSeeder.ReviewEntities[0];

			// Act
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{reviewEntityToUpdate.Id}", new StringContent(JsonConvert.SerializeObject(reviewViewModelToUpdate), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			var updatedReviewDto = JsonConvert.DeserializeObject<ReviewDto>(await response.Content.ReadAsStringAsync());

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			updatedReviewDto.ShouldNotBeNull();
			updatedReviewDto.Id.ShouldBe(reviewEntityToUpdate.Id);
			updatedReviewDto.UserId.ShouldBe(reviewViewModelToUpdate.UserId);
			updatedReviewDto.StoreId.ShouldBe(reviewViewModelToUpdate.StoreId);
			updatedReviewDto.Rating.ShouldBe(reviewViewModelToUpdate.Rating);
			updatedReviewDto.Description.ShouldBe(reviewViewModelToUpdate.Description);
		}

		[Theory]
		[AutoData]
		public async Task UpdateAsync_WhenValidModelButInvalidId_ShouldReturnNotFound(
			ReviewViewModel reviewViewModel)
		{
			// Arrange
			var invalidId = -1;

			// Act
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{invalidId}", new StringContent(JsonConvert.SerializeObject(reviewViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Theory]
		[AutoData]
		public async Task UpdateAsync_WhenValidIdButInvalidModel_ShouldReturnBadRequest(
			ReviewViewModel reviewViewModel)
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			var validReviewEntityId = _dataSeeder.ReviewEntities[0].Id;

			reviewViewModel.Rating = -1;

			// Act
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{validReviewEntityId}", new StringContent(JsonConvert.SerializeObject(reviewViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Theory]
		[AutoData]
		public async Task UpdateAsync_WhenInvalidIdAndInvalidModel_ShouldReturnBadRequest(
			ReviewViewModel reviewViewModel)
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			var validReviewEntityId = _dataSeeder.ReviewEntities[0].Id;

			reviewViewModel.Rating = -1;

			// Act
			var response = await _client.PutAsync(TestConstants.REVIEWS_URL + $"/{validReviewEntityId}", new StringContent(JsonConvert.SerializeObject(reviewViewModel), encoding: Encoding.UTF8, TestConstants.MEDIA_TYPE_APP_JSON));

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task DeleteAsync_WhenValidId_ShouldReturnNoContent()
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			var validReviewEntityId = _dataSeeder.ReviewEntities[0].Id;

			// Act
			var response = await _client.DeleteAsync(TestConstants.REVIEWS_URL + $"/{validReviewEntityId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
			_dataSeeder.ReviewEntities.Count.ShouldBe(TestConstants.ENTITIES_TO_CREATE_COUNT - 1);
		}

		[Fact]
		public async Task DeleteAsync_WhenInvalidId_ShouldReturnNoContent()
		{
			// Arrange
			await _dataSeeder.SeedTestDataAsync();

			var invalidId = -1;

			// Act
			var response = await _client.DeleteAsync(TestConstants.REVIEWS_URL + $"/{invalidId}");

			// Assert
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
			_dataSeeder.ReviewEntities.Count.ShouldBe(TestConstants.ENTITIES_TO_CREATE_COUNT);
		}
	}
}

// Arrange


// Act


// Assert
