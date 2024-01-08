using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class InputService : IInputService, IDisposable
	{
		private readonly MainInputControls _mainInputControls;

		public event Action Started;
		public event Action Stopped;
		public event Action PositionChanged;

		public Vector2 Position => _mainInputControls.Touch.Position.ReadValue<Vector2>();
		public Vector2 Delta => _mainInputControls.Touch.Delta.ReadValue<Vector2>();

		private bool _isActive = false;

		public InputService()
		{
			_mainInputControls = new MainInputControls();
			_mainInputControls.Touch.Started.performed += _ => OnInputStarted();
			_mainInputControls.Touch.Stopped.performed += _ => OnInputStopped();
		}

		private void OnInputStarted() =>
			Started?.Invoke();

		private void OnInputStopped() =>
			Stopped?.Invoke();

		public void Enable()
		{
			if(_isActive)
				throw new Exception($"[{nameof(InputService)}] An attempt to enable an already active InputService");

			_isActive = true;
			_mainInputControls.Enable();
			InputReading();
		}

		public void Disable()
		{
			_mainInputControls.Disable();
			_isActive = false;
		}

		private async void InputReading()
		{
			while(_isActive)
			{
				if(_mainInputControls.Touch.Delta.ReadValue<Vector2>() != Vector2.zero)
					PositionChanged?.Invoke();

				await UniTask.Yield();
			}
		}
		
		public void Dispose()
		{
			Debug.Log("Dispose InputService");
			_mainInputControls?.Dispose();
		}
	}
}