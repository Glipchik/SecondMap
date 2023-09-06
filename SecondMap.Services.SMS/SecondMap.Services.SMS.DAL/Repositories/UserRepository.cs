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
	}
}
