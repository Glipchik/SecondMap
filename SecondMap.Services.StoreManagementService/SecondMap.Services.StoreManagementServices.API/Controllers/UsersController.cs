using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _userService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundUser = await _userService.GetByIdAsync(id);

			return Ok(foundUser);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] User userToAdd)
		{
			await _userService.AddUserAsync(userToAdd);

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] User userToUpdate)
		{
			if (id != userToUpdate.Id)
			{
				return BadRequest();
			}

			var updatedUser = await _userService.UpdateUserAsync(userToUpdate);

			return Ok(updatedUser);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundUser = await _userService.GetByIdAsync(id);

			await _userService.DeleteUserAsync(foundUser);

			return NoContent();
		}
	}
}
