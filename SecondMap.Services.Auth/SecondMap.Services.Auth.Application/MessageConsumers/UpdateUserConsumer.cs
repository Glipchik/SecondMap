using MassTransit;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Shared.Messages;

namespace SecondMap.Services.Auth.Application.MessageConsumers
{
	public class UpdateUserConsumer : IConsumer<UpdateUser>
	{
		private readonly IUserService _userService;

		public UpdateUserConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<UpdateUser> context)
		{
			var updateUserMessage = context.Message;

			var foundUserResult = await _userService.GetUserByEmailAsync(updateUserMessage.OldEmail);

			if (!foundUserResult.Success) { }

			var updateResult = await _userService.UpdateUserAsync(foundUserResult.Result!, updateUserMessage.NewEmail, updateUserMessage.RoleId);

			// if update result is not success than should return some error message back to producer
		}
	}
}
