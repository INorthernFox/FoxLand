using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class LowerRootPanelLevelEditor : MonoBehaviour
	{
		[SerializeField] private LowerButtonsPanel _buttonsPanel;
		public void DeactivateAll()
		{
			gameObject.SetActive(false);
			_buttonsPanel.Deactivate();
		}
	}
}