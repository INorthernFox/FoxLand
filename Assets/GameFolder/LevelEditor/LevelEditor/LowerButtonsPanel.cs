using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class LowerButtonsPanel : MonoBehaviour
	{
		public void Deactivate() =>
			gameObject.SetActive(false);
	}
}