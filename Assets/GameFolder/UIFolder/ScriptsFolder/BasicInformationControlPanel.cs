using System;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class BasicInformationControlPanel : MonoBehaviour
	{
		[SerializeField] private SettingsButton _settingsButton;
		[SerializeField] private HomeButton _homeButton;

		public event Action OnSettingsButtonClicked;
		public event Action OnHomeButtonClicked;

		public void Initialization()
		{
			_settingsButton.OnClicked += SettingsButtonOnClicked;
			_homeButton.OnClicked += HomeButtonOnClicked;
		}

		private void HomeButtonOnClicked() =>
			OnHomeButtonClicked?.Invoke();

		private void SettingsButtonOnClicked() =>
			OnSettingsButtonClicked?.Invoke();

		public void LockInteractiveObjects()
		{
			_settingsButton.SetInteractable(false);
			_homeButton.SetInteractable(false);
		}

		public void SetToDefaultState()
		{
			_settingsButton.Activate();
			_homeButton.Activate();
		}
	}
}