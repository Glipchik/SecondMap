using SecondMap.Services.StoreManagementService.DAL.Context;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Repositories
{
	internal class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
	{
		public ScheduleRepository(StoreManagementDbContext dbContext) : base(dbContext)
		{
		}
	}
}
