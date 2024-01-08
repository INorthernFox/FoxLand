using System;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public interface ILevelEventProcessorService
	{
		event Action OnLevelStarted;
		event Action OnLevelCompleted;
		event Action OnLevelFailed;
		event Action OnLevelRestarted;
	}

	public class LevelEventProcessorService : IService, ILevelEventProcessorService
	{
		public LevelEventProcessorService()
		{

		}

		public event Action OnLevelStarted;
		public event Action OnLevelCompleted;
		public event Action OnLevelFailed;
		public event Action OnLevelRestarted;
		
		public void LevelStarted() =>
			OnLevelStarted?.Invoke();

		public void LevelCompleted() =>
			OnLevelCompleted?.Invoke();


		public void LevelFailed() =>
			OnLevelFailed?.Invoke();

		public void LevelRestarted() =>
			OnLevelRestarted?.Invoke();
	}
}