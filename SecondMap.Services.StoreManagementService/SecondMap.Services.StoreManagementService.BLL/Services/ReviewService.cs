using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using Serilog;

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

		public async Task<IEnumerable<Review>> GetAllAsync()
		{
			return _mapper.Map<IEnumerable<Review>>(await _repository.GetAllAsync());
		}

		public async Task<Review> GetByIdAsync(int id)
		{
			var foundReview = await _repository.GetByIdAsync(id);

			if (foundReview == null)
			{
				Log.Error("Review with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.REVIEW_NOT_FOUND);
			}

			return _mapper.Map<Review>(foundReview);
		}

		public async Task<Review> AddReviewAsync(Review reviewToAdd)
		{
			var addedReview = _mapper.Map<Review>(await _repository.AddAsync(_mapper.Map<ReviewEntity>(reviewToAdd)));

			Log.Information("Added review: {@addedReview}", addedReview);

			return addedReview;
		}

		public async Task<Review> UpdateReviewAsync(Review reviewToUpdate)
		{
			if (!await _repository.ExistsWithId(reviewToUpdate.Id))
			{
				Log.Error("Review with id = {@id} not found", reviewToUpdate.Id);
				throw new NotFoundException(ErrorMessages.REVIEW_NOT_FOUND);
			}

			var updatedReview = await _repository.UpdateAsync(_mapper.Map<ReviewEntity>(reviewToUpdate));

			Log.Information("Updated review: {@updatedReview}", updatedReview);

			return _mapper.Map<Review>(updatedReview);
		}

		public async Task DeleteReviewAsync(int reviewToDeleteId)
		{
			var reviewToDelete = await _repository.GetByIdAsync(reviewToDeleteId);
			if (reviewToDelete == null)
			{
				Log.Error("Review with id = {@id} not found", reviewToDeleteId);
				throw new NotFoundException(ErrorMessages.REVIEW_NOT_FOUND);
			}

			await _repository.DeleteAsync(reviewToDelete);
		}
	}
}
