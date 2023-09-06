namespace SecondMap.Shared.Messages;

public record UpdateUserCommand(string OldEmail, string NewEmail, int RoleId);
