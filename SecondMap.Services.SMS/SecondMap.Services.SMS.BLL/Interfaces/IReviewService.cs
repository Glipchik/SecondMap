using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.BLL.Interfaces
{
	public interface IReviewService
	{
		Task<Review> AddReviewAsync(Review reviewToAdd);
		Task DeleteReviewAsync(int reviewToDeleteId);
		Task<IEnumerable<Review>> GetAllAsync();
		Task<Review> GetByIdAsync(int id);
		Task<Review> UpdateReviewAsync(Review reviewToUpdate);
		Task<IEnumerable<Review>> GetAllByStoreIdAsync(int storeId);
		Task<Review> RestoreByIdAsync(int id);
	}
}
