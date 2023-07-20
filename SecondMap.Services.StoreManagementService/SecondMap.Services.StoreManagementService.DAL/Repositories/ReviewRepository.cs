using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class ReviewRepository : GenericRepository<ReviewEntity>, IReviewRepository
	{
		public ReviewRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
