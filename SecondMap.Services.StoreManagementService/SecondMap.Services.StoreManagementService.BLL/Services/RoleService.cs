using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Services
{
    public class RoleService : IRoleService
	{
		private readonly IRoleRepository _repository;

		public RoleService(IRoleRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<Role>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Role> GetByIdAsync(int id)
		{
			var foundRole = await _repository.GetByIdAsync(id);

			if (foundRole == null)
			{
				throw new Exception("Role not found");
			}

			return foundRole;
		}

		public async Task AddRoleAsync(Role roleToAdd)
		{
			await _repository.AddAsync(roleToAdd);
		}

		public async Task<Role> UpdateRoleAsync(Role roleToUpdate)
		{
			var updatedRole = await _repository.UpdateAsync(roleToUpdate);

			if (updatedRole == null)
			{
				throw new Exception("Role not found");
			}

			return updatedRole;
		}

		public async Task DeleteRoleAsync(Role roleToDelete)
		{
			await _repository.DeleteAsync(roleToDelete);
		}
	}
}