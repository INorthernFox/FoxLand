using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class BasicInformationPanel : MonoBehaviour
	{
		[SerializeField] private BasicInformationControlPanel _controlPanel;
		[SerializeField] private LevelInfoPanel _levelInfoPanel;
		[SerializeField] private CurrencyRootPanel _currencyRootPanel;

		public BasicInformationControlPanel ControlPanel => _controlPanel;
		public LevelInfoPanel LevelInfoPanel => _levelInfoPanel;
		public CurrencyRootPanel CurrencyRootPanel => _currencyRootPanel;
		
		public void Initialization(GameConstantsSettingsValue constantsSettingsValue, CurrencyServices currencyServices)
		{
			_levelInfoPanel.Initialization(constantsSettingsValue);
			_controlPanel.Initialization();
			_currencyRootPanel.Initialization(currencyServices);
		}
	}

}