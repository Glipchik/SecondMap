using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace SecondMap.Services.StoreManagementService.BLL.Tests.TestClasses
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
