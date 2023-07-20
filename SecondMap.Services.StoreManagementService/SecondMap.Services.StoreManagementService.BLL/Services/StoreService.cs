using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _repository;

		public StoreService(IStoreRepository repository)
		{
			_repository = repository;
		}

		// TODO: implement DTO to not pass DAL models through all layers
		public async Task<List<Store>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		// TODO: add custom exceptions to pass in exception middleware
		public async Task<Store> GetByIdAsync(int id)
		{
			var foundStore = await _repository.GetByIdAsync(id);

			if (foundStore == null)
			{
				throw new Exception("Store not found");
			}

			return foundStore;
		}

		// TODO: add validation
		public async Task AddStoreAsync(Store storeToAdd)
		{
			await _repository.AddAsync(storeToAdd);
		}

		public async Task<Store> UpdateStoreAsync(Store storeToUpdate)
		{
			var updatedStore = await _repository.UpdateAsync(storeToUpdate);

			if (updatedStore == null)
			{
				throw new Exception("Store not found");
			}

			return updatedStore;
		}

		public async Task DeleteStoreAsync(Store storeToDelete)
		{
			await _repository.DeleteAsync(storeToDelete);
		}
	}
}

