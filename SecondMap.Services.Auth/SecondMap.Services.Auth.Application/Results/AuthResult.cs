namespace SecondMap.Services.Auth.Application.Results
{
	public class AuthResult<TResult>
	{
		public TResult? Result { get; set; }

		public bool Success { get; set; }

		public string? ErrorMessage { get; set; }
	}
}
