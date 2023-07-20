using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _repository;
		private readonly IMapper _mapper;

		public StoreService(IStoreRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<Store>> GetAllAsync()
		{
			return _mapper.Map<List<Store>>(await _repository.GetAllAsync());
		}

		public async Task<Store> GetByIdAsync(int id)
		{
			var foundStore = await _repository.GetByIdAsync(id);

			if (foundStore == null)
			{
				throw new Exception("Store not found");
			}

			return _mapper.Map<Store>(foundStore);
		}

		public async Task AddStoreAsync(Store storeToAdd)
		{
			await _repository.AddAsync(_mapper.Map<StoreEntity>(storeToAdd));
		}

		public async Task<Store> UpdateStoreAsync(Store storeToUpdate)
		{
			var updatedStore = await _repository.UpdateAsync(_mapper.Map<StoreEntity>(storeToUpdate));

			if (updatedStore == null)
			{
				throw new Exception("Store not found");
			}

			return _mapper.Map<Store>(updatedStore);
		}

		public async Task DeleteStoreAsync(Store storeToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<StoreEntity>(storeToDelete));
		}
	}
}

