using SecondMap.Services.StoreManagementService.DAL.Models;

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
