using GameFolder.ScriptsFolder.Infrastructure.States;

namespace GameFolder.ScriptsFolder.Infrastructure
{
	public class Game
	{
		private readonly GameStateMachine _gameStateMachine;

		public GameStateMachine GameStateMachine => _gameStateMachine;
		
		public Game(GameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
			_gameStateMachine.Enter<BootstrapState>();
		}
}
}