using Microsoft.AspNetCore.Mvc;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.API.Controllers
{
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _roleService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var foundRole = await _roleService.GetByIdAsync(id);

			return Ok(foundRole);
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] Role roleToAdd)
		{
			await _roleService.AddRoleAsync(roleToAdd);

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] Role roleToUpdate)
		{
			if (id != roleToUpdate.Id)
			{
				return BadRequest();
			}

			var updatedRole = await _roleService.UpdateRoleAsync(roleToUpdate);

			return Ok(updatedRole);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var foundRole = await _roleService.GetByIdAsync(id);

			await _roleService.DeleteRoleAsync(foundRole);

			return NoContent();
		}
	}
}