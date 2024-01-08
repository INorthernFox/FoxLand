using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Infrastructure.States;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using GameFolder.ScriptsFolder.Services.ResourcesFolder;
using UnityEngine;
using Zenject;

namespace GameFolder.ScriptsFolder.Infrastructure.Installers
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private GameConstantsSettingsValue GameConstantsSettingsValue;

		public override void InstallBindings()
		{
			RegistrationSceneLoader();
			RegistrationGameStateMachine();
			RegistrationGame();
			RegistrationInputService();
			RegistrationGameConstantsValue();
			RegistrationResourcesLoader();
			
			Container.Bind<LocalDependencyService>().FromNew().AsSingle();
			Container.Bind<LoadLevelDataService>().FromNew().AsSingle().NonLazy();
			Container.BindInterfacesTo<LevelEventProcessorService>().FromNew().AsSingle();
			Container.Bind<CurrencyServices>().FromNew().AsSingle().NonLazy();
		}

		private void RegistrationSceneLoader() =>
			Container.Bind<SceneLoader>().FromNew().AsSingle();
		
		private void RegistrationGameStateMachine()
		{
			Container.Bind<GameStateFactory>().AsSingle();
			Container.Bind<GameStateMachine>().AsSingle();
		}
		
		private void RegistrationGame() =>
			Container.Bind<Game>().AsSingle().NonLazy();

		private void RegistrationInputService() =>
			Container.BindInterfacesTo<InputService>().FromNew().AsSingle().NonLazy();
		
		private void RegistrationGameConstantsValue() =>
			Container.BindInstance<GameConstantsSettingsValue>(GameConstantsSettingsValue).AsSingle();


		private void RegistrationResourcesLoader() =>
			Container.BindInterfacesTo<ResourcesLoaderService>().FromNew().AsSingle();
	}
}