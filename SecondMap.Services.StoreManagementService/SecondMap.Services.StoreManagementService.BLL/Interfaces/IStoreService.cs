using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IStoreService
	{
		Task AddStoreAsync(Store storeToAdd);
		Task DeleteStoreAsync(Store storeToDelete);
		Task<List<Store>> GetAllAsync();
		Task<Store> GetByIdAsync(int id);
		Task<Store> UpdateStoreAsync(Store storeToUpdate);
	}
}
