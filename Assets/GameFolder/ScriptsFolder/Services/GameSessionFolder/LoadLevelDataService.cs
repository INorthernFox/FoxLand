using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.Core.MapFolder;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services.ResourcesFolder;
using Newtonsoft.Json;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class LoadLevelDataService : IService
	{
		private readonly IResourcesLoaderService _resourcesLoaderService;
		private LevelsSaveDataCase _levelsSaveDataCase;

		public LoadLevelDataService(IResourcesLoaderService resourcesLoaderService)
		{
			_resourcesLoaderService = resourcesLoaderService;
			Initialization();
		}

		private async void Initialization()
		{
			_levelsSaveDataCase = await  _resourcesLoaderService.LoadAsync<LevelsSaveDataCase>(Constants.LevelSaves.LevelsSaveDataCasePath);
		}

		public async UniTask<LevelSaveData> LoadData(int levelIndex)
		{
			LevelsDataDataSubCaseClose closeData = _levelsSaveDataCase.Levels[levelIndex];
			
			TextAsset textFile = await _resourcesLoaderService.LoadAsync<TextAsset>(closeData.Path);
			string json = textFile.text;

			LevelSaveData levelSaveData =	JsonConvert.DeserializeObject<LevelSaveData>(json);

			return levelSaveData;
		}
	}
}