using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		public UsersController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<List<UserDto>>(await _userService.GetAllAsync()));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundUser = _mapper.Map<UserDto>(await _userService.GetByIdAsync(id));

			return Ok(foundUser);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] UserViewModel userToAdd)
		{
			await _userService.AddUserAsync(_mapper.Map<User>(userToAdd));

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserViewModel userToUpdate)
		{
			var mappedUserToUpdate = _mapper.Map<User>(userToUpdate);
			mappedUserToUpdate.Id = id;

			var updatedUser = _mapper.Map<UserDto>(await _userService.UpdateUserAsync(mappedUserToUpdate));

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
