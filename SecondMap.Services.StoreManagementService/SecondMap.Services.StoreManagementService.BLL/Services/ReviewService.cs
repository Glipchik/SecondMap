using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IReviewRepository _repository;
		private readonly IMapper _mapper;

		public ReviewService(IReviewRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<Review>> GetAllAsync()
		{
			return _mapper.Map<List<Review>>(await _repository.GetAllAsync());
		}

		public async Task<Review> GetByIdAsync(int id)
		{
			var foundReview = await _repository.GetByIdAsync(id);

			if (foundReview == null)
			{
				throw new NotFoundException("Review not found");
			}

			return _mapper.Map<Review>(foundReview);
		}

		public async Task AddReviewAsync(Review reviewToAdd)
		{
			await _repository.AddAsync(_mapper.Map<ReviewEntity>(reviewToAdd));
		}

		public async Task<Review> UpdateReviewAsync(Review reviewToUpdate)
		{
			var updatedReview = await _repository.UpdateAsync(_mapper.Map<ReviewEntity>(reviewToUpdate));

			if (updatedReview == null)
			{
				throw new NotFoundException("Review not found");
			}

			return _mapper.Map<Review>(updatedReview);
		}

		public async Task DeleteReviewAsync(Review reviewToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<ReviewEntity>(reviewToDelete));
		}
	}
}
