using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
    public class ScheduleService : IScheduleService
	{
		private readonly IScheduleRepository _repository;

		public ScheduleService(IScheduleRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<Schedule>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Schedule> GetByIdAsync(int id)
		{
			var foundSchedule = await _repository.GetByIdAsync(id);

			if (foundSchedule == null)
			{
				throw new Exception("Schedule not found");
			}

			return foundSchedule;
		}

		public async Task AddScheduleAsync(Schedule scheduleToAdd)
		{
			await _repository.AddAsync(scheduleToAdd);
		}

		public async Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate)
		{
			var updatedSchedule = await _repository.UpdateAsync(scheduleToUpdate);

			if (updatedSchedule == null)
			{
				throw new Exception("Schedule not found");
			}

			return updatedSchedule;
		}

		public async Task DeleteScheduleAsync(Schedule scheduleToDelete)
		{
			await _repository.DeleteAsync(scheduleToDelete);
		}
	}
}
