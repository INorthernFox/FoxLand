using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class CellVisualModel : MonoBehaviour
	{
		[SerializeField] private MeshRenderer[] _renderers;
		
		public void RecolorAllObject(Material material)
		{
			foreach(MeshRenderer meshRenderer in _renderers)
			{
				Material[] materials = new Material[meshRenderer.materials.Length];

				for( int i = 0; i <  materials.Length; i++ )
					materials[i] = material;

				meshRenderer.materials = materials;
			}
		}
	}

}