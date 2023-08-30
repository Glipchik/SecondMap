namespace SecondMap.Shared.Messages;

public interface IUpdateUserOption
{
	public Task ChangeUser;
}

public class UpdateUserEmail : IUpdateUserOption
{

}
