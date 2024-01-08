using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class BaseLevelPanel : MonoBehaviour
	{

		public void Deactivate() =>
			gameObject.SetActive(false);
		
		public void Activate() =>
			gameObject.SetActive(true);
	}
}