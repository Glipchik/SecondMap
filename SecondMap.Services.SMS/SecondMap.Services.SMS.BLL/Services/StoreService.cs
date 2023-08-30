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
				Log.Error("Store with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			return _mapper.Map<Store>(foundStore);
		}

		public async Task<Store> AddStoreAsync(Store storeToAdd)
		{
			var addedStore = _mapper.Map<Store>(await _repository.AddAsync(_mapper.Map<StoreEntity>(storeToAdd)));

			Log.Information("Added store: {@addedStore}", addedStore);

			return addedStore;
		}

		public async Task<Store> UpdateStoreAsync(Store storeToUpdate)
		{
			if (!await _repository.ExistsWithId(storeToUpdate.Id))
			{
				Log.Error("Store with id = {@id} not found", storeToUpdate.Id);
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			var updatedStore = await _repository.UpdateAsync(_mapper.Map<StoreEntity>(storeToUpdate));

			Log.Information("Updated store: {@updatedStore}", updatedStore);

			return _mapper.Map<Store>(updatedStore);
		}

		public async Task DeleteStoreAsync(int storeToDeleteId)
		{
			var entityToDelete = await _repository.GetByIdAsync(storeToDeleteId);
			if (entityToDelete == null)
			{
				Log.Error("Store with id = {@id} not found", storeToDeleteId);
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			await _repository.DeleteAsync(entityToDelete);
		}

		public async Task<Store> GetByIdWithDetailsAsync(int id)
		{
			var foundStore = await _repository.GetByIdWithDetailsAsync(id);

			if (foundStore == null)
			{
				Log.Error("Store with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			return _mapper.Map<Store>(foundStore);
		}

		public async Task<Store> RestoreByIdAsync(int id, bool withReviews)
		{
			if (await _repository.ExistsWithId(id))
			{
				Log.Error("Store with id = {@id} already exists", id);
				throw new AlreadyExistsException(ErrorMessages.STORE_ALREADY_EXISTS);
			}

			var restoredStore = await _repository.FindDeletedByIdAsync(id);

			if (restoredStore == null)
			{
				Log.Error("Deleted store with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.STORE_NOT_FOUND);
			}

			await _repository.RestoreDeletedEntityAsync(restoredStore);

			if (withReviews)
			{
				await _repository.RestoreStoreReviewsAsync(restoredStore);
			}

			return _mapper.Map<Store>(restoredStore);
		}
	}
}

