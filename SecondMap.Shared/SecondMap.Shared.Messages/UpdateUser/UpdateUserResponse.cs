namespace SecondMap.Shared.Messages;

public record UpdateUserResponse(UpdateUserCommand Command, bool IsSuccessful, string? ErrorMessage);