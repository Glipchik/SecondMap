namespace SecondMap.Services.SMS.IntegrationTests.DataSetups
{
	public class IntegrationTestCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => fixture.Behaviors.Remove(b));
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customize<ReviewEntity>(f =>
                    f.OmitAutoProperties()
                    .Without(r => r.Id)
                    .Without(r => r.UserId)
                    .Without(r => r.StoreId)
                    .Without(r => r.User)
                    .Without(r => r.Store)
					.Do(r => r.Rating = ValidationConstants.REVIEW_MIN_RATING)
					.Do(r => r.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH, 'a')));

			fixture.Customize<ScheduleEntity>(f =>
					f.OmitAutoProperties()
					.Without(s => s.Id)
					.Without(s => s.StoreId)
					.Without(s => s.Store)
					.Do(s => s.IsClosed = false)
					.Do(s => s.Day = DayOfWeekEu.Monday)
					.Do(s => s.OpeningTime = new TimeOnly(9, 30, 0))
					.Do(s => s.ClosingTime = new TimeOnly(21, 30, 0)));

            fixture.Customize<StoreEntity>(f =>
                    f.OmitAutoProperties()
                    .Without(s => s.Id)
                    .Do(s => s.Price = ValidationConstants.STORE_MIN_PRICE)
                    .Do(s => s.Address = string.Empty.PadRight(ValidationConstants.STORE_MAX_ADDRESS_LENGTH, 'a'))
                    .Do(s => s.Name = string.Empty.PadRight(ValidationConstants.STORE_MAX_NAME_LENGTH, 'a')));

			fixture.Customize<UserEntity>(f =>
					f.OmitAutoProperties()
					.Without(u => u.Id)
					.Without(u => u.Role)
					.Do(u => u.Username = string.Empty.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH - 1, 'a'))
					.Do(u => u.Email = TestConstants.VALID_EMAIL)
					.Do(u => u.Role = UserRole.Admin));

            fixture.Customize<ReviewAddViewModel>(f =>
                    f.OmitAutoProperties()
                    .Without(r => r.UserId)
                    .Without(r => r.StoreId)
					.Do(r => r.Rating = ValidationConstants.REVIEW_MIN_RATING)
					.Do(r => r.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH, 'a')));

            fixture.Customize<ReviewUpdateViewModel>(f =>
                f.OmitAutoProperties()
                    .Do(r => r.Rating = ValidationConstants.REVIEW_MIN_RATING)
                    .Do(r => r.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH, 'a')));

			fixture.Customize<ScheduleAddViewModel>(f =>
					f.OmitAutoProperties()
					.Without(s => s.StoreId)
					.Do(s => s.IsClosed = false)
					.Do(s => s.Day = DayOfWeekEu.Monday)
					.Do(s => s.OpeningTime = new TimeOnly(9, 30, 0))
					.Do(s => s.ClosingTime = new TimeOnly(21, 30, 0)));

			fixture.Customize<ScheduleUpdateViewModel>(f =>
				f.OmitAutoProperties()
					.Do(s => s.IsClosed = false)
					.Do(s => s.OpeningTime = new TimeOnly(9, 30, 0))
					.Do(s => s.ClosingTime = new TimeOnly(21, 30, 0)));

            fixture.Customize<StoreViewModel>(f =>
                    f.OmitAutoProperties()
                    .Do(s => s.Price = ValidationConstants.STORE_MIN_PRICE)
                    .Do(s => s.Address = string.Empty.PadRight(ValidationConstants.STORE_MAX_ADDRESS_LENGTH, 'a'))
                    .Do(s => s.Name = string.Empty.PadRight(ValidationConstants.STORE_MAX_NAME_LENGTH, 'a')));

			fixture.Customize<UserViewModel>(f =>
					f.OmitAutoProperties()
					.Do(u => u.Email = TestConstants.VALID_EMAIL));
		}
	}
}
