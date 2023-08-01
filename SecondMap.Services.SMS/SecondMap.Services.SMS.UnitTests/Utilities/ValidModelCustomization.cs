using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;

namespace SecondMap.Services.SMS.UnitTests.Utilities
{
	public class ValidModelCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => fixture.Behaviors.Remove(b));
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			fixture.Customize<ReviewAddViewModel>(f =>
				f.OmitAutoProperties()
					.Do(r => r.UserId = ValidationConstants.INVALID_ID + 1)
					.Do(r => r.StoreId = ValidationConstants.INVALID_ID + 1)
					.Do(r => r.Rating = ValidationConstants.REVIEW_MIN_RATING + 1)
					.Do(r => r.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH, 'a')));

			fixture.Customize<ReviewUpdateViewModel>(f =>
				f.OmitAutoProperties()
					.Do(r => r.Rating = ValidationConstants.REVIEW_MIN_RATING + 1)
					.Do(r => r.Description = string.Empty.PadRight(ValidationConstants.REVIEW_MAX_DESCRIPTION_LENGTH, 'a')));

			fixture.Customize<ScheduleAddViewModel>(f =>
				f.OmitAutoProperties()
					.Do(s => s.StoreId = ValidationConstants.INVALID_ID + 1)
					.Do(s => s.IsClosed = false)
					.Do(s => s.Day = DayOfWeekEu.Monday)
					.Do(s => s.OpeningTime = new TimeOnly(9, 30, 0))
					.Do(s => s.ClosingTime = new TimeOnly(21, 30, 0)));

			fixture.Customize<StoreViewModel>(f =>
				f.OmitAutoProperties()
					.Do(s => s.Price = ValidationConstants.STORE_MIN_PRICE + 1)
					.Do(s => s.Address = string.Empty.PadRight(ValidationConstants.STORE_MAX_ADDRESS_LENGTH, 'a'))
					.Do(s => s.Name = string.Empty.PadRight(ValidationConstants.STORE_MAX_NAME_LENGTH, 'a')));

			fixture.Customize<UserViewModel>(f =>
				f.OmitAutoProperties()
					.Do(u => u.Username = string.Empty.PadRight(ValidationConstants.USER_NAME_MAX_LENGTH, 'a'))
					.Do(u => u.Password = string.Empty.PadRight(ValidationConstants.USER_PASSWORD_MIN_LENGTH, 'a')));
		}
	}
}
