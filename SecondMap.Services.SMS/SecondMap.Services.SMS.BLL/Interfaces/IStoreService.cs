using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.BLL.Interfaces
{
	public interface IStoreService
	{
		Task<Store> AddStoreAsync(Store storeToAdd);
		Task DeleteStoreAsync(int storeToDeleteId);
		Task<IEnumerable<Store>> GetAllAsync();
		Task<Store> GetByIdAsync(int id);
		Task<Store> UpdateStoreAsync(Store storeToUpdate);
	}
}
