using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.BLL.Interfaces
{
    public interface IRoleService
    {
        Task AddRoleAsync(Role roleToAdd);
        Task DeleteRoleAsync(Role roleToDelete);
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task<Role> UpdateRoleAsync(Role roleToUpdate);
    }
}