using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewService _reviewService;
		private readonly IMapper _mapper;

		public ReviewsController(IReviewService reviewService, IMapper mapper)
		{
			_reviewService = reviewService;
			_mapper = mapper;
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
		public async Task<IActionResult> AddAsync([FromBody] ReviewAddViewModel reviewToAdd)
		{
			var addedReview = _mapper.Map<ReviewDto>(await _reviewService.AddReviewAsync(_mapper.Map<Review>(reviewToAdd)));

			return Ok(addedReview);
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReviewUpdateViewModel reviewToUpdate)
		{
			var mappedReviewToUpdate = _mapper.Map<Review>(reviewToUpdate);
			mappedReviewToUpdate.Id = id;

			var updatedReview = _mapper.Map<ReviewDto>(await _reviewService.UpdateReviewAsync(mappedReviewToUpdate));

			return Ok(updatedReview);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _reviewService.DeleteReviewAsync(id);

			return NoContent();
		}

		[HttpGet(ApiEndpoints.STORE_ID_EQUALS + ApiEndpoints.ID)]
		public async Task<IActionResult> GetAllByStoreIdAsync(int id)
		{
			var foundReviews = _mapper.Map<IEnumerable<ReviewDto>>(await _reviewService.GetAllByStoreIdAsync(id));

			return Ok(foundReviews);
		}

		[HttpPatch(ApiEndpoints.RESTORE + ApiEndpoints.ID)]
		public async Task<IActionResult> RestoreByIdAsync(int id)
		{
			var restoredReview = _mapper.Map<ReviewDto>(await _reviewService.RestoreByIdAsync(id));

			return Ok(restoredReview);
		}
	}
}
