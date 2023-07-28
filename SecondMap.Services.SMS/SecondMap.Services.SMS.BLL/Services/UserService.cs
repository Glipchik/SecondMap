using AutoMapper;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Exceptions;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;
using Serilog;

namespace SecondMap.Services.SMS.BLL.Services
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
			if (!await _repository.ExistsWithId(userToUpdate.Id))
			{
				Log.Error("User with id = {@id} not found", userToUpdate.Id);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}

			var updatedUser = await _repository.UpdateAsync(_mapper.Map<UserEntity>(userToUpdate));

			Log.Information("Updated user: {@addedUser}", updatedUser);

			return _mapper.Map<User>(updatedUser);
		}

		public async Task DeleteUserAsync(int userToDeleteId)
		{
			var entityToDelete = await _repository.GetByIdAsync(userToDeleteId);
			if (entityToDelete == null)
			{
				Log.Error("User with id = {@id} not found", userToDeleteId);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}
			await _repository.DeleteAsync(entityToDelete);
		}
	}
}
