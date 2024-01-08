using UnityEngine;

namespace GameFolder.LevelEditor.LevelEditor
{
	public class LoadLevelEditorPanel : MonoBehaviour
	{
		public void Deactivate() =>
			gameObject.SetActive(false);
		
		public void Activate() =>
			gameObject.SetActive(false);
	}
}