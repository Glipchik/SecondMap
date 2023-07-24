using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
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

		public async Task<IEnumerable<Store>> GetAllAsync()
		{
			return _mapper.Map<IEnumerable<Store>>(await _repository.GetAllAsync());
		}

		public async Task<Store> GetByIdAsync(int id)
		{
			var foundStore = await _repository.GetByIdAsync(id);

			if (foundStore == null)
			{
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			return _mapper.Map<Store>(foundStore);
		}

		public async Task<Store> AddStoreAsync(Store storeToAdd)
		{
			return _mapper.Map<Store>(await _repository.AddAsync(_mapper.Map<StoreEntity>(storeToAdd)));
		}

		public async Task<Store> UpdateStoreAsync(Store storeToUpdate)
		{
			var updatedStore = await _repository.UpdateAsync(_mapper.Map<StoreEntity>(storeToUpdate));

			if (updatedStore == null)
			{
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			return _mapper.Map<Store>(updatedStore);
		}

		public async Task DeleteStoreAsync(Store storeToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<StoreEntity>(storeToDelete));
		}
	}
}

