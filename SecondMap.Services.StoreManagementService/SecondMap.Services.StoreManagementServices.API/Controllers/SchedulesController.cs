using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulesController : ControllerBase
	{
		private readonly IScheduleService _scheduleService;

		public SchedulesController(IScheduleService scheduleService)
		{
			_scheduleService = scheduleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _scheduleService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundSchedule = await _scheduleService.GetByIdAsync(id);

			return Ok(foundSchedule);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Schedule scheduleToAdd)
		{
			await _scheduleService.AddScheduleAsync(scheduleToAdd);

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] Schedule scheduleToUpdate)
		{
			if (id != scheduleToUpdate.Id)
			{
				return BadRequest();
			}

			var updatedSchedule = await _scheduleService.UpdateScheduleAsync(scheduleToUpdate);

			return Ok(updatedSchedule);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundSchedule = await _scheduleService.GetByIdAsync(id);

			await _scheduleService.DeleteScheduleAsync(foundSchedule);

			return NoContent();
		}
	}
}