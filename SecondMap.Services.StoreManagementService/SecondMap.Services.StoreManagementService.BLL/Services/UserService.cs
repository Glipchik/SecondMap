using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using Serilog;

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

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return _mapper.Map<IEnumerable<User>>(await _repository.GetAllAsync());
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var foundUser = await _repository.GetByIdAsync(id);

			if (foundUser == null)
			{
        Log.Error("User with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}

			return _mapper.Map<User>(foundUser);
		}

		public async Task<User> AddUserAsync(User userToAdd)
		{
			var addedUser = _mapper.Map<User>(await _repository.AddAsync(_mapper.Map<UserEntity>(userToAdd)));

			Log.Information("Added user: {@addedUser}", addedUser);

			return addedUser;
		}

		public async Task<User> UpdateUserAsync(User userToUpdate)
		{
			var updatedUser = await _repository.UpdateAsync(_mapper.Map<UserEntity>(userToUpdate));

			if (updatedUser == null)
			{
        Log.Error("User with id = {@id} not found", userToUpdate.Id);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}

			Log.Information("Updated user: {@addedUser}", updatedUser);

			return _mapper.Map<User>(updatedUser);
		}

		public async Task DeleteUserAsync(int userToDeleteId)
		{
			var isDeleted = await _repository.DeleteAsync(userToDeleteId);
			if (!isDeleted) throw new Exception(ErrorMessages.USER_NOT_FOUND);
		}
	}
}
