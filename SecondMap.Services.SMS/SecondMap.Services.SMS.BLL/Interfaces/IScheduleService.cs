using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.BLL.Interfaces
{
	public interface IScheduleService
	{
		Task<Schedule> AddScheduleAsync(Schedule scheduleToAdd);
		Task DeleteScheduleAsync(int scheduleToDeleteId);
		Task<IEnumerable<Schedule>> GetAllAsync();
		Task<Schedule> GetByIdAsync(int id);
		Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate);
	}
}
