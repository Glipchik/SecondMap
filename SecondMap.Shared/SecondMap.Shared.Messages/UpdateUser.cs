namespace SecondMap.Shared.Messages;

public record UpdateUser(string OldEmail, string NewEmail, int RoleId);

