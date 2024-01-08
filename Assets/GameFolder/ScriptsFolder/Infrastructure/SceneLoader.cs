using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameFolder.ScriptsFolder.Infrastructure
{
	public class SceneLoader
	{
		private readonly ZenjectSceneLoader _sceneLoader;

		private string CurrentSceneName => SceneManager.GetActiveScene().name;

		public SceneLoader(ZenjectSceneLoader zenjectSceneLoader) =>
			_sceneLoader = zenjectSceneLoader;

		public async UniTask Load(string name, Action onLoaded = null)
		{
			if(CurrentSceneName.Equals(name))
			{
				onLoaded?.Invoke();
				return;
			}

			await LoadScene(name, onLoaded);
		}

		public async UniTask LoadWithArgument<T, TW>(string nextScene, TW argumet, Action onLoaded = null) where T : IZenjectSceneLoaderTarget =>
			await LoadScene<T, TW>(nextScene, argumet, onLoaded);

		public async UniTask Reload(Action onLoaded = null) =>
			await LoadScene(CurrentSceneName, onLoaded);

		private async UniTask LoadScene(string nextScene, Action onLoaded = null)
		{
			await _sceneLoader.LoadSceneAsync(nextScene);
			onLoaded?.Invoke();
		}

		private async UniTask LoadScene<T, TW>(string nextScene, TW argumet, Action onLoaded = null) where T : IZenjectSceneLoaderTarget
		{
			await _sceneLoader.LoadSceneAsync(nextScene, LoadSceneMode.Single, (container) => container.BindInstance(argumet).WhenInjectedInto<T>());
			onLoaded?.Invoke();
		}
	}

	public interface IZenjectSceneLoaderTarget
	{
		[Inject]
		public void ZenjectSceneLoaderInject();
	}
}