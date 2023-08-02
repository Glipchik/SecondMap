using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public class StoreRepository : GenericRepository<StoreEntity>, IStoreRepository
	{
		public StoreRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<StoreEntity?> GetByIdWithDetailsAsync(int storeId)
		{
			return await _dbContext.Stores
				.Where(s => s.Id == storeId)
				.Include(s => s.Reviews)
				.Include(s => s.Schedules)
				.FirstOrDefaultAsync();
		}

	}
}
