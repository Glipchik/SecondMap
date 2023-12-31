﻿using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;

namespace SecondMap.Services.SMS.DAL.Repositories
{
	public class UserRepository : GenericRepository<UserEntity>, IUserRepository
	{
		public UserRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}

		public override async Task<UserEntity?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<UserEntity>()
				.Where(u => u.Id == id)
				.Include(u => u.Role)
				.FirstOrDefaultAsync();
		}

		public override async Task<IEnumerable<UserEntity>> GetAllAsync()
		{
			return await _dbContext.Set<UserEntity>()
				.Include(u => u.Role)
				.ToListAsync();
		}
	}
}
