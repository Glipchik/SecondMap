﻿using AutoMapper;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Exceptions;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;
using Serilog;

namespace SecondMap.Services.SMS.BLL.Services
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

		public async Task<IEnumerable<Review>> GetAllByStoreIdAsync(int storeId)
		{
			var foundReviews = await _repository.GetAllByPredicateAsync(r => r.StoreId == storeId);

			if (!foundReviews.Any())
			{
				Log.Error("Reviews for store with id = {@id} not found", storeId);
				throw new NotFoundException(ErrorMessages.REVIEWS_FOR_STORE_NOT_FOUND);
			}

			return _mapper.Map<IEnumerable<Review>>(foundReviews);
		}

		public async Task<Review> RestoreByIdAsync(int id)
		{
			if (await _repository.ExistsWithId(id))
			{
				Log.Error("Review with id = {@id} already exists", id);
				throw new AlreadyExistsException(ErrorMessages.REVIEW_ALREADY_EXISTS);
			}

			var restoredReview = await _repository.FindDeletedByIdAsync(id);

			if (restoredReview == null)
			{
				Log.Error("Deleted review with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.REVIEW_NOT_FOUND);
			}

			await _repository.RestoreDeletedEntityAsync(restoredReview);

			return _mapper.Map<Review>(restoredReview);
		}
	}
}
