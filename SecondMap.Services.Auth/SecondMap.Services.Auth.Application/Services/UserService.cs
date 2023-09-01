using Microsoft.AspNetCore.Identity;
using SecondMap.Services.Auth.Application.Constants;
using SecondMap.Services.Auth.Application.Results;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Services.Auth.Domain.Entities;
using SecondMap.Services.Auth.Domain.Enums;
using System.Security.Claims;

namespace SecondMap.Services.Auth.Application.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AuthUser> _userManager;

		public UserService(UserManager<AuthUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<AuthResult<AuthUser>> GetUserByEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return new AuthResult<AuthUser>
				{
					Success = false,
					ErrorMessage = ErrorMessages.USER_NOT_REGISTERED
				};
			}

			return new AuthResult<AuthUser>
			{
				Success = true,
				Result = user
			};
		}

		public async Task<AuthResult<AuthUser>> CreateUserAsync(string userName, string email, string password)
		{
			var user = new AuthUser
			{
				UserName = userName,
				Email = email,
			};

			var creationResult = await _userManager.CreateAsync(user, password);

			if (!creationResult.Succeeded)
			{
				return new AuthResult<AuthUser>
				{
					Success = false,
					ErrorMessage = creationResult.Errors.First().Description
				};
			}

			await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

			var role = (await _userManager.GetRolesAsync(user)).First();

			await _userManager.AddClaimsAsync(user, new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, role)
			});

			return new AuthResult<AuthUser>
			{
				Success = true,
				Result = user
			};
		}

		public async Task<AuthResult<AuthUser>> UpdateUserAsync(AuthUser userToUpdate, string email, int roleId)
		{
			var emailResult = await _userManager.SetEmailAsync(userToUpdate, email);

			if (!emailResult.Succeeded)
			{
				return new AuthResult<AuthUser>
				{
					Success = false,
					ErrorMessage = emailResult.Errors.First().Description
				};
			}

			if (!await _userManager.IsInRoleAsync(userToUpdate, ((Roles)roleId).ToString()))
			{
				var roleResult = await _userManager.AddToRoleAsync(userToUpdate, ((Roles)roleId).ToString());

				if (!roleResult.Succeeded)
				{
					return new AuthResult<AuthUser>
					{
						Success = false,
						ErrorMessage = roleResult.Errors.First().Description
					};
				}
			}

			return new AuthResult<AuthUser>
			{
				Success = true,
				Result = await _userManager.FindByEmailAsync(email)
			};
		}

		public async Task<AuthResult<AuthUser>> DeleteUserAsync(AuthUser userToDelete)
		{
			var deleteResult = await _userManager.DeleteAsync(userToDelete);

			if (!deleteResult.Succeeded)
			{
				return new AuthResult<AuthUser>
				{
					Success = false,
					ErrorMessage = deleteResult.Errors.First().Description
				};
			}

			return new AuthResult<AuthUser>
			{
				Success = true
			};
		}
	}
}
