namespace SecondMap.Services.SMS.IntegrationTests.DataSetups
{
	public class IntegrationTestsAutoDataAttribute : AutoDataAttribute
	{
		public IntegrationTestsAutoDataAttribute() : base(CreateFixture) { }

		private static IFixture CreateFixture()
		{
			var fixture = new Fixture()
				.Customize(new IntegrationTestCustomization());

			return fixture;
		}
	}
}
