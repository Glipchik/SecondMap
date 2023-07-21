using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
	public interface IScheduleService
	{
		Task AddScheduleAsync(Schedule scheduleToAdd);
		Task DeleteScheduleAsync(Schedule scheduleToDelete);
		Task<IEnumerable<Schedule>> GetAllAsync();
		Task<Schedule> GetByIdAsync(int id);
		Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate);
	}
}
