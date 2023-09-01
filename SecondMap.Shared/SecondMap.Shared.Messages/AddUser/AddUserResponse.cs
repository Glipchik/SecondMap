namespace SecondMap.Shared.Messages;

public record AddUserResponse(AddUserCommand command, bool IsSuccessful, string? ErrorMessage);