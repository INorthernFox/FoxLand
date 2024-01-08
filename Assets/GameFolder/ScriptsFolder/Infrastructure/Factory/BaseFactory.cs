using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFolder.ScriptsFolder.Infrastructure.Factory
{
	public class BaseFactory<T> where T : MonoBehaviour
	{
		private readonly Transform _root;
		private readonly T _prefab;

		public BaseFactory(Transform root, T prefabs)
		{
			_root = root;
			_prefab = prefabs;
		}

		public T Create() =>
			Object.Instantiate((T) _prefab, _root);
	}


}