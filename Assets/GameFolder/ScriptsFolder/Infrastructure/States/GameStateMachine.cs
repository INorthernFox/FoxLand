using System;
using System.Collections.Generic;
using GameFolder.ScriptsFolder.Infrastructure.Installers;

namespace GameFolder.ScriptsFolder.Infrastructure.States
{
	public class GameStateMachine : IGameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
		private IExitableState _activeState;
		private readonly GameStateFactory _gameStateFactory;

		public GameStateMachine(GameStateFactory gameStateFactory) =>
			_gameStateFactory = gameStateFactory;

		public void TryRegistrationState<TState>() where TState : IExitableState
		{
			if(!_states.ContainsKey(typeof(TState)))
				_states.Add(typeof(TState), _gameStateFactory.Create<TState>());
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState
		{
			TryRegistrationState<TState>();
			return _states[typeof(TState)] as TState;
		}
	}
}