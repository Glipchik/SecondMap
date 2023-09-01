using MassTransit;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Shared.Messages;

namespace SecondMap.Services.Auth.Application.MessageConsumers
{
	public class UpdateUserConsumer : IConsumer<UpdateUserCommand>
	{
		private readonly IUserService _userService;

		public UpdateUserConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<UpdateUserCommand> context)
		{
			var updateUserMessage = context.Message;

			var foundUserResult = await _userService.GetUserByEmailAsync(updateUserMessage.OldEmail);

			if (!foundUserResult.Success)
			{
				await context.RespondAsync(new UpdateUserResponse(context.Message, false, foundUserResult.ErrorMessage));
			}

			var updateResult = await _userService.UpdateUserAsync(foundUserResult.Result!, updateUserMessage.NewEmail, updateUserMessage.RoleId);

			if (!updateResult.Success)
			{
				await context.RespondAsync(new UpdateUserResponse(context.Message, false, foundUserResult.ErrorMessage));
			}

			await context.RespondAsync(new UpdateUserResponse(context.Message, true, String.Empty));
		}
	}
}
