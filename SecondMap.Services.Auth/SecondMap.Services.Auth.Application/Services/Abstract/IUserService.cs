using SecondMap.Services.Auth.Application.Results;
using SecondMap.Services.Auth.Domain.Entities;

namespace SecondMap.Services.Auth.Application.Services.Abstract
{
	public interface IUserService
	{
		Task<AuthResult<AuthUser>> GetUserAsync(string email);

		Task<AuthResult<AuthUser>> CreateUserAsync(string userName, string email, string password);
	}
}
