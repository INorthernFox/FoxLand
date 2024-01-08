using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFolder.ScriptsFolder.Infrastructure.Factory
{
	public class BasePool<T> : IDisposable where T : MonoBehaviour
	{
		private readonly BaseFactory<T> _factory;
		private readonly Queue<T> _pool;
		
		public BasePool(Transform root, T prefabs) =>
			_factory = new BaseFactory<T>(root, prefabs);
		
		public T Get() =>
			_pool.Count <= 0 ? _factory.Create() : _pool.Dequeue();

		public void Set(T player) =>
			_pool.Enqueue(player);

		public void Dispose()
		{
			while(_pool.Count > 0)
				Object.Destroy(_pool.Dequeue().gameObject);

			_pool.Clear();
		}
	}
}