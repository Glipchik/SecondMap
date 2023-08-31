using Automatonymous;

namespace SecondMap.UserSaga.UserStateMachine
{
	public class AddUserStateMachine : AutomatonymousStateMachine<AddUserStates>
	{
		public State AddedInIdentityServer { get; set; }
		public State AddedInStoreManagement {get; set; }
		public State RejectedInIdentityServer { get; set; }
		public State RejectedInStoreManagement { get; set; }

		public Event AddInIdentityServer { get; set; }
		public Event AddInStoreManagement { get; set; }

		public AddUserStateMachine()
		{
			InstanceState(x => x.CurrentState);
		}
	}
}
