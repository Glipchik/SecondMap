using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Interfaces
{
	public interface IStoreRepository : IGenericRepository<StoreEntity>
	{
		public Task<StoreEntity?> GetByIdWithDetailsAsync(int storeId);
		public Task<StoreEntity?> FindDeletedByIdAsync(int id);
		public Task RestoreDeletedEntityAsync(StoreEntity storeEntity);
		public Task RestoreStoreReviewsAsync(StoreEntity storeEntity);
	}
}
