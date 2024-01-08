using GameFolder.LevelEditor.LevelEditor;
using GameFolder.ScriptsFolder.LevelEditor;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using UnityEngine;
using Zenject;

namespace GameFolder.ScriptsFolder.Infrastructure.Installers
{
	public class LevelEditorInstaller : MonoInstaller
	{
		[SerializeField] private MainRootPanelLevelEditor _mainRootPanelLevelEditor;
		
		public override void InstallBindings()
		{
			Container.Bind<LevelEditorRoot>().FromNew().AsSingle().NonLazy();
			
			Container.BindInstance<MainRootPanelLevelEditor>(_mainRootPanelLevelEditor);
			Container.BindInterfacesAndSelfTo<InputRaycastService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<CellCacheService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelfTo<CameraService>().FromNew().AsSingle();
			
			Container.Resolve<LocalDependencyService>().SetLocalContainer(Container);
		}

		private void OnDisable() =>
			Container.Resolve<LocalDependencyService>().Clear();
	}
}