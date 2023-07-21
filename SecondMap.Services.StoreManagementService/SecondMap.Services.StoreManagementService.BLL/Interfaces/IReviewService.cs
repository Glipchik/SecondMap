﻿using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IReviewService
	{
		Task<Review> AddReviewAsync(Review reviewToAdd);
		Task DeleteReviewAsync(Review reviewToDelete);
		Task<IEnumerable<Review>> GetAllAsync();
		Task<Review> GetByIdAsync(int id);
		Task<Review> UpdateReviewAsync(Review reviewToUpdate);
	}
}
