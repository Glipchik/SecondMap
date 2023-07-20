using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IReviewRepository _repository;

		public ReviewService(IReviewRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<Review>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Review> GetByIdAsync(int id)
		{
			var foundReview = await _repository.GetByIdAsync(id);

			if (foundReview == null)
			{
				throw new Exception("Review not found");
			}

			return foundReview;
		}

		public async Task AddReviewAsync(Review reviewToAdd)
		{
			await _repository.AddAsync(reviewToAdd);
		}

		public async Task<Review> UpdateReviewAsync(Review reviewToUpdate)
		{
			var updatedReview = await _repository.UpdateAsync(reviewToUpdate);

			if (updatedReview == null)
			{
				throw new Exception("Review not found");
			}

			return updatedReview;
		}

		public async Task DeleteReviewAsync(Review reviewToDelete)
		{
			await _repository.DeleteAsync(reviewToDelete);
		}
	}
}
