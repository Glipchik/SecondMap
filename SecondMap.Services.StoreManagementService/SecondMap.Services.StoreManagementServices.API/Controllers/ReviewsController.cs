﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Constants;
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
		public async Task<IActionResult> AddAsync([FromBody] ReviewViewModel reviewToAdd)
		{
			await _reviewService.AddReviewAsync(_mapper.Map<Review>(reviewToAdd));

			return Ok();
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReviewViewModel reviewToUpdate)
		{
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
