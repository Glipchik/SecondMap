﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.SMS.API.Constants;
using SecondMap.Services.SMS.API.Dto;
using SecondMap.Services.SMS.API.ViewModels.AddModels;
using SecondMap.Services.SMS.API.ViewModels.UpdateModels;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;

namespace SecondMap.Services.SMS.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
	[ApiController]
	public class SchedulesController : ControllerBase
	{
		private readonly IScheduleService _scheduleService;
		private readonly IMapper _mapper;

		public SchedulesController(IScheduleService scheduleService, IMapper mapper)
		{
			_scheduleService = scheduleService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IEnumerable<ScheduleDto>>(await _scheduleService.GetAllAsync()));

		}

		[HttpGet(ApiEndpoints.ID)]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundSchedule = _mapper.Map<ScheduleDto>(await _scheduleService.GetByIdAsync(id));

			return Ok(foundSchedule);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] ScheduleAddViewModel scheduleToAdd)
		{
			var addedSchedule = _mapper.Map<ScheduleDto>(await _scheduleService.AddScheduleAsync(_mapper.Map<Schedule>(scheduleToAdd)));

			return Ok(addedSchedule);
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ScheduleUpdateViewModel scheduleToUpdate)
		{
			var mappedScheduleToUpdate = _mapper.Map<Schedule>(scheduleToUpdate);
			mappedScheduleToUpdate.Id = id;

			var updatedSchedule = _mapper.Map<ScheduleDto>(await _scheduleService.UpdateScheduleAsync(mappedScheduleToUpdate));

			return Ok(updatedSchedule);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _scheduleService.DeleteScheduleAsync(id);

			return NoContent();
		}

		[HttpGet(ApiEndpoints.STORE_ID_EQUALS + ApiEndpoints.ID)]
		public async Task<IActionResult> GetAllByStoreIdAsync(int id)
		{
			var foundSchedules = _mapper.Map<IEnumerable<ScheduleDto>>(await _scheduleService.GetAllByStoreIdAsync(id));

			return Ok(foundSchedules);
		}
	}
}
