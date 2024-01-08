using System;
using GameFolder.ScriptsFolder.DataFolder;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class CameraService : IService, IDisposable
	{
		private readonly CameraSettings _cameraSettings;
		private Camera _camera;
		
		public Camera Camera => _camera;
		
		public CameraService(GameConstantsSettingsValue constantsValue)
		{
			_cameraSettings = constantsValue.CameraSettings;
		}
		
		public void CreateCamera(Transform root, Vector3 position, Vector3 rotation)
		{
			_camera = Object.Instantiate(_cameraSettings.Prefab, root);

			Transform transform = _camera.transform;
			transform.eulerAngles = rotation;
			transform.position = position;
			
			_camera.name = $"[Camera_Main]";
		}

		public void CreateCamera(Transform root) =>
			CreateCamera(root, _cameraSettings.DefaultPosition, _cameraSettings.DefaultRotation);

		public void Dispose()
		{
			Object.Destroy(_camera);
		}
	}
}