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
			fixture.Customize(new CompositeCustomization(
				new AutoMoqCustomization { ConfigureMembers = true },
				new ServiceCustomization()));
			return fixture;
		}
	}

	public class ServiceCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register<IReviewService>(CreateReviewService);
			fixture.Register<IScheduleService>(CreateScheduleService);
			fixture.Register<IStoreService>(CreateStoreService);
			fixture.Register<IUserService>(CreateUserService);
		}

		private static ReviewService CreateReviewService() => new ReviewService(new Mock<IReviewRepository>().Object, new Mock<IMapper>().Object);
		private static ScheduleService CreateScheduleService() => new ScheduleService(new Mock<IScheduleRepository>().Object, new Mock<IMapper>().Object);
		private static StoreService CreateStoreService() => new StoreService(new Mock<IStoreRepository>().Object, new Mock<IMapper>().Object);
		private static UserService CreateUserService() => new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object);
	}
}