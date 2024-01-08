using GameFolder.ScriptsFolder.Core.MapFolder;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Infrastructure.States;
using GameFolder.ScriptsFolder.Services.DataServicesFolder;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameFolder.ScriptsFolder.MenuSceneFolder
{
	public class MenuSceneRoot : MonoBehaviour
	{
		private GameStateMachine _gameStateMachine;

		[SerializeField] private Button _playButton;
		[SerializeField] private Button _editorButton;

		[Inject]
		private void Construct(GameStateMachine gameStateMachine) =>
			_gameStateMachine = gameStateMachine;

		private void Start() =>
			Subscribe();

		private void Subscribe()
		{
			_playButton.onClick.AddListener(OnPlayButtonClicked);
			_editorButton.onClick.AddListener(OnEditorButtonClicked);
		}

		private void OnEditorButtonClicked()
		{
			Unsubscribe();
			_gameStateMachine.Enter<LoadEditorState>();
		}

		private void OnPlayButtonClicked()
		{
			Unsubscribe();
			_gameStateMachine.Enter<LoadLevelState, GameSessionConfiguration>(CreateTestData());
		}

		private void Unsubscribe()
		{
			_playButton.onClick.RemoveListener(OnPlayButtonClicked);
			_editorButton.onClick.RemoveListener(OnEditorButtonClicked);
		}

		[NaughtyAttributes.Button]
		public void Test()
		{

			LevelSaveData levelSaveData = new LevelSaveData();

			levelSaveData.ID = "Level_1_Tutorial";
			levelSaveData.FieldData = CreateFieldData();
			string json = JsonConvert.SerializeObject(levelSaveData);
			DataSaver.Save(levelSaveData, Constants.LevelSaves.LevelSavePath +"\\"+ levelSaveData.ID + Constants.LevelSaves.FileExtensionTXT);
			Debug.Log(json);
		}
	
		private static GameSessionConfiguration CreateTestData()
		{
			GameSessionConfiguration gameSessionConfiguration = new GameSessionConfiguration();
			gameSessionConfiguration.LevelNumber = 0;
			gameSessionConfiguration.LevelPlayerNumber = 1;
			return gameSessionConfiguration;
		}
		
		private static FieldData CreateFieldData()
		{
			FieldData fieldData = new FieldData();
			CellData[,] cellsData = new CellData[5, 5];
			
			for( int y = 0; y < cellsData.GetLength(1); y++ )
			{
				for( int x = 0; x < cellsData.GetLength(0); x++ )
				{
					CellData cellData = new()
					{
						Position = new Vector2Int(x, y),
						Rotation = 0,
						ID = Random.Range(0, 2) > 0 ? "TestHex_NotActive" : "TestHex_Active",
					};

					cellsData[x, y] = cellData;
				}
			}
			
			fieldData.CellsData = cellsData;
			return fieldData;
		}
	}

	public struct Vector2Int
	{
		public int x;
		public int y;

		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2Int(UnityEngine.Vector2Int vector2Int)
		{
			this.x = vector2Int.x;
			this.y = vector2Int.y;
		}

		public UnityEngine.Vector2Int ToUnity() =>
			new UnityEngine.Vector2Int(x, y);
	}
}