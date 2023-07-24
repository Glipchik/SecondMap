namespace SecondMap.Services.StoreManagementService.BLL.Exceptions
{
	public class ValidationFailException : Exception
	{
		public ValidationFailException() { }

		public ValidationFailException(string? message) : base(message)
		{
		}

		public ValidationFailException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
