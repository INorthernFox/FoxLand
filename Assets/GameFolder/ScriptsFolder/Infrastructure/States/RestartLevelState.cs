using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
	public class RestartLevelState : IPayloadedState<GameSession>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly LocalDependencyService _localDependency;

		public RestartLevelState(GameStateMachine stateMachine, LocalDependencyService localDependency)
		{
			_stateMachine = stateMachine;
			_localDependency = localDependency;
		}

		public async void Enter(GameSession gameSessionData)
		{
			await gameSessionData.Restart();
			_stateMachine.Enter<GameLoopState>();
		}

		public void Exit()
		{

		}
	}
}