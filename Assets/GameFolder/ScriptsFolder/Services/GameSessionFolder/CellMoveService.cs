using System;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using GameFolder.ScriptsFolder.DataFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class CellMoveService : IService, IDisposable
	{
		private readonly InputRaycastService _raycast;
		private readonly GameConstantsSettingsValue _gameConstantsSettingsValue;
		private float YPosition => _gameConstantsSettingsValue.FieldSettings.YCellPosition;

		private Vector3 _startPosition;
		
		public CellMoveService(InputRaycastService raycast,GameConstantsSettingsValue gameConstantsSettingsValue)
		{
			_raycast = raycast;
			_gameConstantsSettingsValue = gameConstantsSettingsValue;

			Subscribe();
		}

		private void Subscribe()
		{
			_raycast.ObjectCaptured += OnObjectCaptured;
			_raycast.ObjectReleased += OnObjectReleased;

			_raycast.AdditionalObjectCaptured += OnAdditionalObjectCaptured;
			_raycast.AdditionalObjectReleased += OnAdditionalObjectReleased;

			_raycast.PositionChanged += OnPositionChanged;
		}

		private void OnPositionChanged()
		{
			Vector3 position = _raycast.Position;
			position.y = YPosition;
			
			_raycast.Captured.transform.position = position;
		}

		private void OnObjectCaptured()
		{
			_startPosition = _raycast.Captured.transform.position;
			_raycast.Captured.CellPhysicalModel.Disable();
		}

		private void OnObjectReleased()
		{
			_raycast.Captured.CellPhysicalModel.Enable();

			if(_raycast.AdditionalCaptured != null)
			{
				Transform targetTransform = _raycast.AdditionalCaptured.transform;
				Vector3 secondPosition = targetTransform.position;
				
				targetTransform.position = _startPosition;
				_raycast.Captured.transform.position = secondPosition;
			}
			else
			{
				_raycast.Captured.transform.position = _startPosition;
			}
		}

		private void OnAdditionalObjectCaptured()
		{
			if(!_raycast.AdditionalCaptured.TryGetProjection(out ProjectionCellObject projection))
				return;
			
			projection.transform.position = _startPosition;
			projection.Enable();
		}

		private void OnAdditionalObjectReleased()
		{
			if(!_raycast.AdditionalCaptured.TryGetProjection(out ProjectionCellObject projection))
				return;
			
			projection.Disable();
			projection.transform.localPosition = Vector3.zero;
		}

		public void Dispose()
		{
			Unsubscribe();
		}
		
		private void Unsubscribe()
		{
			_raycast.ObjectCaptured -= OnObjectCaptured;
			_raycast.ObjectReleased -= OnObjectReleased;

			_raycast.AdditionalObjectCaptured -= OnAdditionalObjectCaptured;
			_raycast.AdditionalObjectReleased -= OnAdditionalObjectReleased;

			_raycast.PositionChanged -= OnPositionChanged;
		}
	}
}