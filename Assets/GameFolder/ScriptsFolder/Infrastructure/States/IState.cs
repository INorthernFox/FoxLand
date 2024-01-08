namespace GameFolder.ScriptsFolder.Infrastructure.States
{
  public interface IState: IExitableState
  {
    void Enter();
  }

  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload gameSessionData);
  }
  
  public interface IExitableState
  {
    void Exit();
  }
}