using System;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class GamePanel : MonoBehaviour
	{
		[SerializeField] private RestartButton _restartButton;
		[SerializeField] private StartMoveButton _startMoveButton;
		
		private ILevelEventProcessorService _levelEventProcessorService;

		public event Action OnStartMoveClicked;
		public event Action OnRestartClicked;
		
		public void Initialization(ILevelEventProcessorService levelEventProcessorService)
		{
			_levelEventProcessorService = levelEventProcessorService;
			_levelEventProcessorService.OnLevelFailed += OnLevelFailed;
			_levelEventProcessorService.OnLevelCompleted += OnLevelCompleted;
			_levelEventProcessorService.OnLevelRestarted += OnLevelRestarted;
			_levelEventProcessorService.OnLevelStarted += OnLevelStarted;
			
			_restartButton.OnClicked += RestartButtonClicked;
			_startMoveButton.OnClicked += StartMoveButtonClicked;
			
			SetDefaultState();
		}
		
		private void StartMoveButtonClicked() =>
			OnStartMoveClicked?.Invoke();

		private void RestartButtonClicked() =>
			OnRestartClicked?.Invoke();

		private void SetDefaultState()
		{
			_restartButton.Deactivate();
			_startMoveButton.Activate();
		}

		private void OnLevelStarted(){}
		
		private void OnLevelRestarted() =>
			SetDefaultState();

		private void OnLevelCompleted()
		{
			
		}

		private void OnLevelFailed()
		{
			_restartButton.Activate();
			_startMoveButton.Deactivate();
		}

		private void OnDestroy()
		{
			_levelEventProcessorService.OnLevelFailed -= OnLevelFailed;
			_levelEventProcessorService.OnLevelCompleted -= OnLevelCompleted;
			_levelEventProcessorService.OnLevelRestarted -= OnLevelRestarted;
			_levelEventProcessorService.OnLevelStarted -= OnLevelStarted;
		}
	}
}