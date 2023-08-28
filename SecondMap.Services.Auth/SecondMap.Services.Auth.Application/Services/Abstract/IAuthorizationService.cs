using Microsoft.AspNetCore.Identity;
using SecondMap.Services.Auth.Application.Results;

namespace SecondMap.Services.Auth.Application.Services.Abstract
{
	public interface IAuthorizationService
	{
		Task<AuthResult<SignInResult>> LoginAsync(string email, string password);

		Task<AuthResult<SignInResult>> RegisterAsync(string username, string email, string password);
	}
}
