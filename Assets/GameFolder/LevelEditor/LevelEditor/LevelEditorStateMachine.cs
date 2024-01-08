using System;
using System.Collections.Generic;
using GameFolder.ScriptsFolder.Infrastructure.States;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class LevelEditorStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public LevelEditorStateMachine(Dictionary<Type, IExitableState> states) =>
			_states = states;

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

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
	
	public class EditLevelState : ILevelEditorState
	{
		private readonly MainRootPanelLevelEditor _mainRootPanelLevelEditor;
		
		public EditLevelState(MainRootPanelLevelEditor mainRootPanelLevelEditor)
		{
			_mainRootPanelLevelEditor = mainRootPanelLevelEditor;

		}

		public void Enter()
		{
			_mainRootPanelLevelEditor.Lower.DeactivateAll();
			_mainRootPanelLevelEditor.Left.DeactivateAll();
			_mainRootPanelLevelEditor.Right.DeactivateAll();
			_mainRootPanelLevelEditor.Right.BaseLevelPanel.Activate();
			_mainRootPanelLevelEditor.Left.CellsPanel.Activate();
			
		}
		
		public void Exit()
		{
			throw new System.NotImplementedException();
		}

	}
	
	public class LoadLevelState : ILevelEditorState
	{
		private readonly MainRootPanelLevelEditor _mainRootPanelLevelEditor;
		
		public LoadLevelState(MainRootPanelLevelEditor mainRootPanelLevelEditor)
		{
			_mainRootPanelLevelEditor = mainRootPanelLevelEditor;

		}

		public void Enter()
		{
			_mainRootPanelLevelEditor.Lower.DeactivateAll();
			_mainRootPanelLevelEditor.Left.DeactivateAll();
			_mainRootPanelLevelEditor.Right.DeactivateAll();
			_mainRootPanelLevelEditor.Right.LoadLevelPanel.Activate();
		}
		
		public void Exit()
		{
			throw new System.NotImplementedException();
		}

	}
	
	public interface ILevelEditorState : IState
	{
		
	}
}