using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
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
			return Ok(_mapper.Map<IEnumerable<UserDto>>(await _userService.GetAllAsync()));
		}

		[HttpGet(ApiEndpoints.ID)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundUser = _mapper.Map<UserDto>(await _userService.GetByIdAsync(id));

			return Ok(foundUser);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] UserViewModel userToAdd)
		{
			var addedUser = _mapper.Map<UserDto>(await _userService.AddUserAsync(_mapper.Map<User>(userToAdd)));

			return Ok(addedUser);
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserViewModel userToUpdate)
		{
			var mappedUserToUpdate = _mapper.Map<User>(userToUpdate);
			mappedUserToUpdate.Id = id;

			var updatedUser = _mapper.Map<UserDto>(await _userService.UpdateUserAsync(mappedUserToUpdate));

			return Ok(updatedUser);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _userService.DeleteUserAsync(id);

			return NoContent();
		}
	}
}
