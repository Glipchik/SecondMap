using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var foundUser = await _repository.GetByIdAsync(id);

			if (foundUser == null)
			{
				throw new Exception("User not found");
			}

			return foundUser;
		}

		public async Task AddUserAsync(User userToAdd)
		{
			await _repository.AddAsync(userToAdd);
		}

		public async Task<User> UpdateUserAsync(User userToUpdate)
		{
			var updatedUser = await _repository.UpdateAsync(userToUpdate);

			if (updatedUser == null)
			{
				throw new Exception("User not found");
			}

			return updatedUser;
		}

		public async Task DeleteUserAsync(User userToDelete)
		{
			await _repository.DeleteAsync(userToDelete);
		}
	}
}
