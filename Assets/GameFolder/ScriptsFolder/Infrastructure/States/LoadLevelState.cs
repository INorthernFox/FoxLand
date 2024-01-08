using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<GameSessionConfiguration>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LocalDependencyService _localDependency;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LocalDependencyService localDependency)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_localDependency = localDependency;
		}

		public async void Enter(GameSessionConfiguration gameSessionData) =>
			await _sceneLoader.Load(Constants.SceneNames.GameScene, () => EnterLoadLevel(gameSessionData));

		public void Exit()
		{

		}

		private async void EnterLoadLevel(GameSessionConfiguration data)
		{
			SessionCreationService sessionCreationService = _localDependency.GetDependency<SessionCreationService>();
			GameSession gameSession = await sessionCreationService.Create(data);
			_localDependency.GetDependency<IInputService>().Enable();
			_stateMachine.Enter<GameLoopState>();
		}
	}

}