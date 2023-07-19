using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{
		private readonly IStoreService _storeService;

		public StoreController(IStoreService storeService)
		{
			this._storeService = storeService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _storeService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundStore = await _storeService.GetByIdAsync(id);

			return Ok(foundStore);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Store storeToAdd)
		{
			await _storeService.AddStoreAsync(storeToAdd);

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] Store storeToUpdate)
		{
			if (id != storeToUpdate.Id)
			{
				return BadRequest();
			}

			var updatedStore = await _storeService.UpdateStoreAsync(storeToUpdate);

			return Ok(updatedStore);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundStore = await _storeService.GetByIdAsync(id);

			await _storeService.DeleteStoreAsync(foundStore);

			return NoContent();
		}
	}
}
