using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Interfaces
{
	public interface IReviewRepository : IGenericRepository<ReviewEntity>
	{
		public Task<ReviewEntity?> FindDeletedByIdAsync(int id);

		public Task RestoreDeletedEntityAsync(ReviewEntity storeEntity);
	}
}
