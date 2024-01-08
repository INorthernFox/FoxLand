using GameFolder.ScriptsFolder.DataFolder;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
	public class LoadEditorState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public LoadEditorState(GameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public async void Enter() =>
			await _sceneLoader.Load(Constants.SceneNames.EditorScene, onLoaded: EnterLoadLevel);

		public void Exit()
		{
		}

    
		private void EnterLoadLevel() =>
			_stateMachine.Enter<EditorLoopState>();
	}
}