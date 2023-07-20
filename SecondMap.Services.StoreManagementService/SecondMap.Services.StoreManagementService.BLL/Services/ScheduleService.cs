﻿using AutoMapper;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;
using SecondMap.Services.StoreManagementService.DAL.Entities;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;

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

		public async Task<List<Schedule>> GetAllAsync()
		{
			return _mapper.Map<List<Schedule>>(await _repository.GetAllAsync());
		}

		public async Task<Schedule> GetByIdAsync(int id)
		{
			var foundSchedule = await _repository.GetByIdAsync(id);

			if (foundSchedule == null)
			{
				throw new NotFoundException("Schedule not found");
			}

			return _mapper.Map<Schedule>(foundSchedule);
		}

		public async Task AddScheduleAsync(Schedule scheduleToAdd)
		{
			await _repository.AddAsync(_mapper.Map<ScheduleEntity>(scheduleToAdd));
		}

		public async Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate)
		{
			var updatedSchedule = await _repository.UpdateAsync(_mapper.Map<ScheduleEntity>(scheduleToUpdate));

			if (updatedSchedule == null)
			{
				throw new NotFoundException("Schedule not found");
			}

			return _mapper.Map<Schedule>(updatedSchedule);
		}

		public async Task DeleteScheduleAsync(Schedule scheduleToDelete)
		{
			await _repository.DeleteAsync(_mapper.Map<ScheduleEntity>(scheduleToDelete));
		}
	}
}
