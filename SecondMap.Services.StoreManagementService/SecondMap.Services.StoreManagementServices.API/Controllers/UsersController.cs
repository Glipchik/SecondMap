﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
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
			var validationResult = await _validator.ValidateAsync(userToAdd);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException($"Validation failed: {validationResult.Errors[0]}");
			}

			await _userService.AddUserAsync(_mapper.Map<User>(userToAdd));

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserViewModel userToUpdate)
		{
			var validationResult = await _validator.ValidateAsync(userToUpdate);

			if (!validationResult.IsValid)
			{
				throw new ValidationFailException($"Validation failed: {validationResult.Errors[0]}");
			}

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
