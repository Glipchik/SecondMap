using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class StoreRepository : GenericRepository<Store>, IStoreRepository
	{
		public StoreRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
