using System;
using Zenject;

namespace GameFolder.ScriptsFolder.Services
{
	public class LocalDependencyService : IService
	{
		private DiContainer _container;

		public void SetLocalContainer(DiContainer diContainer) =>
			_container = diContainer;

		public T GetDependency<T>()
		{
			if(_container == null)
				throw new Exception("This space has no local dependencies");
			
			return _container.Resolve<T>();
		}

		public void Clear() =>
			_container = null;
	}
}