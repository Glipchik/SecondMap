using SecondMap.Services.Auth.Application.Results;
using SecondMap.Services.Auth.Domain.Entities;

namespace SecondMap.Services.Auth.Application.Services.Abstract
{
	public interface IUserService
	{
		Task<AuthResult<AuthUser>> GetUserByEmailAsync(string email);
		Task<AuthResult<AuthUser>> CreateUserAsync(string username, string email, string password);
		Task<AuthResult<AuthUser>> UpdateUserAsync(AuthUser userToUpdate, string email, int roleId);
		Task<AuthResult<AuthUser>> DeleteUserAsync(AuthUser userToDelete);
	}
}
