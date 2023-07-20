using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class StoreRepository : GenericRepository<StoreEntity>, IStoreRepository
	{
		public StoreRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
