using Automatonymous;

namespace SecondMap.UserSaga.UserStateMachine
{
	public class AddUserStates
	{
		public State CurrentState { get; set; }

		public List<Event> EventHistory { get; set; }
	}
}