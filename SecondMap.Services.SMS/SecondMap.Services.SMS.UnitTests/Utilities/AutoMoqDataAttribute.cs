namespace SecondMap.Services.SMS.UnitTests.Utilities
{
	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute() : base(CreateFixture)
		{

		}

		private static IFixture CreateFixture()
		{
			var fixture = new Fixture();
			fixture.Customize(new ValidModelCustomization());
			return fixture;
		}
	}
}
