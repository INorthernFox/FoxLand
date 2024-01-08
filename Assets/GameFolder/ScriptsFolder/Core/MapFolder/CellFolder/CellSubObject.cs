using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class CellSubObject : MonoBehaviour
	{
		[SerializeField] private CellPathSettings _cellPathSettings;
		[SerializeField] private CellVisualModel _cellVisualModel;

		public CellPathSettings CellPathSettings => _cellPathSettings;
		public CellVisualModel CellVisualModel => _cellVisualModel;
			
			
	}
}