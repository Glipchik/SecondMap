using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.API.Dto;
using SecondMap.Services.StoreManagementService.API.ViewModels;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route(ApiEndpoints.API_CONTROLLER_ROUTE)]
	[ApiController]
	public class SchedulesController : ControllerBase
	{
		private readonly IScheduleService _scheduleService;
		private readonly IMapper _mapper;
		private readonly IValidator<ScheduleViewModel> _validator;

		public SchedulesController(IScheduleService scheduleService, IMapper mapper, IValidator<ScheduleViewModel> validator)
		{
			_scheduleService = scheduleService;
			_mapper = mapper;
			_validator = validator;
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
		public async Task<IActionResult> AddAsync([FromBody] ScheduleViewModel scheduleToAdd)
		{
			var validationResult = await _validator.ValidateAsync(scheduleToAdd);

			if (!validationResult.IsValid)
			{
				throw new Exception(ErrorMessages.VALIDATION_FAILED);
			}

			await _scheduleService.AddScheduleAsync(_mapper.Map<Schedule>(scheduleToAdd));

			return Ok();
		}

		[HttpPut(ApiEndpoints.ID)]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] ScheduleViewModel scheduleToUpdate)
		{
			var validationResult = await _validator.ValidateAsync(scheduleToUpdate);

			if (!validationResult.IsValid)
			{
				throw new Exception(ErrorMessages.VALIDATION_FAILED);
			}
			var mappedScheduleToUpdate = _mapper.Map<Schedule>(scheduleToUpdate);
			mappedScheduleToUpdate.Id = id;

			var updatedSchedule = _mapper.Map<ScheduleDto>(await _scheduleService.UpdateScheduleAsync(mappedScheduleToUpdate));

			return Ok(updatedSchedule);
		}

		[HttpDelete(ApiEndpoints.ID)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundSchedule = await _scheduleService.GetByIdAsync(id);

			await _scheduleService.DeleteScheduleAsync(foundSchedule);

			return NoContent();
		}
	}
}
