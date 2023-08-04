using AutoMapper;
using SecondMap.Services.SMS.BLL.Constants;
using SecondMap.Services.SMS.BLL.Exceptions;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Services.SMS.DAL.Entities;
using SecondMap.Services.SMS.DAL.Interfaces;
using Serilog;

namespace SecondMap.Services.SMS.BLL.Services
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
			if (!await _repository.ExistsWithId(scheduleToUpdate.Id))
			{
				Log.Error("Schedule with id = {@id} not found", scheduleToUpdate.Id);
				throw new NotFoundException(ErrorMessages.SCHEDULE_NOT_FOUND);
			}

			var updatedSchedule = await _repository.UpdateAsync(_mapper.Map<ScheduleEntity>(scheduleToUpdate));

			Log.Information("Updated schedule: {@updatedSchedule}", updatedSchedule);

			return _mapper.Map<Schedule>(updatedSchedule);
		}

		public async Task DeleteScheduleAsync(int scheduleToDeleteId)
		{
			var entityToDelete = await _repository.GetByIdAsync(scheduleToDeleteId);
			if (entityToDelete == null)
			{
				Log.Error("Schedule with id = {@id} not found", scheduleToDeleteId);
				throw new NotFoundException(ErrorMessages.SCHEDULE_NOT_FOUND);
			}

			await _repository.DeleteAsync(entityToDelete);
		}

		public async Task<IEnumerable<Schedule>> GetAllByStoreIdAsync(int storeId)
		{
			var foundSchedules = await _repository.GetAllByPredicateAsync(sch => sch.StoreId == storeId);

			if (!foundSchedules.Any())
			{
				Log.Error("Schedules for store with id = {@id} not found", storeId);
				throw new NotFoundException(ErrorMessages.SCHEDULES_FOR_STORE_NOT_FOUND);
			}

			return _mapper.Map<IEnumerable<Schedule>>(foundSchedules);
		}
	}
}
