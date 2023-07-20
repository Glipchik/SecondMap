using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IUserService
	{
		Task AddUserAsync(User userToAdd);
		Task DeleteUserAsync(User userToDelete);
		Task<List<User>> GetAllAsync();
		Task<User> GetByIdAsync(int id);
		Task<User> UpdateUserAsync(User userToUpdate);
	}
}
