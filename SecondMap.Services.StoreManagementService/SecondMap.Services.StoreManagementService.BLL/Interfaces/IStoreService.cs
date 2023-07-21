﻿using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IStoreService
	{
		Task<Store> AddStoreAsync(Store storeToAdd);
		Task DeleteStoreAsync(Store storeToDelete);
		Task<IEnumerable<Store>> GetAllAsync();
		Task<Store> GetByIdAsync(int id);
		Task<Store> UpdateStoreAsync(Store storeToUpdate);
	}
}
