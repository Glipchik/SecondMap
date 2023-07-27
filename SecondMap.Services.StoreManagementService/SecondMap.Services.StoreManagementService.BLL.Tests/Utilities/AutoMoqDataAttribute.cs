﻿namespace SecondMap.Services.StoreManagementService.UnitTests.Utilities
{
	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute() : base(CreateFixture)
		{

		}

		private static IFixture CreateFixture()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
			return fixture;
		}
	}
}
