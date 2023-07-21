using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewService _reviewService;
		private readonly IMapper _mapper;
		private readonly IValidator<ReviewViewModel> _validator;

		public ReviewsController(IReviewService reviewService, IMapper mapper, IValidator<ReviewViewModel> validator)
		{
			_reviewService = reviewService;
			_mapper = mapper;
			_validator = validator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IEnumerable<ReviewDto>>(await _reviewService.GetAllAsync()));
		}

		[HttpGet(ApiEndpoints.ID)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundReview = _mapper.Map<ReviewDto>(await _reviewService.GetByIdAsync(id));

			return Ok(foundReview);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] ReviewViewModel reviewToAdd)
		{
			var validationResult = await _validator.ValidateAsync(reviewToAdd);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException(ErrorMessages.VALIDATION_FAILED);
			}

			await _reviewService.AddReviewAsync(_mapper.Map<Review>(reviewToAdd));

			return Ok();
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReviewViewModel reviewToUpdate)
		{
			var validationResult = await _validator.ValidateAsync(reviewToUpdate);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException(ErrorMessages.VALIDATION_FAILED);
			}

			var mappedReviewToUpdate = _mapper.Map<Review>(reviewToUpdate);
			mappedReviewToUpdate.Id = id;

			var updatedReview = _mapper.Map<ReviewDto>(await _reviewService.UpdateReviewAsync(mappedReviewToUpdate));

			return Ok(updatedReview);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundReview = await _reviewService.GetByIdAsync(id);

			await _reviewService.DeleteReviewAsync(foundReview);

			return NoContent();
		}
	}
}
