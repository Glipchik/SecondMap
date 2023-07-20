using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewsController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _reviewService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundReview = await _reviewService.GetByIdAsync(id);

			return Ok(foundReview);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Review reviewToAdd)
		{
			await _reviewService.AddReviewAsync(reviewToAdd);

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] Review reviewToUpdate)
		{
			if (id != reviewToUpdate.Id)
			{
				return BadRequest();
			}

			var updatedReview = await _reviewService.UpdateReviewAsync(reviewToUpdate);

			return Ok(updatedReview);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundReview = await _reviewService.GetByIdAsync(id);

			await _reviewService.DeleteReviewAsync(foundReview);

			return NoContent();
		}
	}
}
