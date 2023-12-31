﻿namespace SecondMap.Services.SMS.IntegrationTests.Utilities
{
	public class DataSeeder
	{
		private readonly StoreManagementDbContext _testStoreManagementDbContext;
		private readonly IFixture _fixture;
		public DataSeeder(StoreManagementDbContext testStoreManagementDbContext)
		{
			_testStoreManagementDbContext = testStoreManagementDbContext;
			_fixture = new Fixture().Customize(new IntegrationTestCustomization());
		}

		public async Task<(ReviewEntity, ScheduleEntity)> CreateReviewAndScheduleAsync()
		{
			var addedUser = (await _testStoreManagementDbContext.Users.AddAsync(
					_fixture.Create<UserEntity>()))
				.Entity;
			var addedStore = (await _testStoreManagementDbContext.Stores.AddAsync(
				_fixture.Create<StoreEntity>()
			)).Entity;

			var reviewToAdd = _fixture.Create<ReviewEntity>();
			reviewToAdd.StoreId = addedStore.Id;
			reviewToAdd.UserId = addedUser.Id;

			var scheduleToAdd = _fixture.Create<ScheduleEntity>();
			scheduleToAdd.StoreId = addedStore.Id;

			var addedReview = (await _testStoreManagementDbContext.AddAsync(reviewToAdd)).Entity;
			var addedSchedule = (await _testStoreManagementDbContext.AddAsync(scheduleToAdd)).Entity;

			await _testStoreManagementDbContext.SaveChangesAsync();

			return (addedReview, addedSchedule);
		}

		public async Task<ReviewEntity> CreateReviewAsync()
		{
			var addedUser = (await _testStoreManagementDbContext.Users.AddAsync(
				_fixture.Create<UserEntity>()))
				.Entity;
			var addedStore = (await _testStoreManagementDbContext.Stores.AddAsync(
				_fixture.Create<StoreEntity>()))
				.Entity;

			var reviewToAdd = _fixture.Create<ReviewEntity>();
			reviewToAdd.UserId = addedUser.Id;
			reviewToAdd.StoreId = addedStore.Id;

			var addedReview = (await _testStoreManagementDbContext.AddAsync(reviewToAdd)).Entity;

			await _testStoreManagementDbContext.SaveChangesAsync();

			return addedReview;
		}

		public async Task SoftDeleteReviewAsync(ReviewEntity reviewEntity)
		{
			reviewEntity.IsDeleted = true;
			reviewEntity.DeletedAt = DateTimeOffset.UtcNow;

			_testStoreManagementDbContext.Entry(reviewEntity).State = EntityState.Modified;

			await _testStoreManagementDbContext.SaveChangesAsync();
		}

		public async Task<ScheduleEntity> CreateScheduleAsync()
		{
			var addedStore = await CreateStoreAsync();

			var scheduleToAdd = _fixture.Create<ScheduleEntity>();
			scheduleToAdd.StoreId = addedStore.Id;

			var addedSchedule = (await _testStoreManagementDbContext.AddAsync(scheduleToAdd)).Entity;

			await _testStoreManagementDbContext.SaveChangesAsync();

			return addedSchedule;
		}

		public async Task<StoreEntity> CreateStoreAsync()
		{
			var addedStore = (await _testStoreManagementDbContext.Stores.AddAsync(
				_fixture.Create<StoreEntity>()))
					.Entity;

			await _testStoreManagementDbContext.SaveChangesAsync();

			return addedStore;
		}

		public async Task SoftDeleteStoreAsync(StoreEntity storeEntity)
		{
			storeEntity.IsDeleted = true;
			storeEntity.DeletedAt = DateTimeOffset.UtcNow;

			_testStoreManagementDbContext.Entry(storeEntity).State = EntityState.Modified;

			await _testStoreManagementDbContext.SaveChangesAsync();
		}

		public async Task<UserEntity> CreateUserAsync()
		{
			var addedRole = (await _testStoreManagementDbContext.Roles.AddAsync(
				new RoleEntity
				{
					RoleName = nameof(AppRoles.Customer)
				}
			));

			var addedUser = (await _testStoreManagementDbContext.Users.AddAsync(
				_fixture.Create<UserEntity>()))
				.Entity;

			await _testStoreManagementDbContext.SaveChangesAsync();

			return addedUser;
		}

	}
}
