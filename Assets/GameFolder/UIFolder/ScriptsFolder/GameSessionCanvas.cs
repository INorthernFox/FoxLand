using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class GameSessionCanvas : MonoBehaviour
	{
		[SerializeField] private GamePanel _gamePanel;
		[SerializeField] private BasicInformationPanel _basicInformationPanel;

		public GamePanel GamePanel => _gamePanel;
		public BasicInformationPanel BasicInformationPanel => _basicInformationPanel;
		
		public void Initialization(ILevelEventProcessorService levelEventProcessorService, GameConstantsSettingsValue gameConstantsSettingsValue, CurrencyServices currencyServices)
		{
			_gamePanel.Initialization(levelEventProcessorService);
			_basicInformationPanel.Initialization(gameConstantsSettingsValue, currencyServices);
		}
	}
}