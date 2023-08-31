using MassTransit;
using Microsoft.AspNetCore.Identity;
using SecondMap.Messages;
using SecondMap.Services.Auth.Application.Results;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Services.Auth.Domain.Entities;
using SecondMap.Shared.Messages;

namespace SecondMap.Services.Auth.Application.Services
{
	public class AuthorizationService : IAuthorizationService
	{
		private readonly SignInManager<AuthUser> _signInManager;
		private readonly IUserService _userService;
		private readonly IPublishEndpoint _publishEndpoint;

		public AuthorizationService(SignInManager<AuthUser> signInManager,
			IUserService userService,
			IPublishEndpoint publishEndpoint)
		{
			_signInManager = signInManager;
			_userService = userService;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<AuthResult<SignInResult>> LoginAsync(string email, string password)
		{
			var userServiceResult = await _userService.GetUserByEmailAsync(email);

			if (!userServiceResult.Success)
			{
				return new AuthResult<SignInResult>
				{
					Success = false,
					ErrorMessage = userServiceResult.ErrorMessage
				};
			}

			var signInResult = await _signInManager.PasswordSignInAsync(userServiceResult.Result!, password, false, false);

			return new AuthResult<SignInResult>
			{
				Success = true,
				Result = signInResult
			};
		}

		public async Task<AuthResult<SignInResult>> RegisterAsync(string username, string email, string password)
		{
			var userServiceResult = await _userService.CreateUserAsync(username, email, password);

			if (!userServiceResult.Success)
			{
				return new AuthResult<SignInResult>
				{
					Success = false,
					ErrorMessage = userServiceResult.ErrorMessage
				};
			}

			await _publishEndpoint.Publish(new AddUser(email, username));
			
			var signInResult = await _signInManager.PasswordSignInAsync(userServiceResult.Result!, password, false, false);

			return new AuthResult<SignInResult>
			{
				Success = true,
				Result = signInResult
			};
		}
	}
}
