using GameFolder.ScriptsFolder.Infrastructure.States;
using Zenject;

namespace GameFolder.ScriptsFolder.Infrastructure.Installers
{
	public class GameStateFactory
	{
		private readonly DiContainer _container;
		
		public GameStateFactory(DiContainer container)
		{
			_container = container;
		}
		
		public T Create<T>() where T : IExitableState =>
			_container.Instantiate<T>();
	}
}