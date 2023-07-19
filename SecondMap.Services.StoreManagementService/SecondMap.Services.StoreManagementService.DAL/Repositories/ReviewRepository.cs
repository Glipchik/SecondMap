using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class ReviewRepository : GenericRepository<Review>, IReviewRepository
	{
		public ReviewRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
