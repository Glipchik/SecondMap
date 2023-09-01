using AutoMapper;
using MassTransit;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Exceptions;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;
using SecondMap.Shared.Messages;
using Serilog;

namespace SecondMap.Services.SMS.BLL.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;
		private readonly IRequestClient<UpdateUserCommand> _updateUserRequestClient;
		public UserService(IUserRepository repository, IMapper mapper, IRequestClient<UpdateUserCommand> updateUserRequestClient)
		{
			_repository = repository;
			_mapper = mapper;
			_updateUserRequestClient = updateUserRequestClient;
		}

		public async Task AddUserAsync(User userToAdd)
		{
			var addedUser = await _repository.AddAsync(_mapper.Map<UserEntity>(userToAdd));

			Log.Information("Added user\n{@addedUser}", addedUser);
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

		public async Task<User> UpdateUserAsync(User userToUpdate)
		{
			if (!await _repository.ExistsWithId(userToUpdate.Id))
			{
				Log.Error("User with id = {@id} not found", userToUpdate.Id);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}

			if ((await _repository.GetAllByPredicateAsync(u => u.Email == userToUpdate.Email)).Any())
			{
				Log.Error("Email {@userToUpdateEmail} is already taken", userToUpdate.Email);
				throw new AlreadyExistsException(ErrorMessages.USER_EMAIL_TAKEN);
			}

			var userBeforeUpdate = await GetByIdAsync(userToUpdate.Id);

			var updatedUser = await _repository.UpdateAsync(_mapper.Map<UserEntity>(userToUpdate));

			Log.Information("Updated user: {@addedUser}", updatedUser);

			Log.Information("Sent message: Update user in Identity Server {@userToUpdate}", userToUpdate);

			var updateUserInIdentityResponse = await
				_updateUserRequestClient.GetResponse<UpdateUserResponse>(new UpdateUserCommand(userBeforeUpdate.Email, updatedUser!.Email,
					(int)updatedUser.Role!));

			if (!updateUserInIdentityResponse.Message.IsSuccessful)
			{
				Log.Error("Failed to update user {@updatedUser} in Identity Server. Rolling back.", updatedUser);
				await _repository.UpdateAsync(_mapper.Map<UserEntity>(userBeforeUpdate));
			}

			Log.Information("Successfully updated in Identity");

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

		public async Task<User> GetByEmailAsync(string email)
		{
			var foundUser = (await _repository.GetAllByPredicateAsync(u => u.Email == email)).ToList();
			if (!foundUser.Any())
			{
				Log.Error("User with email = {@email} not found", email);
				throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
			}

			return _mapper.Map<User>(foundUser.First());
		}
	}
}
