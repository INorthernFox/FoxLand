using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using TMPro;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class LevelInfoPanel : MonoBehaviour
	{
		[SerializeField] private TMP_Text _levelText;
		
		private LevelInfoUISampleConvector _convector;

		public void Initialization(GameConstantsSettingsValue constantsSettingsValue)
		{
			_convector = constantsSettingsValue.Text.LevelInfoUISampleConvector;
			
			UpdateText(19);
		}

		public void UpdateText(int level) =>
			_levelText.text = _convector.GetText(level);

		public void Activate() =>
			gameObject.SetActive(true);

		public void Deactivate() =>
			gameObject.SetActive(false);

	}
}