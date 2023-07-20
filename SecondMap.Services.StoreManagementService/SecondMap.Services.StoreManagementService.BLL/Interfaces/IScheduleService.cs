using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IScheduleService
	{
		Task AddScheduleAsync(Schedule scheduleToAdd);
		Task DeleteScheduleAsync(Schedule scheduleToDelete);
		Task<List<Schedule>> GetAllAsync();
		Task<Schedule> GetByIdAsync(int id);
		Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate);
	}
}
