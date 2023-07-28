using AutoFixture;
using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.IntegrationTests.Constants;

namespace SecondMap.Services.SMS.IntegrationTests.Utilities
{
	public class DataSeeder
	{
		private StoreManagementDbContext _testStoreManagementDbContext;
		private IFixture _fixture;

		public List<ReviewEntity> ReviewEntities
		{
			get => _testStoreManagementDbContext.Reviews.ToList();
			set { }
		}
		public List<ScheduleEntity> ScheduleEntities
		{
			get => _testStoreManagementDbContext.Schedules.ToList();
			set { }
		}
		public List<StoreEntity> StoreEntities
		{
			get => _testStoreManagementDbContext.Stores.ToList();
			set { }
		}
		public List<UserEntity> UserEntities
		{
			get => _testStoreManagementDbContext.Users.ToList();
			set { }
		}

		public DataSeeder(StoreManagementDbContext testStoreManagementDbContext)
		{
			_testStoreManagementDbContext = testStoreManagementDbContext;
			_fixture = new Fixture();
		}

		public async Task SeedTestDataAsync()
		{
			// Add test data to different tables here
			ReviewEntities = await AddReviewsAsync();
			ScheduleEntities = await AddSchedulesAsync();
			StoreEntities = await AddStoresAsync();
			UserEntities = await AddUsersAsync();
		}

		private async Task<List<ReviewEntity>> AddReviewsAsync()
		{
			await _testStoreManagementDbContext.Reviews.AddRangeAsync(
					_fixture.Build<ReviewEntity>().Without(x => x.Id).CreateMany(TestConstants.ENTITIES_TO_CREATE_COUNT)
			);

			await _testStoreManagementDbContext.SaveChangesAsync();

			return await _testStoreManagementDbContext.Reviews.ToListAsync();
		}

		private async Task<List<ScheduleEntity>> AddSchedulesAsync()
		{
			await _testStoreManagementDbContext.Schedules.AddRangeAsync(
				_fixture.Build<ScheduleEntity>().Without(x => x.Id).CreateMany(TestConstants.ENTITIES_TO_CREATE_COUNT)
			);

			await _testStoreManagementDbContext.SaveChangesAsync();

			return await _testStoreManagementDbContext.Schedules.ToListAsync();
		}

		private async Task<List<StoreEntity>> AddStoresAsync()
		{
			await _testStoreManagementDbContext.Stores.AddRangeAsync(
				_fixture.Build<StoreEntity>().Without(x => x.Id).CreateMany(TestConstants.ENTITIES_TO_CREATE_COUNT)
			);

			await _testStoreManagementDbContext.SaveChangesAsync();

			return await _testStoreManagementDbContext.Stores.ToListAsync();
		}

		private async Task<List<UserEntity>> AddUsersAsync()
		{
			await _testStoreManagementDbContext.Users.AddRangeAsync(
				_fixture.Build<UserEntity>().Without(x => x.Id).CreateMany(TestConstants.ENTITIES_TO_CREATE_COUNT)
			);

			await _testStoreManagementDbContext.SaveChangesAsync();

			return await _testStoreManagementDbContext.Users.ToListAsync();
		}

	}
}
