﻿using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<User>> GetAllAsync()
		{
			return _mapper.Map<List<User>>(await _repository.GetAllAsync());
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var foundUser = await _repository.GetByIdAsync(id);

			if (foundUser == null)
			{
				throw new NotFoundException("User not found");
			}

			return _mapper.Map<User>(foundUser);
		}

		public async Task AddUserAsync(User userToAdd)
		{
			await _repository.AddAsync(_mapper.Map<UserEntity>(userToAdd));
		}

		public async Task<User> UpdateUserAsync(User userToUpdate)
		{
			var updatedUser = await _repository.UpdateAsync(_mapper.Map<UserEntity>(userToUpdate));

			if (updatedUser == null)
			{
				throw new NotFoundException("User not found");
			}

			return _mapper.Map<User>(updatedUser);
		}

		public async Task DeleteUserAsync(User userToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<UserEntity>(userToDelete));
		}
	}
}
