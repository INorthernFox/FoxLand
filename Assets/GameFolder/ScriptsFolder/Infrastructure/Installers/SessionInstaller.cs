using GameFolder.ScriptsFolder.Infrastructure.Factory;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using Zenject;

namespace GameFolder.ScriptsFolder.Infrastructure.Installers
{
	public class SessionInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<SessionCreationService>().FromNew().AsSingle();
			Container.Bind<SessionClearingService>().FromNew().AsSingle();
			Container.Bind<SessionUIFactory>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<InputRaycastService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<CellMoveService>().FromNew().AsSingle().NonLazy();
			Container.Bind<PlayingFieldCreatorService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<CellCacheService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<CameraService>().FromNew().AsSingle();
			
			Container.Resolve<LocalDependencyService>().SetLocalContainer(Container);
		}

		private void OnDisable() =>
			Container.Resolve<LocalDependencyService>().Clear();
	}


}