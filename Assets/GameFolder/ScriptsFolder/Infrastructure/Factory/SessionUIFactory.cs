using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using GameFolder.ScriptsFolder.Services.ResourcesFolder;
using GameFolder.UIFolder.ScriptsFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Infrastructure.Factory
{
	public class SessionUIFactory
	{
		private readonly IResourcesLoaderService _resourcesLoaderService;
		private readonly ILevelEventProcessorService _levelEventProcessorService;
		private readonly GameConstantsSettingsValue _gameConstantsSettingsValue;
		private readonly CurrencyServices _currencyServices;

		public SessionUIFactory(IResourcesLoaderService resourcesLoaderService, ILevelEventProcessorService levelEventProcessorService, GameConstantsSettingsValue gameConstantsSettingsValue, CurrencyServices currencyServices)
		{
			_resourcesLoaderService = resourcesLoaderService;
			_levelEventProcessorService = levelEventProcessorService;
			_gameConstantsSettingsValue = gameConstantsSettingsValue;
			_currencyServices = currencyServices;
		}

		public async UniTask<GameSessionCanvas> CreateCanvas(Transform root)
		{
			GameSessionCanvas prefab = await _resourcesLoaderService.LoadAsync<GameSessionCanvas>(Constants.UI.Session.CanvasPrefabPath);

			GameSessionCanvas canvas = Object.Instantiate(prefab, root);
			canvas.name = Constants.UI.Session.CanvasName;
			
			canvas.Initialization(_levelEventProcessorService, _gameConstantsSettingsValue, _currencyServices);

			return canvas;
		}

	}
}