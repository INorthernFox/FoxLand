using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder
{
	public static class Constants
	{
		public static class PrefabsPath
		{
			public const string GamePrefabs = "GamePrefabs";

			public static class Field
			{
				public const string PlayingFieldFolder = GamePrefabs + "/PlayingField";
				public const string PlayingFieldMainCellDataCasePath = GamePrefabs + "/PlayingField/ScriptableObjects/" + MainCellDataCaseName;
				public const string MainCellDataCaseName = "MainCellDataCase";
			}
		}

		public static class LevelSaves
		{
			public const string LevelsSaveDataCaseName = "LevelsSaveDataCase";
			public const string LevelsSaveDataCasePath = LevelFolderResourcesPath + "\\" + LevelsSaveDataCaseName;
			public static string LevelFolderPath => Application.dataPath + "\\GameFolder\\Resources\\LevelFolder";
			public const string LevelFolderResourcesPath = "LevelFolder";
			public static string LevelSavePath => LevelFolderPath + "\\LevelSaves";
			public static string LevelSaveResourcesPath => LevelFolderResourcesPath + "\\LevelSaves";
			public const string FileExtensionTXT = ".txt";
		}

		public static class SceneNames
		{
			public const string BootstrapScene = "BootstrapScene";
			public const string MenuScene = "MenuScene";
			public const string GameScene = "GameScene";
			public const string EditorScene = "EditorScene";
		}

		public static class Currency
		{
			public const string CurrencySettingsName = "CurrencySettings";
			public const string CurrencySettingsDataCasePath = "CurrencyFolder" + "\\" + CurrencySettingsName;
			public static readonly string SavePath = Application.persistentDataPath + "/savefile/currencysave.json";
		}
		
		public static class UI
		{
			public static class Session
			{
				public static string CanvasPrefabPath => "UIPrefabsFolder\\Session\\" + CanvasName;
				public static string CanvasName => "Game_Session_Canvas";
			}
		}
	}
}