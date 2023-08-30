using MassTransit;
using SecondMap.Messages;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using Serilog;

namespace SecondMap.Services.SMS.Messages.Consumers
{
	public class AddUserConsumer : IConsumer<AddUser>
	{
		private readonly IUserService _userService;

		public AddUserConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<AddUser> context)
		{
			Log.Information("Received message: Add user\n@{userToAdd}", context.Message);

			var userToAdd = new User
			{
				Email = context.Message.Email,
				Username = context.Message.Username,
			};

			await _userService.AddUserAsync(userToAdd);
		}
	}
}
