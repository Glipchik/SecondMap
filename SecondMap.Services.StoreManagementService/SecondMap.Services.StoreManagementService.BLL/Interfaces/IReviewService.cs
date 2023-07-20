using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IReviewService
	{
		Task AddReviewAsync(Review reviewToAdd);
		Task DeleteReviewAsync(Review reviewToDelete);
		Task<List<Review>> GetAllAsync();
		Task<Review> GetByIdAsync(int id);
		Task<Review> UpdateReviewAsync(Review reviewToUpdate);
	}
}
