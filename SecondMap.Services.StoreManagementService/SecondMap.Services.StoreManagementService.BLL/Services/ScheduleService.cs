using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using Serilog;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly IScheduleRepository _repository;
		private readonly IMapper _mapper;

		public ScheduleService(IScheduleRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<Schedule>> GetAllAsync()
		{
			return _mapper.Map<IEnumerable<Schedule>>(await _repository.GetAllAsync());
		}

		public async Task<Schedule> GetByIdAsync(int id)
		{
			var foundSchedule = await _repository.GetByIdAsync(id);

			if (foundSchedule == null)
			{
        Log.Error("Schedule with id = {@id} not found", id);
				throw new NotFoundException(ErrorMessages.SCHEDULE_NOT_FOUND);
			}

			return _mapper.Map<Schedule>(foundSchedule);
		}

		public async Task<Schedule> AddScheduleAsync(Schedule scheduleToAdd)
		{
			var addedSchedule = _mapper.Map<Schedule>(await _repository.AddAsync(_mapper.Map<ScheduleEntity>(scheduleToAdd)));

			Log.Information("Added schedule: {@addedSchedule}", addedSchedule);

			return addedSchedule;
		}

		public async Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate)
		{
			var updatedSchedule = await _repository.UpdateAsync(_mapper.Map<ScheduleEntity>(scheduleToUpdate));

			if (updatedSchedule == null)
			{
				Log.Error("Schedule with id = {@id} not found", scheduleToUpdate.Id);
			  throw new NotFoundException(ErrorMessages.SCHEDULE_NOT_FOUND);
      }

			Log.Information("Updated schedule: {@updatedSchedule}", updatedSchedule);

			return _mapper.Map<Schedule>(updatedSchedule);
		}

		public async Task DeleteScheduleAsync(Schedule scheduleToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<ScheduleEntity>(scheduleToDelete));
		}
	}
}
