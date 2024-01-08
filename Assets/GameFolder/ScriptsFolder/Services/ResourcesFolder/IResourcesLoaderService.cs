using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.ResourcesFolder
{
	public interface IResourcesLoaderService : IService
	{
		UniTask<T> LoadAsync<T>(string path) where T : Object;
		T Load<T>(string path) where T : Object;
	}
}