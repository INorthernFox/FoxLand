using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class ProjectionCellObject : MonoBehaviour
	{
		public void Enable()
		{
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			gameObject.SetActive(false);
		}
		
	}
}