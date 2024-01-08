
using GameFolder.ScriptsFolder.Core;
using GameFolder.ScriptsFolder.DataFolder;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
	public class LoadMenuState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public LoadMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;

		}

		public async void Enter() =>
			await _sceneLoader.Load(Constants.SceneNames.MenuScene, onLoaded: EnterLoadLevel);

		public void Exit()
		{
		}

    
		private void EnterLoadLevel() =>
			_stateMachine.Enter<MenuState>();
	}
}