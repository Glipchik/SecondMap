using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class ScheduleRepository : GenericRepository<ScheduleEntity>, IScheduleRepository
	{
		public ScheduleRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
