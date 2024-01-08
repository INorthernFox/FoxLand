using GameFolder.ScriptsFolder.Core;
using GameFolder.ScriptsFolder.DataFolder;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;

    }

    public async void Enter() =>
      await _sceneLoader.Load(Constants.SceneNames.BootstrapScene, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    
    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadMenuState>();
  }

}