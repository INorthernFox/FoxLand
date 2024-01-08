using System;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class InputRaycastService : IService, IDisposable
	{
		private readonly CameraService _cameraService;
		private readonly IInputService _inputService;

		public CellObject Captured { get; private set; }
		public CellObject AdditionalCaptured { get; private set; }
		public Vector3 Position { get; private set; }
		public bool HaveCapturedObject => Captured != null;
		
		public Camera Camera => _cameraService.Camera;

		public Action PositionChanged;

		public Action ObjectCaptured;
		public Action ObjectReleased;

		public Action AdditionalObjectCaptured;
		public Action AdditionalObjectReleased;

		public InputRaycastService(IInputService inputService, CameraService cameraService)
		{
			_inputService = inputService;
			_cameraService = cameraService;
			Subscribe();
		}

		private void Subscribe()
		{
			_inputService.Started += OnInputStarted;
			_inputService.Stopped += OnInputStopped;
			_inputService.PositionChanged += OnInputPositionChanged;
		}

		private void OnInputStarted()
		{
			if(!TryHit(out CellObject target))
				return;

			Captured = target;
			ObjectCaptured?.Invoke();
		}

		private void OnInputStopped()
		{
			if(!HaveCapturedObject)
				return;

			TryReleaseAdditionalCaptured();
			
			ObjectReleased?.Invoke();
			Captured = null;
		}

		private void OnInputPositionChanged()
		{
			if(!HaveCapturedObject)
				return;
			ObjectLogic();
			AdditionalObjectLogic();
		}

		private void ObjectLogic()
		{
			Vector3 position = _inputService.Position;
			position += Vector3.forward * Camera.nearClipPlane;

			Position = Camera.ScreenToWorldPoint(position);
			PositionChanged?.Invoke();
		}

		private void AdditionalObjectLogic()
		{
			if(!TryHit(out CellObject cellObject))
			{
				TryReleaseAdditionalCaptured();
				return;
			}

			if(cellObject == Captured)
				return;

			if(cellObject == AdditionalCaptured)
				return;

			TryReleaseAdditionalCaptured();

			AdditionalCaptured = cellObject;
			AdditionalObjectCaptured?.Invoke();
		}
		
		private void TryReleaseAdditionalCaptured()
		{
			if(AdditionalCaptured == null)
				return;
			AdditionalObjectReleased?.Invoke();
			AdditionalCaptured = null;
		}

		private bool TryHit(out CellObject cellObject)
		{
			cellObject = null;
			Ray ray = Camera.ScreenPointToRay(_inputService.Position);

			if(!Physics.Raycast(ray, out RaycastHit hit) || !hit.transform.TryGetComponent(out CellPhysicalModel physicalModel) || !physicalModel.CellObject.CellData.Interactive)
				return false;

			cellObject = physicalModel.CellObject;
			return true;
		}

		public void Dispose() =>
			Unsubscribe();

		private void Unsubscribe()
		{
			_inputService.Started -= OnInputStarted;
			_inputService.Stopped -= OnInputStopped;
			_inputService.PositionChanged -= OnInputPositionChanged;
		}
	}
}