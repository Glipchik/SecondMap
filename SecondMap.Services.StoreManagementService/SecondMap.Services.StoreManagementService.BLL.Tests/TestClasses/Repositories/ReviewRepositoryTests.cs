using Microsoft.EntityFrameworkCore;

namespace SecondMap.Services.StoreManagementService.UnitTests.TestClasses.Repositories
{
	public class ReviewRepositoryTests
	{
		private readonly Mock<StoreManagementDbContext> _contextMock;
		private readonly Mock<DbSet<ReviewEntity>> _dbSetMock;
		private readonly ReviewRepository _repository;

		public ReviewRepositoryTests()
		{
			_contextMock = new Mock<StoreManagementDbContext>(new DbContextOptions<StoreManagementDbContext>());
			_dbSetMock = new Mock<DbSet<ReviewEntity>>();
			_repository = new ReviewRepository(_contextMock.Object);
		}

		[Theory]
		[AutoMoqData]
		public async Task AddAsync_ShouldAddReviewEntity(
			ReviewEntity entityToAdd)
		{
			// Arrange
			
			// Act
			var addedEntity = await _repository.AddAsync(entityToAdd);

			// Assert
			addedEntity.Should().NotBeNull();
			addedEntity.Should().BeOfType<ReviewEntity>();
			_contextMock.Verify(context => context.Set<ReviewEntity>().AddAsync(addedEntity, default), Times.Once);
			_contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
		}
	}
}
