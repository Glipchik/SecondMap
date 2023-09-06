using MassTransit;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Models;
using SecondMap.Shared.Messages;
using Serilog;

namespace SecondMap.Services.SMS.BLL.MessageConsumers
{
	public class AddUserConsumer : IConsumer<AddUserCommand>
	{
		private readonly IUserService _userService;

		public AddUserConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<AddUserCommand> context)
		{
			Log.Information("Received message: Add user\n@{userToAdd}", context.Message);

			var userToAdd = new User
			{
				Email = context.Message.Email,
				Username = context.Message.Username
			};

			try
			{
				await _userService.AddUserAsync(userToAdd);
				await context.RespondAsync(new AddUserResponse(context.Message, true, string.Empty));
			}

			catch (Exception exception)
			{
				Log.Error("Adding user {@userToAdd} in SMS failed\nSent message to Auth to rollback user", userToAdd);
				await context.RespondAsync(new AddUserResponse(context.Message, false, exception.Message));
			}
		}
	}
}
