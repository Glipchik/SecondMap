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
		public async Task<StoreEntity?> FindDeletedByIdAsync(int id)
		{
			return await _dbContext.Stores.IgnoreQueryFilters()
				.Include(s => s.Reviews!
					.Where(r => r.IsDeleted == true))
				.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == true);
		}

		public async Task RestoreDeletedEntityAsync(StoreEntity storeEntity)
		{
			storeEntity.IsDeleted = false;
			storeEntity.DeletedAt = null;

			_dbContext.Entry(storeEntity).State = EntityState.Modified;

			await _dbContext.SaveChangesAsync();
		}

		public async Task RestoreStoreReviewsAsync(StoreEntity storeEntity)
		{
			if (storeEntity.Reviews != null)
			{
				foreach (var reviewEntity in storeEntity.Reviews)
				{
					reviewEntity.IsDeleted = false;
					reviewEntity.DeletedAt = null;
					_dbContext.Entry(reviewEntity).State = EntityState.Modified;
				}
			}

			await _dbContext.SaveChangesAsync();
		}
	}
}
