﻿using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.BLL.Interfaces
{
	public interface IUserService
	{
		Task<User> AddUserAsync(User userToAdd);
		Task DeleteUserAsync(int userToDeleteId);
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> GetByIdAsync(int id);
		Task<User> UpdateUserAsync(User userToUpdate);
	}
}
