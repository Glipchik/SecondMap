using AutoMapper;
using FluentValidation;
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
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IValidator<UserViewModel> _validator;

		public UsersController(IUserService userService, IMapper mapper, IValidator<UserViewModel> validator)
		{
			_userService = userService;
			_mapper = mapper;
			_validator = validator;
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
			var validationResult = await _validator.ValidateAsync(userToAdd);

			if (!validationResult.IsValid)
			{
				throw new Exception(ErrorMessages.VALIDATION_FAILED);
			}

			await _userService.AddUserAsync(_mapper.Map<User>(userToAdd));

			return Ok();
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserViewModel userToUpdate)
		{
			var validationResult = await _validator.ValidateAsync(userToUpdate);

			if (!validationResult.IsValid)
			{
				throw new Exception(ErrorMessages.VALIDATION_FAILED);
			}

			var mappedUserToUpdate = _mapper.Map<User>(userToUpdate);
			mappedUserToUpdate.Id = id;

			var updatedUser = _mapper.Map<UserDto>(await _userService.UpdateUserAsync(mappedUserToUpdate));

			return Ok(updatedUser);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundUser = await _userService.GetByIdAsync(id);

			await _userService.DeleteUserAsync(foundUser);

			return NoContent();
		}
	}
}
