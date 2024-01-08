using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.Core.MapFolder;
using GameFolder.ScriptsFolder.Infrastructure.Factory;
using GameFolder.ScriptsFolder.Infrastructure.States;
using GameFolder.UIFolder.ScriptsFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class SessionCreationService : IService
	{
		private readonly PlayingFieldCreatorService _playingFieldCreatorService;
		private readonly CameraService _cameraService;
		private readonly LoadLevelDataService _levelDataService;
		private readonly SessionUIFactory _sessionUIFactory;

		public SessionCreationService(PlayingFieldCreatorService playingFieldCreatorService, CameraService cameraService, LoadLevelDataService levelDataService, SessionUIFactory sessionUIFactory)
		{
			_playingFieldCreatorService = playingFieldCreatorService;
			_cameraService = cameraService;
			_levelDataService = levelDataService;
			_sessionUIFactory = sessionUIFactory;
		}

		public async UniTask<GameSession> Create(GameSessionConfiguration configuration)
		{
			Transform rootTransform = CreateRootTransform();

			LevelSaveData levelSaveData = await _levelDataService.LoadData(configuration.LevelNumber);
			FieldData fieldData = levelSaveData.FieldData;

			CreateCamera(rootTransform);
			PlayingField playingField = await _playingFieldCreatorService.Create(fieldData, rootTransform);
			GameSessionCanvas canvas = await _sessionUIFactory.CreateCanvas(rootTransform);
			GameSessionData gameSessionData = new GameSessionData(playingField, levelSaveData.ID, configuration);

			GameSession gameSession = new GameSession(gameSessionData, _cameraService, rootTransform);
			return gameSession;
		}

		private void CreateCamera(Transform rootTransform) =>
			_cameraService.CreateCamera(rootTransform);

		private static Transform CreateRootTransform()
		{
			GameObject gameSessionRoot = new("GameSessionRoot");
			Transform rootTransform = gameSessionRoot.transform;
			return rootTransform;
		}
	}

	public class GameSessionData
	{
		public PlayingField PlayingField { get; private set; }
		public int LevelNumber { get; private set; }
		public int LevelPlayerNumber { get; private set; }

		public GameSessionData(PlayingField playingField, string levelID, GameSessionConfiguration configuration)
		{
			PlayingField = playingField;
			LevelNumber = configuration.LevelNumber;
			LevelPlayerNumber = configuration.LevelPlayerNumber;
		}
	}

	public class GameSession
	{
		private readonly GameSessionData _gameSessionData;

		private CameraService _camera;
		private Transform _rootTransform;

		public GameSession(GameSessionData gameSessionData, CameraService cameraService, Transform root)
		{
			_camera = cameraService;
			_rootTransform = root;
			_gameSessionData = gameSessionData;
		}

		public async UniTask Restart()
		{

		}
	}

}