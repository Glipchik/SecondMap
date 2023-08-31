using MassTransit;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Services.Auth.Domain.Entities;
using SecondMap.Shared.Messages;

namespace SecondMap.Services.Auth.Application.MessageConsumers
{
	public class DeleteUserByEmailConsumer : IConsumer<DeleteUserByEmail>
	{
		private readonly IUserService _userService;

		public DeleteUserByEmailConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<DeleteUserByEmail> context)
		{
			var updateUserMessage = context.Message;

			var foundUser = (await _userService.GetUserByEmailAsync(updateUserMessage.Email)).Result!;

			var deleteResult = await _userService.DeleteUserAsync(foundUser);

			// if update result is not success than should return some error message back to producer
		}
	}
}