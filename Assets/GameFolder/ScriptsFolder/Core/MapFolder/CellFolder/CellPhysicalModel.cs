using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class CellPhysicalModel : MonoBehaviour
	{
		[SerializeField] private CellObject _cellObject;
		[SerializeField] private Collider[] _colliders;
		
		public CellObject CellObject => _cellObject;

		public void Enable()
		{
			foreach(Collider collider in _colliders)
				collider.enabled = true;
			
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			foreach(Collider collider in _colliders)
				collider.enabled = false;

			gameObject.SetActive(false);
		}

	}
}